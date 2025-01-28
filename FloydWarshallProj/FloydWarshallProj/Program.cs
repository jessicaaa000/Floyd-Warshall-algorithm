using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using System.Windows.Forms;
using FloydWarshallCs;
using System.IO;
using System.Threading;
using System.Diagnostics;
using System.Drawing;

namespace FloydWarshallProj
{
    internal static class Program
    {
        /// <summary>
        /// Główny punkt wejścia dla aplikacji.
        /// </summary>

        [DllImport(@"C:\projects\Floyd-Warshall-algorithm\FloydWarshallProj\x64\Debug\FloydWarshallAsm.dll")]
        static extern unsafe int InitializeRowAsm(int row, int vertices, int* address);

        [DllImport(@"C:\projects\Floyd-Warshall-algorithm\FloydWarshallProj\x64\Debug\FloydWarshallAsm.dll")]
        static extern unsafe int CalculateRowForKAsm(int* row, int* kRow, int k, int vertices, int* address);

        private static object _matrixLock = new object();

        [STAThread]
        static void Main()
        {
            int x = 1, y = 3, z = 4;
            //int val2 = CalculateRowForKAsm(x, y);

            //DANE - do asm i do c#
            int vertices = 5;
            int numOfThreads = 2;
            int[,] distanceMatrixCs = new int[vertices, vertices];
            int[,] distanceMatrixAsm = new int[vertices, vertices];
            string filePath = @"C:\projects\Floyd-Warshall-algorithm\FloydWarshallProj\FloydWarshallProj\bin\x64\Debug\graph.txt";

            //CSHARP

            //inicjalizacja macierzy odleglosci - tu beda przechowywane wyniki CSHARP
            FloydWarshallCalculator calculatorCs = new FloydWarshallCalculator();


            //POMIARY

            // Rozpoczęcie pomiaru czasu całkowitego
            Stopwatch totalTimeCs = new Stopwatch();
            totalTimeCs.Start();

            // Mierzenie czasu inicjalizacji
            Stopwatch initTimeCs = new Stopwatch();
            initTimeCs.Start();


            Thread[] initThreadsCs = new Thread[numOfThreads];

            // Tworzenie wątków dla inicjalizacji macierzy i INICJALIZACJA MACIERZY
            for (int threadId = 0; threadId < numOfThreads; threadId++)
            {
                int currentThreadId = threadId; // Zachowanie aktualnego ID wątku
                initThreadsCs[threadId] = new Thread(() =>
                {
                    for (int row = 0; row < vertices; row++)
                    {
                        if (row % numOfThreads == currentThreadId) // Warunek przydziału wiersza do wątku
                        {
                            int[] rowData = calculatorCs.InitializeRowCs(row, vertices); // Obliczanie wiersza
                            lock (_matrixLock)
                            {
                                for (int j = 0; j < vertices; j++)
                                {
                                    distanceMatrixCs[row, j] = rowData[j];
                                }
                            }
                        }
                    }
                });
                initThreadsCs[threadId].Start();
            }

            foreach (Thread thread in initThreadsCs)
            {
                thread.Join();
            }

            initTimeCs.Stop();
            Console.WriteLine($"Czas inicjalizacji macierzy w CSharp: {initTimeCs.ElapsedMilliseconds} ms");


            LoadDataFromFile(filePath, ref distanceMatrixCs);

            // Mierzenie czasu obliczeń
            Stopwatch computeTimeCs = new Stopwatch();
            computeTimeCs.Start();

            // Główna pętla algorytmu z wielowątkowością
            for (int k = 0; k < vertices; k++)   // (k to wierzcholek przez ktory sprawdzamy droge)
            {
                int[] kRow = GetRow(distanceMatrixCs, k);               //bierzemy odleglosci z tego wierzcholka
                int currentK = k;                            //wierzcholek przez ktory sprawdzamy droge

                // Tworzenie wątków dla każdego wiersza w danej iteracji k
                Thread[] computeThreads = new Thread[numOfThreads];
                for (int threadId = 0; threadId < numOfThreads; threadId++)
                {
                    int currentThread = threadId;
                    computeThreads[threadId] = new Thread(() =>
                    {
                        for (int i = 0; i < vertices; i++)
                        {
                            if (i % numOfThreads == currentThread) // Warunek przydziału wiersza do wątku
                            {
                                if (i != currentK) // Sprawdzamy, czy nie obliczamy wiersza, który jest równy k - nie ma takiej potrzeby bo te dane sie nie zmienia
                                {
                                    int[] rowData = GetRow(distanceMatrixCs, i); // wyciagamy wiersz o aktualnym i
                                    int[] newRow = calculatorCs.CalculateRowForKCs(rowData, kRow, currentK, vertices);  //sprawdzamy czy przez wierzcholek k sa krotsze drogi

                                    lock (_matrixLock)
                                    {
                                        for (int j = 0; j < vertices; j++)
                                        {
                                            distanceMatrixCs[i, j] = newRow[j];
                                        }
                                    }
                                }
                            }
                        }
                    });
                    computeThreads[threadId].Start();
                }

                // Czekanie na zakończenie obliczeń dla danej iteracji k
                foreach (Thread thread in computeThreads)
                {
                    thread.Join();
                }
            }

            computeTimeCs.Stop();
            Console.WriteLine($"Czas obliczeń w CSharp: {computeTimeCs.ElapsedMilliseconds} ms");

            totalTimeCs.Stop();
            Console.WriteLine($"Całkowity czas wykonania w CSharp: {totalTimeCs.ElapsedMilliseconds} ms");

            PrintDistanceMatrix(distanceMatrixCs);
            Console.ReadLine();



            //OBLICZENIA W ASM

            //CSHARP

            //inicjalizacja macierzy odleglosci - tu beda przechowywane wyniki CSHARP
            //moze jakas funckja w asm


            //POMIARY

            // Rozpoczęcie pomiaru czasu całkowitego
            Stopwatch totalTimeAsm = new Stopwatch();
            totalTimeCs.Start();

            // Mierzenie czasu inicjalizacji
            Stopwatch initTimeAsm = new Stopwatch();
            initTimeAsm.Start();


            Thread[] initThreadsAsm = new Thread[numOfThreads];

            unsafe {
                // Tworzenie wątków dla inicjalizacji macierzy - wszystko dla podanych wczesniej watkow - I INICJALIZACJA MACIERZY
                for (int threadId = 0; threadId < numOfThreads; threadId++)
                {
                    int currentThreadId = threadId; // Zachowanie aktualnego ID wątku
                    initThreadsAsm[threadId] = new Thread(() =>
                    {
                    for (int row = 0; row < vertices; row++)
                    {
                            if (row % numOfThreads == currentThreadId) // Warunek przydziału wiersza do wątku
                            {
                                lock (_matrixLock)
                                {
                                    // Alokujemy pamięć o rozmiarze: wierzcholki + 16(to dla datat alignemnt w instrukcjach SSE
                                    IntPtr rawPointer = Marshal.AllocHGlobal((vertices + 16) * sizeof(int) + 32); //dodajemy 3 jeszcze gdyby przy instrukcjach wektorowych wyszlo poza zakres

                                    // Obliczamy adres wyrównany do 16 bajtów
                                    long rawAddress = rawPointer.ToInt64();
                                    long alignedAddress = (rawAddress + (16 - 1)) & ~(16 - 1);
                                    IntPtr alignedPointer = new IntPtr(alignedAddress);
                                    int* address = (int*)alignedPointer;
                                    InitializeRowAsm(row, vertices, address); // Inicjalizacja w Asm
                                    for (int j = 0; j < vertices; j++)
                                    {
                                        distanceMatrixAsm[row, j] = *(address + j);
                                    }
                                }
                            }
                        }
                    });
                    initThreadsAsm[threadId].Start();
                }
            }

            foreach (Thread thread in initThreadsAsm)
            {
                thread.Join();
            }

            initTimeAsm.Stop();
            Console.WriteLine($"Czas inicjalizacji macierzy w Asm: {initTimeAsm.ElapsedMilliseconds} ms");

            LoadDataFromFile(filePath, ref distanceMatrixAsm);
            PrintDistanceMatrix(distanceMatrixAsm);

            // Mierzenie czasu obliczeń
            // Mierzenie czasu obliczeń
            Stopwatch computeTimeAsm = new Stopwatch();
            computeTimeAsm.Start();

            // Główna pętla algorytmu z wielowątkowością
            for (int k = 0; k < vertices; k++)   // (k to wierzcholek przez ktory sprawdzamy droge)
            {
                int[] kRow = GetRow(distanceMatrixAsm, k);               //bierzemy odleglosci z tego wierzcholka
                int currentK = k;                            //wierzcholek przez ktory sprawdzamy droge

                // Tworzenie wątków dla każdego wiersza w danej iteracji k
                Thread[] computeThreadsAsm = new Thread[numOfThreads];

                unsafe
                {
                    for (int threadId = 0; threadId < numOfThreads; threadId++)
                    {
                        int currentThread = threadId;
                        computeThreadsAsm[threadId] = new Thread(() =>
                        {
                            for (int i = 0; i < vertices; i++)
                            {
                                if (i % numOfThreads == currentThread) // Warunek przydziału wiersza do wątku
                                {
                                    if (i != currentK) // Sprawdzamy, czy nie obliczamy wiersza, który jest równy k - nie ma takiej potrzeby bo te dane sie nie zmienia
                                    {
                                        lock (_matrixLock)
                                        {
                                            // Alokujemy pamięć o rozmiarze: wierzcholki + 16(to dla datat alignemnt w instrukcjach SSE
                                            IntPtr rawPointer = Marshal.AllocHGlobal((vertices + 16) * sizeof(int) + 32); //dodajemy 3 jeszcze gdyby przy instrukcjach wektorowych wyszlo poza zakres

                                            // Obliczamy adres wyrównany do 16 bajtów
                                            long rawAddress = rawPointer.ToInt64();
                                            long alignedAddress = (rawAddress + (16 - 1)) & ~(16 - 1);
                                            IntPtr alignedPointer = new IntPtr(alignedAddress);
                                            int* address = (int*)alignedPointer;
                                            //int*[] rowData = &GetRow(distanceMatrixAsm, i); // wyciagamy wiersz o aktualnym i
                                            fixed (int* matrixPtr = &distanceMatrixAsm[0, 0])
                                            {
                                                int* rowPtr = matrixPtr + (i * vertices);
                                                int* kRowPtr = matrixPtr + (currentK * vertices);
                                                Console.WriteLine($"k: {currentK}, current row: {i}, Value at rowPtr: 0x{(long)rowPtr:X}");
                                                Console.WriteLine($"Value at kRowPtr: 0x{(long)kRowPtr:X}");
                                                Console.WriteLine($"address: 0x{(long)address:X}");
                                                //Console.WriteLine($"k: {currentK}, current row: {i}, Value at rowPtr: {rowPtr}");
                                                Console.WriteLine($"Value at kRowPtr: {*kRowPtr}");
                                                CalculateRowForKAsm(rowPtr, kRowPtr, currentK, vertices, address);  //sprawdzamy czy przez wierzcholek k sa krotsze drogi - OBLICZENIA ASM
                                            }
                                                for (int j = 0; j < vertices; j++)
                                            {
                                                distanceMatrixAsm[i, j] = *(address + j);
                                            }
                                        }
                                    }
                                }
                            }
                        });
                        computeThreadsAsm[threadId].Start();
                    }
                }

                // Czekanie na zakończenie obliczeń dla danej iteracji k
                foreach (Thread thread in computeThreadsAsm)
                {
                    thread.Join();
                }
            }

            computeTimeAsm.Stop();
            Console.WriteLine($"Czas obliczeń w Asm: {computeTimeAsm.ElapsedMilliseconds} ms");

            totalTimeAsm.Stop();
            Console.WriteLine($"Całkowity czas wykonania w Asm: {totalTimeAsm.ElapsedMilliseconds} ms");

            PrintDistanceMatrix(distanceMatrixAsm);
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
                        lock (_matrixLock)
                        {
                            matrix[from, to] = weight;
                        }
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
            int[] row = new int[matrix.GetLength(1)];
            int cols = matrix.GetLength(1);           //nie ma znaczenia bo tyle ile kolumn tyle wierszy

            lock (_matrixLock)
            {
                for (int j = 0; j < cols; j++)
                {
                    row[j] = matrix[rowIndex, j];
                }
            }
            return row;
        }

        private static void PrintDistanceMatrix(int[,] matrix)
        {
            int vertices = matrix.GetLength(0);
            Console.WriteLine("\nShortest distance matrix:");
            for (int i = 0; i < vertices; i++)
            {
                for (int j = 0; j < vertices; j++)
                {
                    if (matrix[i, j] >= int.MaxValue)
                        Console.Write("INF\t");
                    else
                        Console.Write($"{matrix[i, j]}\t");
                }
                Console.WriteLine();
            }
        }
    }
}