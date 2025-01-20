using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace FloydWarshallCs
{
    public class FloydWarshallCalculator
    {
        // Oblicza jeden rząd macierzy dla danej iteracji k
        public int[] CalculateRowForK(int[] row, int[] kRow, int k, int numberOfVertices)
        {
            int[] newRow = new int[numberOfVertices];
            Array.Copy(row, newRow, numberOfVertices);  //newRow to nasz rzad ktory bedzie zmieniany

            for (int j = 0; j < numberOfVertices; j++)
            {
                if (j != k) //bo to sie nie zmieni
                {
                    if (row[k] != int.MaxValue && kRow[j] != int.MaxValue)
                    {
                        int potentialNewPath = row[k] + kRow[j];
                        if (potentialNewPath < row[j])
                        {
                            newRow[j] = potentialNewPath;
                        }
                    }
                }
            }

            return newRow;
        }



        // Inicjalizuje rząd macierzy dla danego wierzchołka
        public int[] InitializeRow(int vertex, int numberOfVertices)
        {
            int[] row = new int[numberOfVertices];
            for (int j = 0; j < numberOfVertices; j++)
            {
                row[j] = vertex == j ? 0 : int.MaxValue;
            }
            return row;
        }
    }
}
