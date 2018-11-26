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
    public List<Piece> Reds { get; set; }
    public List<Piece> Blacks { get; set; }
    public List<int[]> JumpCoordinates { get; set; }
    public List<int[]> MoveCoordinates { get; set; }
    private List<int[]> JumpDirections { get; set; }

    public bool Player { get; set; }

    public Board()
    {
      GameBoard = new Piece[8, 8];
      Reds = new List<Piece>();
      Blacks = new List<Piece>();
      JumpCoordinates = new List<int[]>();
      MoveCoordinates = new List<int[]>();
      JumpDirections = new List<int[]>
      {
        new int[] { -1, -1 },
        new int[] { -1, 1 }
      };
      Player = true;
      Reset();
    }

    public void Reset()
    {
      for (int row = 0; row < 3; row++)
      {
        for (int col = 0; col < 8; col += 2)
        {
          int index = row % 2 == 0 ? col + 1 + 8 * row : col + 8 * row;

          Piece black = new Piece(row, index % 8, "BB");
          Piece red = new Piece( 7 - row, 7 - index % 8, "RR");
          GameBoard[black.Row, black.ColInt] = black;
          Blacks.Add(black);
          GameBoard[red.Row, red.ColInt] = red;
          Reds.Add(red);
        }
      }
    }

    //Check validate location
    
    /*
     * Parameter should include the coordinate to be checked and a list of required jumps
     * Get the row and column and check if the piece/location is valid (if it's its own color)
     * Maybe return the type of the location
     */

    //Check location move
    /*
     * Take a coordinate pair that checks the upleft,upright,downleft,downright
     * If the coordinate pair causes the check to be out of bounds or is a player piece then return false
     * If the location is an opponent check for a possible jump (call check jump)
     * If the location is empty return true
     * /

    //Check required jump

    //Move
    /*
     * Check if there needs to be jump and return coordinates (required jump)
     * Get the piece type (call validate location with any required jumps)
     *    If the entered location is not valid => kick it back out
     * (call location move) based on the type of piece
     *    Two calls for:
     *      -one to address a normal piece
     *        cycle through (call location moves for upleft/upright (red) or downleft/downright (black))
     *    Four calls total for:
     *      -the other to address a king piece
     *        cycle through (call location moves for remainder of moves)
     * If it's true display the location as a possible move
     */

    //Check Jump
    /*
     * Take a (coordinate pair X 2) that checks the jump possibility
     * 
     */
     public bool CheckDirections(Piece piecetocheck)
    {
      if (piecetocheck.PieceType[0] == 'R' || piecetocheck.PieceType[1] == 'K')
      {
        foreach (int[] jump in JumpDirections)
        {
          try
          {
            if (piecetocheck.Compare(GameBoard[jump[0] + piecetocheck.Row, jump[1] + piecetocheck.ColInt]))
            {
              if (GameBoard[2 * jump[0] + piecetocheck.Row, 2 * jump[1] + piecetocheck.ColInt] == null)
              {
                return true;
              }
            }
          }
          catch (IndexOutOfRangeException)
          {
            continue;
          }

          try
          {
            if (GameBoard[jump[0] + piecetocheck.Row, jump[1] + piecetocheck.ColInt] == null)
            {
              MoveCoordinates.Add(new int[] { piecetocheck.Row, piecetocheck.ColInt });
            }
          }
          catch (IndexOutOfRangeException)
          {
            continue;
          }
        }
      }
      if (piecetocheck.PieceType[0] == 'B' || piecetocheck.PieceType[1] == 'K')
      {
        foreach (int[] jump in JumpDirections)
        {
          try
          {
            if (piecetocheck.Compare(GameBoard[-1 * jump[0] + piecetocheck.Row, -1 * jump[1] + piecetocheck.ColInt]))
            {
              if (GameBoard[-2 * jump[0], -2 * jump[1]] == null)
              {
                return true;
              }
            }
          }
          catch (IndexOutOfRangeException)
          {
            continue;
          }

          try
          {
            if (GameBoard[-1 * jump[0] + piecetocheck.Row, -1 * jump[1] + piecetocheck.ColInt] == null)
            {
              MoveCoordinates.Add(new int[] { piecetocheck.Row, piecetocheck.ColInt });
            }
          }
          catch (IndexOutOfRangeException)
          {
            continue;
          }
        }
      }

      return false;
    }

     public void GetRequiredJumps_Moves()
    {
      JumpCoordinates.Clear();
      MoveCoordinates.Clear();
      if (Player == true)
      {
        foreach (Piece red in Reds)
        {
          if (CheckDirections(red))
          {
            JumpCoordinates.Add(new int[] { red.Row, red.ColInt });
          }
        }
      }
      else
      {
        foreach (Piece black in Blacks)
        {
          if (CheckDirections(black))
          {
            JumpCoordinates.Add(new int[] { black.Row, black.ColInt });
          }
        }
      }
    }

    //Jump

    /*public bool Move(int row, char col, ref bool doublej)
    {
      Piece temp = new Piece(row, col, 0);
      int tempcol = temp.locxInt;
      temp = GameBoard[row, tempcol];
      bool[] moveoptions = new bool[] { false, false, false, false }; //topleft, topright, bottomleft, bottomright

      if (temp.ptype == 1 || temp.ptype == 3)
      {
        if (temp.ptype == 1) //if the piece is a non king
        {
          if (temp.locxInt != 0) //if the piece is not on the far left column
          {
            if (GameBoard[row - 1, tempcol - 1] == null) // if upperleft is free
            {
              moveoptions[0] = true;
            }
            if (GameBoard[row - 1, tempcol - 1].ptype == 2) //if upperleft is a computer piece
            {
              if (GameBoard[row - 1, tempcol - 1].locxInt != 0 || (GameBoard[row - 1, tempcol - 1].locy != 0))
                //check to see if the comp piece is not in the first row or the first column
              {
                if(GameBoard[row - 2, tempcol - 2] == null) //check to see if a jump is available
                {
                  moveoptions[0] = true;
                }
              }
            }
          }

        }
      }
      else
      {
        moveoptions[0] = false;
      }
      foreach (bool x in moveoptions)
      {
        if (x) return x;
      }
    }*/
  }
}
