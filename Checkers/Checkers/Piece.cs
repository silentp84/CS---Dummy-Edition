using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Checkers
{
  class Piece
  {
    private readonly Dictionary<int, char> IntToChar = new Dictionary<int, char>
                                      { { 0, 'A' }, { 1, 'B' }, { 2, 'C' }, { 3, 'D' },
                                        { 4, 'E' }, { 5, 'F' }, { 6, 'G' }, { 7, 'H' } };
    private readonly Dictionary<char, int> CharToInt = new Dictionary<char, int>
                                      { { 'A', 0 }, { 'B', 1 }, { 'C', 2 }, { 'D', 3 },
                                        { 'E', 4 }, { 'F', 5 }, { 'G', 6 }, { 'H', 7 } };

    public int ColInt { get; set; } //col location integer value
    public char ColChar { get; set; } //col location char value
    public int Row { get; set; } // row location integer value
    public char Color { get; set; } 
    public char Type { get; set; }
    public List<int[]> Jumps { get; set; }
    public List<int[]> Moves { get; set; }
    public int ID { get; set; }

    public Piece(int row, int col, char color, char type, int id = -1)
    {
      ColInt = col;
      ColChar = IntToChar[col];
      Row = row;
      Color = color;
      Type = type;
      Jumps = new List<int[]>();
      Moves = new List<int[]>();
      ID = id;
    }

    public Piece(int row, char col, char color, char type, int id = -1)
    {
      ColChar = col;
      ColInt = CharToInt[col];
      Row = row;
      Color = color;
      Type = type;
      ID = id;
    }

    public bool Compare(Piece other, Piece origin)
    {
      if(other == null)
      {
        return false;
      }
      return (other.Color != origin.Color);
    }
  }
}
