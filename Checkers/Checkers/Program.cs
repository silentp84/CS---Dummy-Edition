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

      bool game = true;
      bool select;
      bool move;
      string cPair;

      while(game)
      {
        select = false;
        move = false;
        while (!select)
        {
          PrintBoard(gameBoard);
          gameBoard.GetRequiredJumps_Moves();
          Console.WriteLine(gameBoard.Player + " player's turn: ");
          Console.WriteLine("Select a piece to move/jump by typing coordinates. (e.g. '5A')");
          cPair = Console.ReadLine();
          if (cPair.Length == 2)
          {
            try
            {
              gameBoard.Row = (int)Char.GetNumericValue(cPair[0]);
              gameBoard.Col = ((byte)Char.ToUpper(cPair[1])) - 65;
            }
            catch (Exception)
            {
              Console.WriteLine("Invalid entry.");
              Console.ReadLine();
              continue;
            }

            if (!gameBoard.CheckValidPiece(cPair.Length))
            {
              Console.WriteLine("Invalid entry.");
              Console.ReadLine();
            }
            else if (gameBoard.MoveRequiresJump())
            {
              Console.WriteLine("Invalid entry: You must select a piece that must make a jump.");
              Console.ReadLine();
            }
            else if (gameBoard.MovePossible() && !gameBoard.MoveRequiresJump())
            {
              Console.WriteLine("Invalid entry: You must select a piece that has moves.");
              Console.ReadLine();
            }
            else
            {
              select = true;
            }
          }
          else
          {
            Console.WriteLine("Invalid entry: Need a length of at least 2.");
            Console.ReadLine();
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
              gameBoard.TargetCol = ((byte)Char.ToUpper(cPair[1])) - 65;
            }
            catch (Exception)
            {
              Console.WriteLine("Invalid entry.");
              Console.ReadLine();
              continue;
            }
            if (!gameBoard.IsTargetValid())
            {
              Console.WriteLine("Target not valid.");
              Console.ReadLine();
            }
            else
            {
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
        if (i == 0)
        {
          Console.Write("\tRR - Red Piece");
        }
        else if(i == 1)
        {
          Console.Write("\tRK - Red King");
        }
        else if(i == 2)
        {
          Console.Write("\tBB - Black Piece");
        }
        else if(i == 3)
        {
          Console.Write("\tBK - Black King");
        }
      }
      Console.Write("\n---------------------------------\n");
      Console.WriteLine("       A  B  C  D  E  F  G  H\n");
    }
  }
}
