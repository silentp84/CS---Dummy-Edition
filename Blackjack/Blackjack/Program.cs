using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blackjack
{
  class Card
  {
    public string suit { get; set; }
    public string rank { get; set; }
    public Card(string s, string r)
    {
      suit = s;
      rank = r;
    }
  }

  class Program
  {
    static void Main(string[] args)
    {
      string[] suit = new string[] { "Hearts", "Diamonds", "Clubs", "Spades" };
      string[] rank = new string[] { "Two", "Three", "Four", "Five", "Six", "Seven", "Eight", "Nine", "Ten", "Jack", "Queen", "King" };
      Dictionary<string, int> value = new Dictionary<string, int>() {
        { "Two",2 }, {"Three",3}, {"Four",4}, {"Five",5},
        { "Six",6}, {"Seven",7},{ "Eight",8 }, { "Nine",9 },
        { "Ten",10 }, {"Jack",10 }, {"Queen",10 }, {"King",10 } };

      List<Card> deck = new List<Card>();
      foreach (string i in suit)
      {
        foreach (string j in rank)
        {
          deck.Add(new Card(i, j));
        }
      }

      shuffle(ref deck);

      foreach (Card k in deck)
      {
        Console.WriteLine(k.rank + " of " + k.suit);
      }
    }
    
    void shuffle(ref List<Card> blackj)
    {
      Random rng = new Random();
      int n = blackj.Count;
      while (n > 1)
      {
        n--;
        int k = rng.Next(n + 1);
        Card temp = blackj[k];
        blackj[k] = blackj[n];
        blackj[n] = temp;
      }
    }
  }
}
