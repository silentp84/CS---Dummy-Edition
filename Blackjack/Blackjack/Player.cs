using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blackjack
{
  class Player
  {
    public string Name { get; set; }
    public int Chips { get; set; }
    public int Bet { get; set; }

    public Player()
    {
      do
      {
        Console.Write("Enter a name: ");
        Name = Console.ReadLine();
      } while (Name == "");
      Chips = 100;
      Bet = 5;
    }

    public void Print()
    {
      Console.WriteLine(Name + "\nChips: " + Chips);
    }

    public void AdjustChip(int bet)
    {
      Chips += bet;
    }
  }
}
