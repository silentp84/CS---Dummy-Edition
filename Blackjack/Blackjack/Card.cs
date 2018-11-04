using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blackjack
{
  class Card
  {
<<<<<<< HEAD
    public string[] Suits = new string[] { "Hearts", "Diamonds", "Clubs", "Spades" };
    public string[] Ranks = new string[] { "Two", "Three", "Four", "Five", "Six", "Seven", "Eight", "Nine", "Ten", "Jack", "Queen", "King", "Ace" };

=======
>>>>>>> c999e53207808577442d8ddeea6d842b058620fd
    //Each card has an associated numeric value via Value
    Dictionary<string, int> Value = new Dictionary<string, int>() {
        { "Two",2 }, {"Three",3}, {"Four",4}, {"Five",5},
        { "Six",6}, {"Seven",7},{ "Eight",8 }, { "Nine",9 },
        { "Ten",10 }, {"Jack",10 }, {"Queen",10 }, {"King",10 },
        { "Ace", 11 } };

    //Each card has a Suit and Rank
    //Point value is set via Value
    public string Suit { get; set; }
    public string Rank { get; set; }
    public int Point { get; set; }

    public Card() { }
    public Card(string s, string r)
    {
      Suit = s;
      Rank = r;
      Point = Value[Rank];
    }
  }
}
