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
    int Score { get; set; }
    Dictionary<string, int> value = new Dictionary<string, int>() {
        { "Two",2 }, {"Three",3}, {"Four",4}, {"Five",5},
        { "Six",6}, {"Seven",7},{ "Eight",8 }, { "Nine",9 },
        { "Ten",10 }, {"Jack",10 }, {"Queen",10 }, {"King",10 } };

    string[] Suit = new string[] { "Hearts", "Diamonds", "Clubs", "Spades" };
    string[] Rank = new string[] { "Two", "Three", "Four", "Five", "Six", "Seven", "Eight", "Nine", "Ten", "Jack", "Queen", "King", "Ace" };

    public DeckHand()
    {
      Deck = new List<Card>();
    }

    public void AddCard(Card card)
    {
      Deck.Add(card);
    }

    public Card DrawCard()
    {
      Card draw = new Card(Deck[0].Suit,Deck[0].Rank);
      Deck.RemoveAt(0);
      return draw;
    }

    public void Create()
    {
      foreach (string i in Suit)
      {
        foreach (string j in Rank)
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

    public void Print(string turn)
    {
      int total = 0;
      Console.WriteLine(turn + " Hand: ");
      foreach (Card k in Deck)
      {
        Console.WriteLine(k.Rank + " of " + k.Suit);
        total += value[k.Rank];
      }
      Console.WriteLine(turn + " Total: " + total + "\n");
    }

  }

  class Program
  {
    static void Main(string[] args)
    {
      bool game = true;

      DeckHand deck = new DeckHand();
      deck.Create();
      deck.Shuffle();

      DeckHand dealer = new DeckHand();
      DeckHand player = new DeckHand();

      player.AddCard(deck.DrawCard());
      dealer.AddCard(deck.DrawCard());
      player.AddCard(deck.DrawCard());
      dealer.AddCard(deck.DrawCard());

      player.Print("Player's");
      dealer.Print("Dealer's");



      while (game)
      {
        break;
      }
    }
    
    
  }
}
