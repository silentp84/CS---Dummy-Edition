using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

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
    public int Score { get; set; }

    string[] Suit = new string[] { "Hearts", "Diamonds", "Clubs", "Spades" };
    string[] Rank = new string[] { "Two", "Three", "Four", "Five", "Six", "Seven", "Eight", "Nine", "Ten", "Jack", "Queen", "King", "Ace" };

    public DeckHand()
    {
      Deck = new List<Card>();
    }

    public void AddCard(DeckHand deck)
    {
      Card draw = new Card(deck.Deck[0].Suit, deck.Deck[0].Rank);
      deck.Deck.RemoveAt(0);
      Deck.Add(draw);
      if (draw.Rank == "Ace")
      {
        if (Score >= 11)
          draw.Point = 1;
      }
      Score += draw.Point;
      ScoreHand();
    }

    public void ScoreHand()
    {
      List<Card> SortedTemp;
      Score = 0;
      SortedTemp = Deck.OrderBy(o => o.Point).ToList();
      foreach (Card k in Deck)
      {
        if (k.Point != 11)
          Score += k.Point;
        else
        {
          if (Score + 11 > 21)
            Score += 1;
          else
            Score += 11;
        }
      }
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
      bool hide = true;
      Console.WriteLine(turn + " Hand: ");
      foreach (Card k in Deck)
      {
        if (turn == "Dealer's" && hide == true)
          Console.WriteLine("********************");
        else
          Console.WriteLine(k.Rank + " of " + k.Suit + " " + k.Point);
        hide = false;
      }
      if (turn != "Dealer's")
        Console.WriteLine(turn + " Total: " + Score + "\n");
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

    public bool CheckBust(string name)
    {
      ConsoleKeyInfo key_press;
      if (Score > 21)
      {
        Console.Clear();
        Print(name);
        Console.WriteLine(name + " busts!");
        key_press = Console.ReadKey();
      }
      return (Score > 21);
    }
  }

  class Program
  {
    static void Main(string[] args)
    {
      bool game = true;
      int result;

      DeckHand deck = new DeckHand();
      DeckHand dealer = new DeckHand();
      DeckHand player = new DeckHand();
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
          player.Score = 0;
          dealer.Score = 0;

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
        player.AddCard(deck);
        dealer.AddCard(deck);
        player.AddCard(deck);
        dealer.AddCard(deck);

        while (true)
        {
          Console.Clear();
          player.Print(play.Name);
          dealer.Print("Dealer's");
          Console.WriteLine("\nSpace (Hit) Enter (Stay)");
          do
          {
            key_press = Console.ReadKey();
          } while (key_press.Key != ConsoleKey.Spacebar && key_press.Key != ConsoleKey.Enter);
          if (key_press.Key == ConsoleKey.Enter)
            break;
          else
            player.AddCard(deck);
          if (player.CheckBust(play.Name))
            break;
        }

        do
        {
          Console.Clear();
          player.Print(play.Name);
          dealer.Print("Dealer's");
          if (dealer.Score > 15)
          {
            Console.WriteLine("Dealer stays...");
            Thread.Sleep(2000);
            break;
          }
          else
          {
            dealer.AddCard(deck);
            Console.Clear();
            player.Print(play.Name);
            dealer.Print("Dealer's");
            Console.WriteLine("Dealer hits...");
            Thread.Sleep(2000);
          }

          if (dealer.CheckBust("Dealer"))
            break;
        } while (dealer.Score <= 15 && player.Score <= 21);

        result = CheckWin(player.Score, dealer.Score);
        if ((player.Score == result) && (dealer.Score == result))
        {
          Console.Clear();
          player.Print(play.Name);
          dealer.Print("Dealer");
          Console.WriteLine("Tie!");
        }
        else if ((player.Score == result || dealer.Score > 21) && player.Score <= 21)
        {
          Console.Clear();
          player.Print(play.Name);
          dealer.Print("Dealer");
          Console.WriteLine(play.Name + " wins!");
          play.AdjustChip(play.Bet);
        }
        else
        {
          Console.Clear();
          player.Print(play.Name);
          dealer.Print("Dealer");
          Console.WriteLine("Dealer wins!");
          play.AdjustChip(-1 * play.Bet);
        }
        key_press = Console.ReadKey();  
      }
    }
    
    public static int CheckWin(int p_score, int d_score)
    {
      return Math.Max(p_score, d_score);
    }
  }
}
