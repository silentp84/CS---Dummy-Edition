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
      bool select;
      bool move;
      string cPair;

      Console.WriteLine(Char.GetNumericValue('b'));

      while(game)
      {
        select = false;
        move = false;
        while (!select)
        {
          PrintBoard(gameBoard);
          Console.WriteLine(gameBoard.Player + " player's turn: ");
          Console.WriteLine("Select a piece to move/jump by typing coordinates. (e.g. '5A')");
          cPair = Console.ReadLine();
          if (cPair.Length == 2)
          {
            try
            {
              gameBoard.Row = (int)Char.GetNumericValue(cPair[0]);
              gameBoard.Col = ((byte)cPair[1]) - 65;
            }
            catch (Exception)
            {
              Console.WriteLine("Invalid entry.");
              Console.Read();
              continue;
            }
            if (!gameBoard.CheckValidPiece(cPair.Length))
            {
              Console.WriteLine("Invalid entry.");
              Console.Read();
            }
            else if (gameBoard.MoveRequiresJump())
            {
              Console.WriteLine("Invalid entry: You must select a piece that must make a jump.");
              Console.Read();
            }
            else if (gameBoard.MovePossible())
            {
              Console.WriteLine("Invalid entry: You must select a piece that has moves.");
              Console.Read();
            }
            else
            {
              Console.WriteLine("Acceptable entry.");
              Console.Read();
              select = true;
            }
          }
          else
          {
            Console.WriteLine("Invalid entry: Need a length of at least 2.");
            Console.Read();
          }
        }

        while (!move)
        {
          PrintBoard(gameBoard);
          Console.WriteLine(gameBoard.Player + " player's turn: ");
          Console.WriteLine("Select a target by typing coordinates. (e.g. '5A')");
          cPair = Console.ReadLine();
          if (cPair.Length == 2)
          {
            try
            {
              gameBoard.TargetRow = (int)Char.GetNumericValue(cPair[0]);
              gameBoard.TargetCol = ((byte)cPair[1]) - 65;
            }
            catch (Exception)
            {
              Console.WriteLine("Invalid entry.");
              Console.Read();
              continue;
            }
            if (!gameBoard.IsTargetValid())
            {
              Console.WriteLine("Target not valid.");
              Console.Read();
            }
            else
            {
              Console.WriteLine("Target is probably valid.");
              move = gameBoard.MoveJump();
              if (move)
              {
                gameBoard.Swap();
              }
            }
          }
        }

      }
    }

    static void PrintBoard(Board layout)
    {

      Console.Clear();
      Console.Write("       A  B  C  D  E  F  G  H");

      for (int i = 0; i < 8; i++)
      {
        Console.WriteLine("\n---------------------------------");
        Console.Write("| " + i + " | ");
        for (int j = 0; j < 8; j++)
        {
          if (layout.GameBoard[i, j] == null)
          {
            Console.Write("  ");
          }
          else
          {
            Console.Write(layout.GameBoard[i, j].PieceType);
          }
          Console.Write("|");
        }
        Console.Write(" " + i + " |");
      }
      Console.Write("\n---------------------------------\n");
      Console.WriteLine("       A  B  C  D  E  F  G  H\n");
    }
  }
}
