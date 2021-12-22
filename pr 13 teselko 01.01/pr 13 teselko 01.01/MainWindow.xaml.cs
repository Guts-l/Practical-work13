using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.IO;
using LibMas;
using Microsoft.Win32;

namespace pr_13_teselko_01._01
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private Modules modules = new();
        SumAndProdOfKColumn sumAndProd = new();
        private int[,] matrix;

        public MainWindow()
        {
            InitializeComponent();
        }

        private void input_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key < Key.D0 || e.Key > Key.D9)
            {
                if (e.Key < Key.NumPad0 || e.Key > Key.NumPad9)
                    e.Handled = true;
            }
            else e.Handled = false;
        }

        private void input_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Space)
            {
                e.Handled = true;
            }
        }

        private void open_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog
            {
                DefaultExt = ".txt"
            };

            if (openFileDialog.ShowDialog() == false)
            {
                return;
            }

            matrix = modules.Open(openFileDialog.FileName);
            inputRowCount.Text = Convert.ToString(matrix.GetLength(0));
            inputColumnCount.Text = Convert.ToString(matrix.GetLength(1));
            mainDataGrid.ItemsSource = matrix.ToDataTable().DefaultView;
        }

        private void save_Click(object sender, RoutedEventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog
            {
                DefaultExt = ".txt"
            };
            if (mainDataGrid.ItemsSource != null)
            {
                if (saveFileDialog.ShowDialog() == true)
                {
                    modules.Save(saveFileDialog.FileName);
                }
            }
            else MessageBox.Show("Нечего сохранять", "Ошибка");
        }

        private void clear_Click(object sender, RoutedEventArgs e)
        {
            inputRowCount.Clear();
            inputColumnCount.Clear();
            sumOutput.Clear();
            prodOutput.Clear();
            modules.Clear(matrix);
            mainDataGrid.ItemsSource = VisualMatrix.ToDataTable(matrix).DefaultView;
        }

        private void about_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Практическая работа №13\n" +
                "Теселько Максим ИСП-34\n" +
                "Дана матрица размера M * N и целое число K (1 < K < N). Найти сумму и произведение элементов K-го столбца данной матрицы.", "О программе", MessageBoxButton.OK);
        }

        private void exit_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void fillMatrix_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                int rowCount = Int32.Parse(inputRowCount.Text);
                int columnCount = Int32.Parse(inputColumnCount.Text);
                matrix = modules.Generate(rowCount, columnCount);
                mainDataGrid.ItemsSource = VisualMatrix.ToDataTable(matrix).DefaultView;
            }
            catch
            {
                MessageBox.Show("Введите размеры", "Ошибка");
            }
            matrixInfo.Content = $"Матрица {matrix.GetLength(0)} x {matrix.GetLength(1)}";
        }

        private void getSumAndProd_Click(object sender, RoutedEventArgs e)
        {
            if (matrix != null)
            {
                try
                {
                    int k = Int32.Parse(inputK.Text);
                    if (1 < k && k < matrix.GetLength(1))
                    {
                        sumOutput.Text = sumAndProd.GetSum(matrix, k).ToString();
                        prodOutput.Text = sumAndProd.GetProduct(matrix, k).ToString();
                    }
                    else MessageBox.Show("0<K<Кол-ва столбцов", "Ошибка");
                }
                catch (Exception)
                {
                    MessageBox.Show("Введите данные", "Ошибка");
                }
            }
            else MessageBox.Show("Сгенерируйте таблицу", "Ошибка");
        }
    }
}
