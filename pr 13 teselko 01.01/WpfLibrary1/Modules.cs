using System;
using System.Data;
using System.IO;

namespace LibMas
{
    public static class VisualMatrix
    {
        public static DataTable ToDataTable<T>(this T[,] matrix)
        {
            var res = new DataTable();
            for (int i = 0; i < matrix.GetLength(1); i++)
            {
                res.Columns.Add($"{i}", typeof(T));
            }

            for (int i = 0; i < matrix.GetLength(0); i++)
            {
                var row = res.NewRow();

                for (int j = 0; j < matrix.GetLength(1); j++)
                {
                    row[j] = matrix[i, j];
                }

                res.Rows.Add(row);
            }

            return res;
        }
    }

    public class Modules
    {
        private int[,] _matrix;
        private Random _random = new Random();

        public int[,] Generate(int n, int m, int min = -100, int max = 100)
        {
            _matrix = new int[n, m];
            for (int i = 0; i < _matrix.GetLength(0); i++)
            {
                for (int j = 0; j < _matrix.GetLength(1); j++)
                {
                    _matrix[i, j] = _random.Next(min, max + 1);
                }
            }
            return _matrix;
        }

        public int[,] Clear(int[,] matrix)
        {
            for (int i = 0; i < matrix.GetLength(0); i++)
            {
                for (int j = 0; j < matrix.GetLength(1); j++)
                {
                    matrix[i, j] = _random.Next(0, 0);
                }
            }
            return matrix;
        }

        public int[,] Open(string path)
        {
            if (!File.Exists(path))
            {
                throw new ArgumentException();
            }

            StreamReader read = new StreamReader(path);
            _matrix = new int[Convert.ToInt32(read.ReadLine()), Convert.ToInt32(read.ReadLine())];
            for (int i = 0; i < _matrix.GetLength(0); i++)
            {
                for (int j = 0; j < _matrix.GetLength(1); j++)
                {
                    _matrix[i, j] = Convert.ToInt32(read.ReadLine());
                }
            }
            return _matrix;
        }

        public void Save(string path)
        {

            {
                StreamWriter write = new StreamWriter(path);
                write.WriteLine(_matrix.GetLength(0));
                write.WriteLine(_matrix.GetLength(1));
                for (int i = 0; i < _matrix.GetLength(0); i++)
                {
                    for (int j = 0; j < _matrix.GetLength(1); j++)
                    {
                        write.WriteLine(_matrix[i, j]);
                    }
                }
                write.Close();
            }
        }
    }
}
