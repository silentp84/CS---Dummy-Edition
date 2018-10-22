using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cursor_Exercises
{
  class Program
  {
    static void Main(string[] args)
    {
      Console.CursorVisible = true;
      Console.CursorSize = 100;
      var x = 0;
      var y = 0;
      while (true)
      {
        ConsoleKeyInfo info= Console.ReadKey();
        if (info.Key == ConsoleKey.UpArrow && y!= 0)
          y -= 1;
        if (info.Key == ConsoleKey.DownArrow)
          y += 1;
        if (info.Key == ConsoleKey.LeftArrow && x != 0)
          x -= 1;
        if (info.Key == ConsoleKey.RightArrow && x < 79)
          x += 1;
        Console.SetCursorPosition(x, y);

      }
    }
  }
}
