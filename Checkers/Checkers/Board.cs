using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Checkers
{
  class Board
  {
    public Piece[,] GameBoard { get; set; }
    private List<Piece> Reds { get; set; }
    private List<Piece> Blacks { get; set; }
    private bool JumpRequiredBlack { get; set; }
    private bool JumpRequiredRed { get; set; }
    private List<int[]> JumpDirections { get; set; }
    public int Row;
    public int Col;
    public int TargetRow;
    public int TargetCol;
    private int Remove;

    public string Player { get; set; }

    public Board()
    {
      GameBoard = new Piece[8, 8];
      Reds = new List<Piece>();
      Blacks = new List<Piece>();
      JumpRequiredRed = false;
      JumpRequiredBlack = false;
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
      int id = 0;
      for (int row = 0; row < 3; row++)
      {
        for (int col = 0; col < 8; col += 2)
        {
          int index = row % 2 == 0 ? col + 1 + 8 * row : col + 8 * row;

          Piece black = new Piece(row, index % 8, 'B', 'B', id);
          Piece red = new Piece(7 - row, 7 - index % 8, 'R', 'R', id);
          GameBoard[black.Row, black.ColInt] = black;
          Blacks.Add(black);
          GameBoard[red.Row, red.ColInt] = red;
          Reds.Add(red);
          id++;
        }
      }
    }

    public void GetRequiredJumps_Moves()
    {
      JumpRequiredRed = false;
      JumpRequiredBlack = false;
      foreach (Piece red in Reds)
      {
        GameBoard[red.Row, red.ColInt].Jumps.Clear();
        GameBoard[red.Row, red.ColInt].Moves.Clear();
        if (CheckDirections(red))
        {
          JumpRequiredRed = true;
        }
      }

      foreach (Piece black in Blacks)
      {
        GameBoard[black.Row, black.ColInt].Jumps.Clear();
        GameBoard[black.Row, black.ColInt].Moves.Clear();
        if (CheckDirections(black))
        {
          JumpRequiredBlack = true;
        }
      }
    }

    public bool CheckDirections(Piece piecetocheck)
    {
      piecetocheck.Jumps.Clear();
      piecetocheck.Moves.Clear();
      if (piecetocheck.Color == 'R' || piecetocheck.Type == 'K')
      {
        foreach (int[] jump in JumpDirections)
        {
          try
          {
            if (piecetocheck.Compare(GameBoard[jump[0] + piecetocheck.Row, jump[1] + piecetocheck.ColInt], piecetocheck))
            {
              if (GameBoard[2 * jump[0] + piecetocheck.Row, 2 * jump[1] + piecetocheck.ColInt] == null)
              {
                GameBoard[piecetocheck.Row, piecetocheck.ColInt].Jumps.Add
                (new int[] { 2 * jump[0] + piecetocheck.Row, 2 * jump[1] + piecetocheck.ColInt });
                return true;
              }
            }
          }
          catch (IndexOutOfRangeException)
          {
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
          }
        }
      }

      if (piecetocheck.Color == 'B' || piecetocheck.Type == 'K')
      {
        foreach (int[] jump in JumpDirections)
        {
          try
          {
            if (piecetocheck.Compare(GameBoard[-1 * jump[0] + piecetocheck.Row, jump[1] + piecetocheck.ColInt], piecetocheck))
            {
              if (GameBoard[-2 * jump[0] + piecetocheck.Row, 2 * jump[1] + piecetocheck.ColInt] == null)
              {
                GameBoard[piecetocheck.Row, piecetocheck.ColInt].Jumps.Add
                (new int[] { -2 * jump[0] + piecetocheck.Row, 2 * jump[1] + piecetocheck.ColInt });
                return true;
              }
            }
          }
          catch (IndexOutOfRangeException)
          {
          }

          try
          {
            if (GameBoard[-1 * jump[0] + piecetocheck.Row, jump[1] + piecetocheck.ColInt] == null)
            {
              GameBoard[piecetocheck.Row, piecetocheck.ColInt].Moves.Add
                (new int[] { -1 * jump[0] + piecetocheck.Row, jump[1] + piecetocheck.ColInt });
            }
          }
          catch (IndexOutOfRangeException)
          {
          }
        }
      }
      return false;
    }

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
        return false;
      }
    }

    public bool Move()
    {
      Piece temp = GameBoard[Row, Col];
      temp.Row = TargetRow;
      temp.ColInt = TargetCol;
      if (temp.Row == 0 && temp.Color=='R')
      {
        temp.Type = 'K';
      }
      if (temp.Row == 7 && temp.Color == 'B')
      {
        temp.Type = 'K';
      }
      GameBoard[TargetRow, TargetCol] = temp;
      GameBoard[Row, Col] = null;
      
      return true;
    }

    public bool Jump()
    {
      Piece temp = GameBoard[Row, Col];
      temp.Row = TargetRow;
      temp.ColInt = TargetCol;
      if (temp.Row == 0 && temp.Color == 'R')
      {
        temp.Type = 'K';
      }
      if (temp.Row == 7 && temp.Color == 'B')
      {
        temp.Type = 'K';
      }
      GameBoard[TargetRow, TargetCol] = temp;
      GameBoard[Row, Col] = null;
      DeletePiece();
      GameBoard[(Row + TargetRow) / 2, (Col + TargetCol) / 2] = null;
      Row = TargetRow;
      Col = TargetCol;
      GetRequiredJumps_Moves();
      return (!GameBoard[TargetRow, TargetCol].Jumps.Any());
    }

    public void DeletePiece()
    {
      if (Player == "Red")
      {
        Remove = 0;
        foreach (Piece black in Blacks)
        {
          if (GameBoard[(Row + TargetRow) / 2, (Col + TargetCol) / 2].ID == black.ID)
          {
            break;
          }
          Remove++;
        }
        Blacks.RemoveAt(Remove);
      }
      else if (Player == "Black")
      {
        Remove = 0;
        foreach (Piece red in Reds)
        {
          if (GameBoard[(Row + TargetRow) / 2, (Col + TargetCol) / 2].ID == red.ID)
          {
            break;
          }
          Remove++;
        }
        Reds.RemoveAt(Remove);
      }
    }

    public bool IsTargetValid()
    {
      if (GameBoard[Row, Col].Moves.Any(p => p.SequenceEqual(new int[2] { TargetRow, TargetCol })))
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
      //required jumps True - False
      //does the piece have no jumps? True - False
      if (Player == "Red")
      {
        return JumpRequiredRed && !GameBoard[Row, Col].Jumps.Any();
      }
      else
      {
        return JumpRequiredBlack && !GameBoard[Row, Col].Jumps.Any();
      }
    }

    public bool MoveNotPossible()
    {
      //does the piece have no moves? True - False
      if (!GameBoard[Row, Col].Moves.Any())
      {
        if (GameBoard[Row, Col].Jumps.Any())
        {
          return false;
        }
        else
        {
          return true;
        }
      }
      else
      {
        return false;
      }
    }

    public bool CheckValidPiece()
    {
      if (Row < 0 || Row > 7 || Col < 0 || Col > 7 || GameBoard[Row, Col] == null)
      {
        return false;
      }

      if (GameBoard[Row, Col].Color == Player[0])
      {

        return true;
      }
      else
      {
        return false;
      }
    }

    public bool CheckGameOver()
    {
      return (!Reds.Any() || !Blacks.Any());
    }

    public void CheckWinner()
    {
      Player = !Reds.Any() ? "Black" : "Red";
    }
    //Check Jump
    /*
     * Take a (coordinate pair X 2) that checks the jump possibility
     * 
     */

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
