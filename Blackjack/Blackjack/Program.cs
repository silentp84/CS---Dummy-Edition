﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blackjack
{
  class Card
  {
    Dictionary<string, int> Value = new Dictionary<string, int>() {
        { "Two",2 }, {"Three",3}, {"Four",4}, {"Five",5},
        { "Six",6}, {"Seven",7},{ "Eight",8 }, { "Nine",9 },
        { "Ten",10 }, {"Jack",10 }, {"Queen",10 }, {"King",10 },
        { "Ace", 11 } };

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

  class Player
  {
    public string Name { get; set; }
    public int Chips { get; set; }
    public int Bet { get; set; }

    public Player()
    {
      Console.Write("Enter a name: ");
      Name = Console.ReadLine();
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

  class DeckHand
  {
    public List<Card> Deck { get; set; }
    int Score { get; set; }

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

    public void Clear()
    {
      Deck.Clear();
    }

    public void Create()
    {
      Clear();
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
      bool hide = true;
      Console.WriteLine(turn + " Hand: ");
      foreach (Card k in Deck)
      {
        if (turn == "Dealer's" && hide == true)
          Console.WriteLine("********************");
        else
          Console.WriteLine(k.Rank + " of " + k.Suit + " " + k.Point);
        hide = false;
        total += k.Point;
      }
      Console.WriteLine(turn + " Total: " + total + "\n");
    }

    public void Print()
    {
      int count = 0;
      foreach(Card k in Deck)
      {
        Console.WriteLine(k.Rank + " of " + k.Suit);
        count++;
      }
      Console.WriteLine(count);
    }
  }

  class Program
  {
    static void Main(string[] args)
    {
      bool game = true;
      bool turn = true;

      DeckHand deck = new DeckHand();
      DeckHand dealer = new DeckHand();
      DeckHand player = new DeckHand();
      List<Card> SortedTemp;
      ConsoleKeyInfo key_press;

      Console.WriteLine("---Blackjack---\n\n Press Spacebar to play");
      do
      {
        key_press = Console.ReadKey();
      } while (key_press.Key != ConsoleKey.Spacebar);

      Player play = new Player();

      while (game)
      {
        do
        {
          Console.Clear();
          play.Print();
          Console.WriteLine("Make your bet (use the up and down arrow keys to adjust the bet)");
          Console.WriteLine("Current bet: " + play.Bet);
          Console.WriteLine("Press enter to play or esc to quit");
          key_press = Console.ReadKey();
          if (key_press.Key == ConsoleKey.UpArrow && play.Bet < play.Chips)
            play.Bet += 5;
          if (key_press.Key == ConsoleKey.DownArrow && play.Bet > 5)
            play.Bet -= 5;
        } while (key_press.Key != ConsoleKey.Enter && key_press.Key != ConsoleKey.Escape);

        if (key_press.Key == ConsoleKey.Escape)
          break;

        //initialize the deck and deal two cards to the dealer and player
        deck.Create();
        deck.Shuffle();

        player.Clear();
        dealer.Clear();
        player.AddCard(deck.DrawCard());
        dealer.AddCard(deck.DrawCard());
        player.AddCard(deck.DrawCard());
        dealer.AddCard(deck.DrawCard());

        SortedTemp = player.Deck.OrderBy(o => o.Point).ToList();
        player.Deck = SortedTemp;
        SortedTemp = dealer.Deck.OrderBy(o => o.Point).ToList();
        dealer.Deck = SortedTemp;

        player.Print(play.Name);
        dealer.Print("Dealer's");

        while (turn == true)
        {
          Console.Clear();
          player.Print(play.Name);
          dealer.Print("Dealer's");
          Console.WriteLine("Space (Hit) Enter (Stay)");
          do
          {
            key_press = Console.ReadKey();
          } while (key_press.Key != ConsoleKey.Spacebar && key_press.Key != ConsoleKey.Enter);
          if (key_press.Key == ConsoleKey.Enter)
            break;
          else
            player.AddCard(deck.DrawCard());
        }
        
      }
    }
    
    
  }
}
