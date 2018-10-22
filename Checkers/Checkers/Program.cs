using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Checkers
{
  public class Board
  {
    // 0 empty 1 player 2 computer 3 player(king) 4 computer(king)
    Dictionary<int, string> Piece = new Dictionary<int, string>() {{ 0, "**"}, { 1, "PP" }, { 2, "CC"}, { 3, "PK" }, { 4, "CK"} };

    public Board()
    {
      GameBoard = new int[,] { { 2, 2, 2, 2 }, { 2, 2, 2, 2 }, { 2, 2, 2, 2 }, { 0, 0, 0 , 0 }, { 0, 0, 0, 0 }, { 1, 1, 1, 1 }, { 1, 1, 1, 1 }, { 1, 1, 1, 1 } };
    }

    public int[,] GameBoard { get; set; }

    public void Display()
    {
      bool row = true;

      Console.Write("   0 1 2 3 4 5 6 7");

      for (int i = 0; i < 8; i++)
      {
        Console.WriteLine();
        Console.Write(i + " ");
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
    string Name { get; set; }

    public Player(string name)
    {
      Name = name;
    }
  }

  class Program
  {
    static void Main(string[] args)
    {
      Board initial = new Board();
      Player user = new Player("Frank");
      Player comp = new Player("Computer");
      initial.Display();


    }
  }
}
