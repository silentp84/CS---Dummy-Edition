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

    public Piece[,] GameBoard { get; set; } //a multidimensional array of pieces
    private List<Piece> Reds { get; set; }
    private List<Piece> Blacks { get; set; }
    public List<int[]> JumpCoordinates { get; set; }
    private List<int[]> JumpDirections { get; set; }
    public int Row;
    public int Col;
    public int TargetRow;
    public int TargetCol;

    public string Player { get; set; }

    public Board()
    {
      GameBoard = new Piece[8, 8];
      Reds = new List<Piece>();
      Blacks = new List<Piece>();
      JumpCoordinates = new List<int[]>();
      JumpDirections = new List<int[]>
      {
        new int[] { -1, -1 },
        new int[] { -1, 1 }
      };
      Player = "Red";
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

    public void Swap()
    {
      if (Player == "Red")
      {
        Player = "Black";
      }
      else
      {
        Player = "Red";
      }
    }

    public bool MoveJump()
    {
      if (Math.Abs(Row - TargetRow) == 1)
      {
        return Move();
      }
      else if (Math.Abs(Row - TargetRow) == 2)
      {
        return Jump(); 
      }
      else
      {
        Console.Write("I broke.");
        Console.Read();
        return false;
      }
    }

    public bool Move()
    {
      Piece temp = GameBoard[Row, Col];
      GameBoard[TargetRow, TargetCol] = temp;
      GameBoard[Row, Col] = null;
      return true;
    }

    public bool Jump()
    {
      Piece temp = GameBoard[Row, Col];
      GameBoard[TargetRow, TargetCol] = temp;
      GameBoard[Row, Col] = null;
      GameBoard[(Row+TargetRow)/2, (Col+TargetCol)/2] = null;
      GetRequiredJumps_Moves();
      return (!GameBoard[TargetRow, TargetCol].Jumps.Any());
    }


    public bool IsTargetValid()
    {
      if (GameBoard[Row,Col].Moves.Any(p => p.SequenceEqual(new int[2] { TargetRow, TargetCol })))
      {
        return true;
      }
      else if (GameBoard[Row, Col].Jumps.Any(p => p.SequenceEqual(new int[2] { TargetRow, TargetCol })))
      {
        return true;
      }
      else
      {
        return false;
      }
    }

    public bool MoveRequiresJump()
    {
      GetRequiredJumps_Moves();
      return JumpCoordinates.Any() && !GameBoard[Row, Col].Jumps.Any();
    }

    public bool MovePossible()
    {
      return !GameBoard[Row, Col].Moves.Any();
    }

    public bool CheckValidPiece(int len)
    {
      
      if ((Row < 0 || Row > 7 || Col < 0 || Col > 7) && len!=2)
      {
        return false;
      }

      if (GameBoard[Row, Col].PieceType[0] == Player[0])
      {

        return true;
      }
      else
      {
        return false;
      }
    }

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
                GameBoard[piecetocheck.Row, piecetocheck.ColInt].Jumps.Add
                (new int[] { 2 * jump[0] + piecetocheck.Row, 2* jump[1] + piecetocheck.ColInt });
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
              GameBoard[piecetocheck.Row, piecetocheck.ColInt].Moves.Add
                (new int[] { jump[0] + piecetocheck.Row, jump[1] + piecetocheck.ColInt });
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
                GameBoard[piecetocheck.Row, piecetocheck.ColInt].Jumps.Add
                (new int[] { 2 * jump[0] + piecetocheck.Row, 2 * jump[1] + piecetocheck.ColInt });
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
              GameBoard[piecetocheck.Row, piecetocheck.ColInt].Moves.Add
                (new int[] { -1 * jump[0] + piecetocheck.Row, -1 * jump[1] + piecetocheck.ColInt });
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
      if (Player == "Red")
      {
        foreach (Piece red in Reds)
        {
          GameBoard[red.Row, red.ColInt].Jumps.Clear();
          GameBoard[red.Row, red.ColInt].Moves.Clear();
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
          GameBoard[black.Row, black.ColInt].Jumps.Clear();
          GameBoard[black.Row, black.ColInt].Moves.Clear();
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
