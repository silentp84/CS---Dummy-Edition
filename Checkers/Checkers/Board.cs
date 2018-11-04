using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Checkers
{
  class Board
  {
    // 0 empty 1 player 2 computer 3 player(king) 4 computer(king)
    public Dictionary<int, string> Piece = new Dictionary<int, string>() { { 0, "**" }, { 1, "PP" }, { 2, "CC" }, { 3, "PK" }, { 4, "CK" } };

    public int[,] GameBoard { get; set; }

    public Board()
    {
      GameBoard = new int[,] { { 2, 2, 2, 2 }, { 2, 2, 2, 2 }, { 2, 2, 2, 2 }, { 0, 0, 0, 0 }, { 0, 0, 0, 0 }, { 1, 1, 1, 1 }, { 1, 1, 1, 1 }, { 1, 1, 1, 1 } };
    }

    public void Reset()
    {
      GameBoard = new int[,] { { 2, 2, 2, 2 }, { 2, 2, 2, 2 }, { 2, 2, 2, 2 }, { 0, 0, 0, 0 }, { 0, 0, 0, 0 }, { 1, 1, 1, 1 }, { 1, 1, 1, 1 }, { 1, 1, 1, 1 } };
    }
  }
}
