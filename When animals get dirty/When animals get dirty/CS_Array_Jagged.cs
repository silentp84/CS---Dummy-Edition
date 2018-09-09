using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp7
{
  class Program
  {
    static void _Main(string[] args)
    {
      int[][] matrix = new int[3][];
      for (int i = 0; i < matrix.Length; i++)
      {
        matrix[i] = new int[3];
        for (int j = 0; j < matrix[i].Length; j++)
          matrix[i][j] = i * 3 + j;
      }
      for (int i = 0; i < matrix.Length; i++)
        for (int j = 0; j < matrix[i].Length; j++)
          Console.WriteLine($"Matrix[{i}][{j}]): "+matrix[i][j]);
    }
  }
}
