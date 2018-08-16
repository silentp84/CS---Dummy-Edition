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

      //Create the wolf and the rabbit

      Card wolf = new Card("wolf");
      Card rabbit = new Card("rabbit", 0);

      //Create the position on the board for the wolf and rabbit
      while (wolf.location == rabbit.location)
      {
        wolf.location = r.Next(0, 15);
        rabbit.location = r.Next(0, 15);
      }


      while (true)
      {
        String temp;
        //showboard where the wolf only can see
        BoardPrint(huntmap, animal, wolf.location, rabbit.location);
        //Ask for a direction for the animal to move
        if(animal=="W")  
          Move(animal, ref wolf, ref rabbit);
        else
          Move(animal, ref rabbit, ref wolf);
        //CheckWin(wolf.location, rabbit.location);
        /*Console.Write("Keep going?");
        temp = Console.ReadLine();
        if (temp == "0")
          break;*/
      }

      //Console.WriteLine(wolf.location);
      //Console.WriteLine(rabbit.location);




    }



    static void Move(String animal, ref Card pos_play, ref Card pos_comp)
    {
      Random rnd = new Random();
      String direction="";
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

      finish = false;

      while (finish == false)
      {
        compass = rnd.Next(4);
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

      Console.Clear();

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

    static bool CheckPosition(int position, int animal_position)
    {
      bool up = false, down = false, left = false, right = false;

      //check left
      if (position - 1 == animal_position && position!= 4 && position != 8 && position != 12)
        left = true;

      //check right
      if (position + 1 == animal_position && position != 3 && position != 7 && position != 11)
        right = true;


      //check above
      if (position - 4 == animal_position)
        up = true;

      //check below
      if (position + 4 == animal_position)
        down = true;

      return (bool)(up==true || down == true || left == true || right == true || position == animal_position);
    }


  }
}
