using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blackjack
{
  class Card
  {
    public string Suit { get; set; }
    public string Rank { get; set; }
    public Card(string s, string r)
    {
      Suit = s;
      Rank = r;
    }
  }

  class DeckHand
  {
    List<Card> Deck { get; set; }
    Dictionary<string, int> value = new Dictionary<string, int>() {
        { "Two",2 }, {"Three",3}, {"Four",4}, {"Five",5},
        { "Six",6}, {"Seven",7},{ "Eight",8 }, { "Nine",9 },
        { "Ten",10 }, {"Jack",10 }, {"Queen",10 }, {"King",10 } };

    string[] suit = new string[] { "Hearts", "Diamonds", "Clubs", "Spades" };
    string[] rank = new string[] { "Two", "Three", "Four", "Five", "Six", "Seven", "Eight", "Nine", "Ten", "Jack", "Queen", "King", "Ace" };

    public DeckHand()
    {

    }

    public void AddCard(string i, string j)
    {
      Deck.Add(new Card(i, j));
    }

    public Card DrawCard()
    {
      Card draw = new Card(Deck[0].Suit,Deck[0].Rank);
      Deck.RemoveAt(0);
      return draw;
    }

    public void Create()
    {
      foreach (string i in suit)
      {
        foreach (string j in rank)
        {
          Deck.Add(new Card(i, j));
        }
      }
    }

    public void Shuffle()
    {
      Random rng = new Random();
      int n = Deck.Count;
      while (n > 1)
      {
        n--;
        int k = rng.Next(n + 1);
        Card temp = Deck[k];
        Deck[k] = Deck[n];
        Deck[n] = temp;
      }
    }

    public void Print()
    {
      foreach (Card k in Deck)
      {
        Console.WriteLine(k.Rank + " of " + k.Suit);
      }
    }
  }

  class Program
  {
    static void Main(string[] args)
    {
      bool game = true;

      DeckHand deck = new DeckHand();
      deck.Create();
      //deck.Shuffle();
      deck.Print();

      DeckHand dealer = new DeckHand();
      DeckHand player = new DeckHand();



      while (game)
      {
        break;
      }
    }
    
    
  }
}
