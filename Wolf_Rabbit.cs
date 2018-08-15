using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp9
{
  class Program
  {
    public class Card
    {
      public String name;
      public static int rabbits;
      public int location;
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
      Random rnd = new Random();
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
      int[] huntmap = new int[] { 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 2, 2, 2, 2, 2 }; //initialize board
      Random r = new Random();      //randomize the board
      for (int i = huntmap.Length; i > 0; i--)
      {
        int j = r.Next(i);
        int k = huntmap[j];
        huntmap[j] = huntmap[i - 1];
        huntmap[i - 1] = k;
      }

      String animal = "";

      //Select a character either Wolf or Rabbit

      while (animal != "W" && animal != "R")
      {
        Console.Clear();
        Console.WriteLine("**** Wilderness ****");
        Console.WriteLine("Would you like to be the predator or prey?");
        Console.Write("Enter W for Wolf or R for Rabbit:  ");
        animal = Console.ReadLine();
      }

      Card wolf = new Card("wolf");
      Card rabbit = new Card("rabbit", 0);
      while (wolf.location == rabbit.location)
      {
        wolf.location = r.Next(0, 15);
        rabbit.location = r.Next(0, 15);
      }


      while (true)
      {
        //showboard where the wolf only can see
        BoardPrint(huntmap, animal, wolf.location, rabbit.location);
        //Ask for a direction for the animal to move
        //Move(wolf.location, rabbit.location);
        break;
      }

      //Console.WriteLine(wolf.location);
      //Console.WriteLine(rabbit.location);




    }

    /* ---BoardPrint---
     * Display the board and reinterpret the numbers in the array as symbol representations
     * hmap will show the layout of the map
     * beast signifies if they are a wolf or rabbit ("W" or "R")
     * w is the location of the wolf
     * r is the location of the rabbit
    */

    static void BoardPrint(int[] hmap, String beast, int w, int r)
    {
      bool symbol;
      int token_position;

      Dictionary<int, char> dict = new Dictionary<int, char>()
                                {
                                    {1,'F'},
                                    {2,'_'}
                                };
      for (int i = 0; i < 16; i++)
      {
        if (i % 4 == 0 && i != 0)
          Console.Write("\n");

        //figure out if player is the wolf or rabbit and give the location that needs to be revealed
        if (beast == "W")
          token_position = w;
        else
          token_position = r;

        symbol = CheckPosition(i, token_position);

        if (symbol == true)
        {
          if (i != w && i != r)
            Console.Write(string.Format("{0} ", dict[hmap[i]]));
          else if (i == w)
            Console.Write("W ");
          else if (i == r)
            Console.Write("R ");
        }
        else
        {
          Console.Write("* ");
        }

      }

      Console.Write("\n\n");

      for (int i = 0; i < 16; i++)
      {
        if (i % 4 == 0 && i != 0)
          Console.Write("\n");
        if (i != w && i != r)
          Console.Write(string.Format("{0} ", dict[hmap[i]]));
        else if (i == w)
          Console.Write("W ");
        else if (i == r)
          Console.Write("R ");
      }
    
      Console.Write("\n\n");


      for (int i = 0; i < 16; i++)
      {
        if (i % 4 == 0 && i != 0)
          Console.Write("\n");
        Console.Write(string.Format("{0} ", dict[hmap[i]]));
      }
      Console.Write("\n");
    }



    /* ---CheckPosition---
     * Check to see if the token is within the cell's range and if it is in bounds of the array
     *If the token piece is "visible" return true so the display knows to display it correctly
     */

    static bool CheckPosition(int position, int animal_position)
    {
      bool up = false, down = false, left = false, right = false;

      //check left
      try
      {
        if (position - 1 == animal_position && position!= 4 && position != 8 && position != 12)
          left = true;
      }
      catch
      {
      }

      //check right
      try
      {
        if (position + 1 == animal_position && position != 3 && position != 7 && position != 11)
          right = true;
      }
      catch
      {
      }


      //check above
      try
      {
        if (position - 4 == animal_position)
          up = true;
      }
      catch
      {
      }

      //check below
      try
      {
        if (position + 4 == animal_position)
          down = true;
      }
      catch
      {
      }
      return (bool)(up==true || down == true || left == true || right == true || position == animal_position);
    }


  }
}
