using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Checkers
{
  class Program
  {
    static void Main(string[] args)
    {
      Board gameBoard = new Board();

      Player user = new Player("Frank");
      Player comp = new Player("Computer");

      bool game = true;
      bool select = false;
      string cPair;
      int c1 = 0;
      char c2;

      Console.WriteLine(Char.GetNumericValue('b'));

      while(game)
      {
        while (!select)
        {
          PrintBoard(gameBoard);
          Console.WriteLine("Select a piece to move by typing coordinates. (e.g. '5A')");
          cPair = Console.ReadLine();
          if(cPair.Length == 2)
          {
            c1 = (int)Char.GetNumericValue(cPair[0]);
            c2 = cPair[1];
            if (c1 < 0 || c1 > 8 || c2 < 'A' || c2 > 'H')
            { 
              Console.WriteLine("Invalid entry.");
              Console.Read();
            }
            else
            {
              select = true;
              Console.WriteLine("c1: " + c1 + "\nc2: " + c2);
            }
          }            
        }
        break;
      }
    }

    static void PrintBoard(Board layout)
    {

      Console.Clear();
      Console.Write("     A  B  C  D  E  F  G  H");

      for (int i = 0; i < 8; i++)
      {
        Console.WriteLine("\n----------------------------");
        Console.Write(i + " | ");
        for (int j = 0; j < 8; j++)
        {
          if (layout.GameBoard[i, j] == null)
          {
            Console.Write("  ");
          }
          else
          {
            Console.Write(layout.GameBoard[i, j].disp);
          }
          Console.Write("|");
        }
      }
      Console.WriteLine("\n----------------------------\n");

    }
  }
}
