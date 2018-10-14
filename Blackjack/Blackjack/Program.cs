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

  class DeckHand
  {
    //DeckHand covers both groups of cards whether it is the deck or the hand of a player
    public List<Card> Deck { get; set; }
    public int Score { get; set; } //Score gives the total value of a hand
    public bool Blackjack { get; set; } //Blackjack is a bool set to signify if the initial hand is a blackjack

    string[] Suit = new string[] { "Hearts", "Diamonds", "Clubs", "Spades" };
    string[] Rank = new string[] { "Two", "Three", "Four", "Five", "Six", "Seven", "Eight", "Nine", "Ten", "Jack", "Queen", "King", "Ace" };

    public DeckHand()
    {
      Deck = new List<Card>();
    }

    public void AddCard(DeckHand deck)
    {
      //draw card from the deck
      Card draw = new Card(deck.Deck[0].Suit, deck.Deck[0].Rank);
      deck.Deck.RemoveAt(0);
      Deck.Add(draw);
      ScoreHand();
    }

    public void ScoreHand()
    {
      //sort the hand for Aces and then calculate score
      List<Card> SortedTemp;
      Score = 0;
      SortedTemp = Deck.OrderBy(o => o.Point).ToList();
      foreach (Card k in SortedTemp)
      {
        //check to see if the Ace pushes over 21, if so make it worth one
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

    //Creating the deck
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

    //Shuffling the deck
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

    public void Print(string turn, int end = 0)
    {
      bool hide = true; //hide the first card of the dealer
      string possess;

      //Check the possession display for the player's turn
      if (char.ToLower(turn[turn.Length - 1]) == 's')
        possess = "'";
      else
        possess = "'s";

      Console.WriteLine(turn + possess + " Hand: ");
      foreach (Card k in Deck)
      {
        //hide the dealer's first card until the end
        //if end is anything but 0, show the card
        if (turn == "Dealer" && hide == true && end == 0)
          Console.WriteLine("********************");
        else
          Console.WriteLine(k.Rank + " of " + k.Suit + " ");
        hide = false;
      }
      Console.WriteLine();

      if (turn != "Dealer")
        Console.WriteLine(turn + " Total: " + Score + "\n");
    }

    public bool CheckBust()
    {
      ScoreHand();
      return Score > 21;
    }

    public void CheckBJ()
    {
      //check point values for a blackjack by a list
      //if the list is empty then whomever is being checked has a blackjack
      List<int> bj = new List<int> { 10, 11 };
      foreach (Card k in Deck)
      {
        if (k.Point == 11)
          bj.Remove(11);
        if (k.Point == 10)
          bj.Remove(10);
      }
      if (!bj.Any())
        Blackjack = true;
    }
  }

  class Program
  {
    static void Main(string[] args)
    {
      bool game = true;
      int result;

      DeckHand deck = new DeckHand(); //Deck
      DeckHand dealer = new DeckHand(); //Dealer's hand
      DeckHand player = new DeckHand(); //Player's hand
      ConsoleKeyInfo key_press;

      Console.WriteLine("---Blackjack---\n\n Press Spacebar to play");
      do
      {
        key_press = Console.ReadKey();
      } while (key_press.Key != ConsoleKey.Spacebar);

      Player play = new Player(); //player info

      while (game)
      {
        //Reset chips if player runs out of money or leave
        if (play.Chips <= 0)
        {
          do
          {
            Console.Clear();
            Console.WriteLine("Out of money. Reset balance?\nYes (Enter) Leave Table (ESC)");
            key_press = Console.ReadKey();
            if (key_press.Key == ConsoleKey.Enter)
              play.Chips = 100;
            if (key_press.Key == ConsoleKey.Escape)
              break;
          } while (key_press.Key != ConsoleKey.Enter && key_press.Key != ConsoleKey.Escape);
          if (key_press.Key == ConsoleKey.Escape)
            break;
        }

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
        player.CheckBJ();
        dealer.CheckBJ();

        //player's turn
        while (!player.CheckBust() && !player.Blackjack && !dealer.Blackjack)
        {
          Print(player, dealer, play);
          Console.WriteLine("\nSpace (Hit) Enter (Stay)");
          do
          {
            key_press = Console.ReadKey();
          } while (key_press.Key != ConsoleKey.Spacebar && key_press.Key != ConsoleKey.Enter);
          if (key_press.Key == ConsoleKey.Enter)
            break;
          else
            player.AddCard(deck);
        }

        //house's turn
        while (dealer.Score <= 15 && !player.CheckBust() && !player.Blackjack && !dealer.Blackjack)
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
              Print(player, dealer, play);
              Console.WriteLine("Dealer hits...");
              Thread.Sleep(2000);
            }

            if (dealer.CheckBust())
              break;
          }

        result = CheckWin(player.Score, dealer.Score);

        Print(player, dealer, play, 1);

        //check to see who wins and if anyone busted
        if (player.Blackjack)
          Console.WriteLine(play.Name + "has blackjack!");
        if (dealer.Blackjack)
          Console.WriteLine("Dealer has blackjack!");

        if (dealer.CheckBust())
          Console.WriteLine("Dealer busts!");
        if (player.CheckBust())
          Console.WriteLine(play.Name + " busts!");

        if ((player.Score == result) && (dealer.Score == result) || (player.Blackjack && dealer.Blackjack))
        {
          Console.WriteLine("Tie!");
        }
        else if ((player.Score == result || dealer.Score > 21) && player.Score <= 21 || (player.Blackjack))
        {
          Console.WriteLine(play.Name + " wins!");
          play.AdjustChip(play.Bet);
        }
        else
        {
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

    public static void Print(DeckHand p, DeckHand d, Player n, int end = 0)
    {
      Console.Clear();
      Console.WriteLine();
      p.Print(n.Name);
      d.Print("Dealer", end);
    }
  }
}
