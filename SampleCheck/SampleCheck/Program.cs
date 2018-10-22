using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Checkers
{
  class Checkers
  {
    const int X = 8;
    const int Y = 8;
    int switcher = 0;
    int num = 8;
    int xpos = 0;
    int ypos = 0;
    bool pick = false;
    char[,] Area = new char[X, Y];
    char[,] Area2 = new char[X, Y];
    char Symbol = (char)5;

    void Checkerboard()
    {
      Cursor();
      for (int i = 0; i < X; i++)
      {
        for (int y = 0; y < Y; y++)
        {
          if (switcher % 2 == 0)
            Area[i, y] = 'X';
          else
            Area[i, y] = 'Y';
          switcher++;
          Area[0, 0] = Symbol;
          Area[0, 2] = Symbol;
          Console.Write(Area[i, y]);
        }
        Console.WriteLine(num);
        num--;
        switcher++;
      }
      Console.WriteLine("ABCDEFGH");
      if (pick == false)
        Console.Write("Vyber figurku");
      if (pick == true)
        Console.Write("Vyber misto a uloz");
    }

    void Checkerboard2()
    {
      for (int i = 0; i < X; i++)
      {
        for (int y = 0; y < Y; y++)
        {
          if (switcher % 2 == 0)
            Area2[i, y] = 'X';
          else
            Area2[i, y] = 'Y';
          switcher++;
        }
        switcher++;
      }

    }

    void Cursor()
    {
      bool saveCursorVisibile;
      int saveCursorSize;

      Console.CursorVisible = true;
      saveCursorVisibile = Console.CursorVisible;
      saveCursorSize = Console.CursorSize;
      Console.CursorSize = 100;
    }


    void Keys()
    {
      while (true)
      {

        var ch = Console.ReadKey().Key;
        switch (ch)
        {

          case ConsoleKey.Enter:
            if (Area[xpos, ypos] == Symbol && pick == false)
            {
              pick = true;
            }
            if (Area[xpos, ypos] != Symbol && pick == true)
            {
              Area[xpos, ypos] = Symbol;
              pick = false;
            }
            break;


          case ConsoleKey.UpArrow:
            {
              if (ypos - 1 < 0)
                break;

              Console.SetCursorPosition(xpos, ypos);
              Console.Write(Area[xpos, ypos]);
              ypos--;
              break;
            }
          case ConsoleKey.DownArrow:
            {
              if (ypos + 1 == Y)
                break;

              Console.SetCursorPosition(xpos, ypos);
              Console.Write(Area[xpos, ypos]);
              ypos++;
              break;
            }
          case ConsoleKey.LeftArrow:
            {
              if (xpos - 1 < 0)
                break;

              Console.SetCursorPosition(xpos, ypos);
              Console.Write(Area[xpos, ypos]);
              xpos--;
              break;
            }
          case ConsoleKey.RightArrow:
            {
              if (xpos + 1 == X)
                break;

              Console.SetCursorPosition(xpos, ypos);
              Console.Write(Area[xpos, ypos]);
              xpos++;
              break;
            }
        }
        Console.SetCursorPosition(xpos, ypos);
        if (pick == true)
        {
          Area[xpos, ypos] = Area2[xpos, ypos];
          Console.Write(Symbol);
          Console.SetCursorPosition(xpos, ypos);
        }
      }
    }

    static void Main()
    {
      Console.Clear();
      Checkers check = new Checkers();
      check.Checkerboard();
      check.Checkerboard2();
      Console.SetCursorPosition(0, 0);
      check.Keys();
    }
  }
}
