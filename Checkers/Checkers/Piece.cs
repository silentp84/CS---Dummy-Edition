using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Checkers
{
  class Piece
  {
    // 0 empty 1 player 2 computer 3 player(king) 4 computer(king)
    private readonly Dictionary<int, string> PieceKey = new Dictionary<int, string>() { { 0, "**" }, { 1, "PP" }, { 2, "CC" }, { 3, "PK" }, { 4, "CK" } };
    private readonly Dictionary<int, char> ptranslate = new Dictionary<int, char>
                                      { { 0, 'A' }, { 1, 'B' }, { 2, 'C' }, { 3, 'D' }, { 4, 'E' }, { 5, 'F' }, { 6, 'G' }, { 7, 'H' } };

    public int locxInt { get; set; } //col location integer value
    public char locxChar { get; set; } //col location char value
    public int locy { get; set; } // row location integer value
    public string disp { get; set; } // the display of ptype
    public int ptype { get; set; } // piece type => see PieceKey dictionary

    public Piece(int x, int y, int pt)
    {
      locxInt = x;
      locxChar = ptranslate[x];
      locy = y;
      ptype = pt;
      disp = PieceKey[ptype];
    }
  }
}
