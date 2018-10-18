using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Checkers
{
  public class Board
  {
    Dictionary<int, string> Piece = new Dictionary<int, string>() {{ 0, "**"}, { 1, "PP" }, { 2, "CC"}};

    public Board()
    {
      // 0 empty 1 player 2 computer
      GameBoard = new int[,] { { 2, 2, 2, 2 }, { 2, 2, 2, 2 }, { 2, 2, 2, 2 }, { 0, 0, 0 , 0 }, { 0, 0, 0, 0 }, { 1, 1, 1, 1 }, { 1, 1, 1, 1 }, { 1, 1, 1, 1 } };
    }

    public int[,] GameBoard { get; set; }

    public void Display()
    {
      bool row = true;

      for (int i = 0; i < 8; i++)
      {
        Console.WriteLine();
        for (int j = 0; j < 4; j++)
        {
          if (row)
            Console.Write("  ");
          Console.Write(Piece[GameBoard[i, j]]);
          if (!row)
            Console.Write("  ");
        }        
        row = !row;
      }
    }
  }

  public class Player
  {

  }

  class Program
  {
    static void Main(string[] args)
    {
      Board initial = new Board();
      initial.Display();
    }
  }
}
