using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Checkers
{
  class Board
  {
    // 0 empty 1 player 2 computer 3 player(king) 4 computer(king) from Piece class

    public List<Piece> PieceBoard { get; set; } //a list of the actual pieces with location and piece types
    public Piece[,] GameBoard { get; set; } //a multidimensional array that states if a position is occupied or not

    public Board()
    {
      List<Piece> PieceBoard = new List<Piece>();
      GameBoard = new Piece[8, 4];
    }

    public void Reset()
    {
      bool ColOffset = true;
      //y is row coordinate, x is col coordinate
      for (int y = 0; y < 3; y++) //set computer pieces
      {
        for (int x = 0; x < 8; x += 2)
        {
          if (ColOffset == true)
          {
            x += 1;
          }
          PieceBoard.Add(new Piece(x, y, 2)); //add piece to a list
        }
        ColOffset = !ColOffset;
      }

      for (int y = 0; y < 2; y++) //set empty locations
      {
        for (int x = 0; x < 8; x += 2)
        {
          if (ColOffset == true)
          {
            x += 1;
          }
          PieceBoard.Add(new Piece(x, y, 0));
        }
        ColOffset = !ColOffset;
      }

      for (int y = 0; y < 3; y++) //set player pieces
      {
        for (int x = 0; x < 8; x += 2)
        {
          if (ColOffset == true)
          {
            x += 1;
          }
          PieceBoard.Add(new Piece(x, y, 1));
        }
        ColOffset = !ColOffset;
      }

      for (int y = 0; y < 8; y++)
      {
        for (int x = 0; x < 4; x++)
        {
          GameBoard[y, x] = PieceBoard[0];
          PieceBoard.RemoveAt(0);
        }
      }
    }
  }
}
