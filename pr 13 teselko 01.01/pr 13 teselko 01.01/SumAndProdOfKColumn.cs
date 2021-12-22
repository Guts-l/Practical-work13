using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace pr_13_teselko_01._01
{
    internal class SumAndProdOfKColumn
    {
        public int GetSum(int[,] matrix, int k)
        {
            int sum = 0;
            for (int i = 0; i < matrix.GetLength(0); i++)
            {
                for (int j = k; j <= k; j++)
                {
                    sum += matrix[i, j];
                }
            }
            return sum;
        }

        public int GetProduct(int[,] matrix, int k)
        {
            int prod = 1;
            for (int i = 0; i < matrix.GetLength(0); i++)
            {
                for (int j = k; j <= k; j++)
                {
                    prod *= matrix[i, j];
                }
            }
            return prod;
        }
    }
}
