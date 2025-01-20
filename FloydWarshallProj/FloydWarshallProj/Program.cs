using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using System.Windows.Forms;
using FloydWarshallCs;
using System.IO;

namespace FloydWarshallProj
{
    internal static class Program
    {
        /// <summary>
        /// Główny punkt wejścia dla aplikacji.
        /// </summary>

        [DllImport(@"C:\projects\Floyd-Warshall-algorithm\FloydWarshallProj\x64\Debug\FloydWarshallAsm.dll")]
        static extern int MyProc1(int a, int b);

        [STAThread]
        static void Main()
        {
            int x = 5, y = 3;
            int retVal = MyProc1(x, y);
            Console.Write("Moja pierwsza wartość obliczona w asm to:");
            Console.WriteLine(retVal);
            Console.ReadLine();

            Console.Write("Enter the number of vertices: ");
            int vertices = 4; // Możesz zmienić na Console.ReadLine() i sparsować

            // Inicjalizacja macierzy odległości
            int[,] distanceMatrix = new int[vertices, vertices];
            FloydWarshallCalculator calculator = new FloydWarshallCalculator();

            // Inicjalizacja wszystkich wierszy
            for (int i = 0; i < vertices; i++)
            {
                int[] row = calculator.InitializeRow(i, vertices);
                for (int j = 0; j < vertices; j++)
                {
                    distanceMatrix[i, j] = row[j];
                }
            }

            // Wczytanie danych z pliku
            Console.Write("Enter the path to the input file: ");
            string filePath = @"C:\projects\Floyd-Warshall-algorithm\FloydWarshallProj\FloydWarshallProj\bin\x64\Debug\graph.txt";
            LoadDataFromFile(filePath, ref distanceMatrix);

            // Główna pętla algorytmu Floyda-Warshalla
            for (int k = 0; k < vertices; k++)
            {
                int[] kRow = GetRow(distanceMatrix, k);

                for (int i = 0; i < vertices; i++)
                {
                    int[] currentRow = GetRow(distanceMatrix, i);
                    int[] newRow = calculator.CalculateRowForK(currentRow, kRow, k, vertices);

                    // Aktualizacja macierzy
                    for (int j = 0; j < vertices; j++)
                    {
                        distanceMatrix[i, j] = newRow[j];
                    }
                }
            }

            PrintDistanceMatrix(distanceMatrix);
            Console.ReadLine();
        }

        private static void LoadDataFromFile(string filePath, ref int[,] matrix)
        {
            try
            {
                string[] lines = File.ReadAllLines(filePath);
                foreach (var line in lines)
                {
                    string[] values = line.Split(' ').Select(v => v.Trim()).ToArray();
                    if (values.Length == 3)
                    {
                        int from = int.Parse(values[0]) - 1;
                        int to = int.Parse(values[1]) - 1;
                        int weight = int.Parse(values[2]);
                        matrix[from, to] = weight;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error reading file: {ex.Message}");
            }
        }

        private static int[] GetRow(int[,] matrix, int rowIndex)
        {
            int cols = matrix.GetLength(1);
            int[] row = new int[cols];
            for (int j = 0; j < cols; j++)
            {
                row[j] = matrix[rowIndex, j];
            }
            return row;
        }

        private static void PrintDistanceMatrix(int[,] matrix)
        {
            int vertices = matrix.GetLength(0);
            Console.WriteLine("Shortest distance matrix:");
            for (int i = 0; i < vertices; i++)
            {
                for (int j = 0; j < vertices; j++)
                {
                    if (matrix[i, j] == int.MaxValue)
                        Console.Write("INF\t");
                    else
                        Console.Write($"{matrix[i, j]}\t");
                }
                Console.WriteLine();
            }
        }
    }
}
