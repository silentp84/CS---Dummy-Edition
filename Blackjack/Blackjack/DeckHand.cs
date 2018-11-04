using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blackjack
{
  class DeckHand
  {
    //DeckHand covers both groups of cards whether it is the deck or the hand of a player
    public List<Card> Deck { get; set; }
    public int Score { get; set; } //Score gives the total value of a hand
    public bool Blackjack { get; set; } //Blackjack is a bool set to signify if the initial hand is a blackjack

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

    //Creating the deck
    public void Create()
    {
      Card x = new Card();
      foreach (string i in x.Suits)
      {
        foreach (string j in x.Ranks)
        {
          Deck.Add(new Card(i, j));
        }
      }
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
}
