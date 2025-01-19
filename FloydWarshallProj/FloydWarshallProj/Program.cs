﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using System.Windows.Forms;
using FloydWarshallCs;

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
            int vertices = 4;

            FloydWarshall fw = new FloydWarshall(vertices);

            Console.Write("Enter the path to the input file: ");
            string filePath = Console.ReadLine();

            fw.LoadDataFromFile(filePath);
            fw.ComputeShortestPaths();
            fw.PrintDistanceMatrix();
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Background());
        }
    }
}
