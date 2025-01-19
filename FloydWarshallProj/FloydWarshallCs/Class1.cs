using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace FloydWarshallCs
{
    public class FloydWarshall
    {
        private int[,] distanceMatrix;
        private int numberOfVertices;

        public FloydWarshall(int numberOfVertices)
        {
            this.numberOfVertices = numberOfVertices;
            distanceMatrix = new int[numberOfVertices, numberOfVertices];

            // Initialize the distance matrix with maximum values
            for (int i = 0; i < numberOfVertices; i++)
            {
                for (int j = 0; j < numberOfVertices; j++)
                {
                    if (i == j)
                        distanceMatrix[i, j] = 0;
                    else
                        distanceMatrix[i, j] = int.MaxValue;
                }
            }
        }

        public void LoadDataFromFile(string filePath)
        {
            try
            {
                string[] lines = File.ReadAllLines(filePath);

                foreach (var line in lines)
                {
                    string[] values = line.Split(' ').Select(v => v.Trim()).ToArray();
                    if (values.Length == 3)
                    {
                        int from = int.Parse(values[0]) - 1; // Adjusting to zero-based indexing
                        int to = int.Parse(values[1]) - 1;
                        int weight = int.Parse(values[2]);

                        distanceMatrix[from, to] = weight;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error reading file: {ex.Message}");
            }
        }

        public void ComputeShortestPaths()
        {
            for (int k = 0; k < numberOfVertices; k++)
            {
                for (int i = 0; i < numberOfVertices; i++)
                {
                    for (int j = 0; j < numberOfVertices; j++)
                    {
                        if (distanceMatrix[i, k] != int.MaxValue && distanceMatrix[k, j] != int.MaxValue)
                        {
                            distanceMatrix[i, j] = Math.Min(distanceMatrix[i, j], distanceMatrix[i, k] + distanceMatrix[k, j]);
                        }
                    }
                }
            }
        }

        public void PrintDistanceMatrix()
        {
            Console.WriteLine("Shortest distance matrix:");
            for (int i = 0; i < numberOfVertices; i++)
            {
                for (int j = 0; j < numberOfVertices; j++)
                {
                    if (distanceMatrix[i, j] == int.MaxValue)
                        Console.Write("INF\t");
                    else
                        Console.Write($"{distanceMatrix[i, j]}\t");
                }
                Console.WriteLine();
            }
        }
    }
}
