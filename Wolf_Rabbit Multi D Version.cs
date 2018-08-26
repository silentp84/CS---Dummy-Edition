using System;
using System.Collections.Generic;

namespace ConsoleApp9
{
  class Program
  {
    public class Card
    {
      public String name;
      public int rabbits;
      public int[] location = new int[] { 0, 0 };
      public Card(string tokenType)
      {
        name = tokenType;
      }

      public Card(string tokenType, int rInitial) : this(tokenType)
      {
        rabbits = 1;
      }

      public int Add_Rabbit()
      {
        return ++rabbits;
      }
    }

    static void Main(string[] args)
    {
      /*
       -build a game that at first is a 4x4 grid
       -a wolf will start somewhere, a rabbit in another
       -choose to be the rabbit or wolf
       -both the wolf and rabbit may only see a 1 block radius around them (maybe only the way they are facing? u-vision)
       -wolf can move two spaces, but if it moves two spaces it may only move once in a direction that is not backwards
       -rabbit can only move one space
       ***rabbit wins when there is no grass left
       ***wolf wins if it kills the rabbit
       */
      Random rnd = new Random();
      int[,] huntmap = new int[,] { { 1, 1, 1, 1 }, { 1, 1, 1, 1 }, { 1, 1, 1, 2 }, { 2, 2, 2, 2 } }; //initialize board
      bool game = true;

      //Create the wolf and the rabbit

      Card wolf = new Card("wolf");
      Card rabbit = new Card("rabbit", 0);

      randomize(ref huntmap, ref wolf, ref rabbit);

      String animal = "";
      String winner = "";

      //Select a character either Wolf or Rabbit

      while (animal != "W" && animal != "R")
      {
        Console.Clear();
        Console.WriteLine("**** Wilderness ****");
        Console.WriteLine("Would you like to be the predator or prey?");
        Console.Write("Enter W for Wolf or R for Rabbit:  ");
        animal = Console.ReadLine();
      }





      if (huntmap[rabbit.location[0], rabbit.location[1]] == 1)
      {
        huntmap[rabbit.location[0], rabbit.location[1]] = 2;
      }

      /*while (game == true)
      {
        //showboard where the wolf only can see
        BoardPrint(huntmap, animal, wolf, rabbit);
        //Ask for a direction for the animal to move
        if (animal == "W")
          Move(huntmap, animal, ref wolf, ref rabbit);
        else
        {
          Move(huntmap, animal, ref rabbit, ref wolf);
        }

        if (huntmap[rabbit.location] == 1)
        {
          huntmap[rabbit.location] = 2;
          rabbit.Add_Rabbit();
        }

        game = CheckWin(wolf.location, rabbit.location, rabbit.rabbits);
        Console.Write("Keep going?");
        temp = Console.ReadLine();
        if (temp == "0")
          break;
      }*/

      BoardPrint(huntmap, animal, wolf, rabbit);

      Console.Write("\n\n");
      /*if (wolf.location == rabbit.location)
      {
        Console.WriteLine("Nom Nom Nom, Bunny yummy...");
        winner = "W";
      }
      else if (rabbit.rabbits > 8)
      {
        Console.WriteLine("Wolfy can't eat us all");
        winner = "R";
      }

      if (animal == winner)
        Console.WriteLine("You Win!!");
      else
        Console.WriteLine("You LOSE...");

      Console.WriteLine("GAME OVER");
      //Console.WriteLine(wolf.location);
      //Console.WriteLine(rabbit.location);
      */



    }

    static void randomize(ref int[,] hmap, ref Card w, ref Card r)
    {
      // Get the dimensions.
      int num_rows = hmap.GetUpperBound(0) + 1;
      int num_cols = hmap.GetUpperBound(1) + 1;
      int num_cells = num_rows * num_cols;

      // Randomize the array.
      Random rand = new Random();
      for (int i = 0; i < num_cells - 1; i++)
      {
        // Pick a random cell between i and the end of the array.
        int j = rand.Next(i, num_cells);

        // Convert to row/column indexes.
        int row_i = i / num_cols;
        int col_i = i % num_cols;
        int row_j = j / num_cols;
        int col_j = j % num_cols;

        // Swap cells i and j.
        int temp = hmap[row_i, col_i];
        hmap[row_i, col_i] = hmap[row_j, col_j];
        hmap[row_j, col_j] = temp;
      }

      while (w.location == r.location)
      {
        w.location[0] = rand.Next(0, num_rows);
        w.location[1] = rand.Next(0, num_cols);
        r.location[0] = rand.Next(0, num_rows);
        r.location[1] = rand.Next(0, num_cols);
      }
    }

    static bool CheckWin(int locw, int locr, int rab)
    {
      if (locw == locr)
        return false;
      if (rab == 9)
        return false;
      else
        return true;
    }

    /*static void Move(int[] hmap, String animal, ref Card pos_play, ref Card pos_comp)
    {
      Random rnd = new Random();
      ConsoleKeyInfo key_press;
      //String direction="";
      int compass;
      bool finish = false;

      Console.Write("\n\nWhich direction would you like to move?\n Up (U), Down (D), Left (L), Right (R):  ");
      while (direction != "U" && direction != "D" && direction != "L" && direction != "R" || finish == false)
      {
        direction = Console.ReadLine();
        finish = true;
        if (direction == "U" && pos_play.location > 3)
          pos_play.location -= 4;
        else if (direction == "D" && pos_play.location < 12)
          pos_play.location += 4;
        else if (direction == "L" && pos_play.location != 0 && pos_play.location != 4 && pos_play.location != 8 && pos_play.location != 12)
          pos_play.location -= 1;
        else if (direction == "R" && pos_play.location != 3 && pos_play.location != 7 && pos_play.location != 11 && pos_play.location != 15)
          pos_play.location += 1;
        else
          finish = false;
      }

      Console.Write("\n\nUse arrow keys to move around.");
      do
      {
        key_press = Console.ReadKey();
        finish = true;
        if (key_press.Key == ConsoleKey.UpArrow && pos_play.location > 3)
          pos_play.location -= 4;
        else if (key_press.Key == ConsoleKey.DownArrow && pos_play.location < 12)
          pos_play.location += 4;
        else if (key_press.Key == ConsoleKey.LeftArrow && pos_play.location != 0 && pos_play.location != 4 && pos_play.location != 8 && pos_play.location != 12)
          pos_play.location -= 1;
        else if (key_press.Key == ConsoleKey.RightArrow && pos_play.location != 3 && pos_play.location != 7 && pos_play.location != 11 && pos_play.location != 15)
          pos_play.location += 1;
        else
          finish = false;
      } while (key_press.Key != ConsoleKey.UpArrow && key_press.Key != ConsoleKey.RightArrow && key_press.Key != ConsoleKey.LeftArrow && key_press.Key != ConsoleKey.DownArrow || finish == false);

      finish = false;

      while (finish == false)
      {
        compass = rnd.Next(1, 5);
        finish = true;
        if (compass == 1 && pos_comp.location > 3)
          pos_comp.location -= 4;
        else if (compass == 2 && pos_comp.location < 12)
          pos_comp.location += 4;
        else if (compass == 3 && pos_comp.location != 0 && pos_comp.location != 4 && pos_comp.location != 8 && pos_comp.location != 12)
          pos_comp.location -= 1;
        else if (compass == 4 && pos_comp.location != 3 && pos_comp.location != 7 && pos_comp.location != 11 && pos_comp.location != 15)
          pos_comp.location += 1;
        else
          finish = false;
      }
    } */

    /* ---BoardPrint---
     * Display the board and reinterpret the numbers in the array as symbol representations
     * hmap will show the layout of the map
     * beast signifies if they are a wolf or rabbit ("W" or "R")
     * w is the location of the wolf
     * r is the location of the rabbit
    */

    static void BoardPrint(int[,] hmap, String beast, Card w, Card r)
    {
      bool symbol = false;
      int num_rows = hmap.GetUpperBound(0) + 1;
      int num_cols = hmap.GetUpperBound(1) + 1;
      int num_cells = num_rows * num_cols;


      Dictionary<int, char> dict = new Dictionary<int, char>()
                                {
                                    {1,'F'},
                                    {2,'_'}
                                };

      Console.Clear();
      Console.Write("\n\n\n");

      for (int i = 0; i < num_rows; i++)
      {
        Console.WriteLine("");
        for (int j = 0; j < num_cols; j++)
        {
          //figure out if player is the wolf or rabbit and give the location that needs to be revealed
          if (beast == "W")
            symbol = CheckPosition(i, j, w, num_rows, num_cols);
          else if (beast == "R")
            symbol = CheckPosition(i, j, r, num_rows, num_cols);


          if (symbol == true)
          {
            if (i != w.location[0] && j!=w.location[1] && i != r.location[0] && j!=r.location[1])
              Console.Write(string.Format("{0} ", dict[hmap[i, j]]));
            if (i == w.location[0] && j == w.location[1])
            {
              if (beast == "W")
                Console.Write("W ");
              else if (beast == "R")
                Console.Write("F ");
            }
            if (i == r.location[0] && j == r.location[1])
              Console.Write("R ");
          }
          else
          {
            Console.Write("* ");
          }
        }
      }

      Console.Write("\n\n");

      for (int i = 0; i < num_rows; i++)
      {
        Console.WriteLine("");
        for (int j = 0; j < num_cols; j++)
        {
          if (i != w.location[0] && j != w.location[1] && i != r.location[0] && j != r.location[1])
            Console.Write(string.Format("{0} ", dict[hmap[i, j]]));
          else if (i == w.location[0] && j == w.location[1])
            Console.Write("W ");
          else if (i == w.location[0] && j == w.location[1])
            Console.Write("R ");
        }        
      }

      Console.Write("\n\n");

      /*      for (int i = 0; i < 16; i++)
            {
              if (i % 4 == 0 && i != 0)
                Console.Write("\n");
              Console.Write(string.Format("{0} ", dict[hmap[i]]));
            }
            Console.Write("\n"); */
    }



    /* ---CheckPosition---
     * Check to see if the token is within the cell's range and if it is in bounds of the array
     *If the token piece is "visible" return true so the display knows to display it correctly
     */

    static bool CheckPosition(int y, int x, Card a_loc, int rows, int cols)
    {
      bool up = false, down = false, left = false, right = false;

      //x is the column; y is the row

      //check left
      if (x != 0)
      {
        if (x - 1 == a_loc.location[0])
          left = true;
      }

      //check right
      if (x != cols - 1)
      {
        if (x + 1 == a_loc.location[0])
          right = true;
      }


      //check above
      if (y != 0)
      {
        if (y - 1 == a_loc.location[1])
          up = true;
      }

      //check below
      if (y != rows - 1)
      {
        if (y + 1 == a_loc.location[1])
          down = true;
      }

      return (bool)(up == true || down == true || left == true || right == true || (x == a_loc.location[0] && y == a_loc.location[1]));
    }


  }
}
