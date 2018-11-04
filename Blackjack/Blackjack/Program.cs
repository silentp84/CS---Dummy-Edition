using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace Blackjack
{
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

    public void Print(string turn, int end = 0)
    {
      Console.Clear();
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
  }
}
