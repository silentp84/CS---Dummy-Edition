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
      PrintBoard(gameBoard);

      while (game)
      {

      }
    }

    static void PrintBoard(Board piece)
    {
      bool row = true;

      Console.Write("     A  B  C  D  E  F  G  H");

      for (int i = 0; i < 8; i++)
      {
        Console.WriteLine("\n----------------------------");
        Console.Write(i + " | ");
        for (int j = 0; j < 4; j++)
        {
          if (row)
            Console.Write("  |");
          Console.Write(piece.Piece[piece.GameBoard[i, j]]);
          Console.Write("|");
          if (!row)
            Console.Write("  |");
        }
        row = !row;
      }
    }
  }
}
