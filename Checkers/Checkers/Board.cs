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

    public Piece[,] GameBoard { get; set; } //a multidimensional array that states if a position is occupied or not

    public Board()
    {
      GameBoard = new Piece[8, 8];
      Reset();
    }

    public void Reset()
    {
      for (int row = 0; row < 3; row++) //set computer pieces
      {
        for (int col = 0; col < 8; col += 2)
        {
          int index = row % 2 == 0 ? col + 1 + 8 * row : col + 8 * row;

          Piece black = new Piece(index % 8, row, 2);
          Piece red = new Piece(7 - index % 8, 7 - row, 1);
          GameBoard[black.locy, black.locxInt] = black;
          GameBoard[red.locy, red.locxInt] = red;
        }
      }
    }
  }
}
