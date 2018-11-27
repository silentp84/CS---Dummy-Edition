﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Checkers
{
  class Piece
  {
    // 0 empty 1 player 2 computer 3 player(king) 4 computer(king)
    private readonly Dictionary<int, char> IntToChar = new Dictionary<int, char>
                                      { { 0, 'A' }, { 1, 'B' }, { 2, 'C' }, { 3, 'D' },
                                        { 4, 'E' }, { 5, 'F' }, { 6, 'G' }, { 7, 'H' } };
    private readonly Dictionary<char, int> CharToInt = new Dictionary<char, int>
                                      { { 'A', 0 }, { 'B', 1 }, { 'C', 2 }, { 'D', 3 },
                                        { 'E', 4 }, { 'F', 5 }, { 'G', 6 }, { 'H', 7 } };

    public int ColInt { get; set; } //col location integer value
    public char ColChar { get; set; } //col location char value
    public int Row { get; set; } // row location integer value
    public string PieceType { get; set; } // piece type => see PieceKey dictionary
    public int[] Coordinate {get; set;}

    public Piece(int row, int col, string pt)
    {
      ColInt = col;
      ColChar = IntToChar[col];
      Row = row;
      PieceType = pt;
      Coordinate = new int[] { row, col };
    }

    public Piece(int row, char col, string pt)
    {
      ColChar = col;
      ColInt = CharToInt[col];
      Row = row;
      PieceType = pt;
      Coordinate = new int[] { row, col };
    }

    public bool Compare(Piece other)
    {
      if(other == null)
      {
        return false;
      }
      return (other.PieceType[0] != this.PieceType[0]);
    }
  }
}