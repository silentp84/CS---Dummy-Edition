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
      int token;
      int[] huntmap = new int[] { 1, 3, 3, 3, 4, 4, 3, 4, 2, 3, 4, 3, 3, 4, 3, 3 }; //initialize board
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

      while(animal!="W" && animal != "R")
      {
        Console.Clear();
        Console.WriteLine("**** Wilderness ****");
        Console.WriteLine("Would you like to be the predator or prey?");
        Console.Write("Enter W for Wolf or R for Rabbit:  ");
        animal = Console.ReadLine();
      }
      
      Card wolf = new Card("wolf");
      Card rabbit = new Card("rabbit", 0);

      if (animal == "W")
      {
        while (true)
        {
          //showboard where the wolf only can see
          
          break;
        }
      }

      if (animal == "W")
        token = 1;
      else
        token = 2;

      BoardPrint(huntmap,token);


    }

    //Display the board and reinterpret the numbers in the array as symbol representations

    static void BoardPrint(int[] hmap, int token)
    {
      bool symbol;

      Dictionary<int, char> dict = new Dictionary<int, char>()
                                {
                                    {1,'W'},
                                    {2, 'R'},
                                    {3,'F'},
                                    {4,'_'}
                                };
      for (int i = 0; i < 16; i++)
      {
        if (i % 4 == 0 && i != 0)
          Console.Write("\n");
        symbol = CheckPosition(hmap, i, token);
        if (symbol == true)
        {
          Console.Write(string.Format("{0} ", dict[hmap[i]]));
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
        Console.Write(string.Format("{0} ", dict[hmap[i]]));
      }
      Console.Write("\n");
    }



    /*CheckPosition
     * Check to see if the token is within the cell's range and if it is in bounds of the array
     *If the token piece is "visible" return true so the display knows to display it correctly
     */

    static bool CheckPosition(int[] map, int position, int token)
    {
      bool up = false, down = false, left = false, right = false;

      //check left
      try
      {
        if (map[position - 1] == token && position!= 4 && position != 8 && position != 12)
          left = true;
      }
      catch
      {
      }

      //check right
      try
      {
        if (map[position + 1] == token && position != 3 && position != 7 && position != 11)
          right = true;
      }
      catch
      {
      }


      //check above
      try
      {
        if (map[position - 4] == token)
          up = true;
      }
      catch
      {
      }

      //check below
      try
      {
        if (map[position + 4] == token)
          down = true;
      }
      catch
      {
      }
      return (bool)(up==true || down == true || left == true || right == true || map[position]==token);
    }


  }
}
