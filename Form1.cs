// To do:         // fnc to decide if textbox1 gets the text or textbox2 which is the dealer's score
                  // a way to implement pictures being shown in the Print() func and displaying current score in textBox1 -> player
                  // instead of MessageBox when bust or win display a before hidden textBox with the text BUST or WIN
                  // dynamic allocated pictures for the cards?
                  // animate the getting cards thingy
                  // PlayerWStand and PlayerWantsHit 
                  // Somehow player.score is wrong ALWAYS resulting in busts
                  // first runs everything before loading in the form like wtf not supposed to do that
                  // something is very wrong with the texts in the labels meaning there is a mistake on the way of getting there
                  // Main problems: automatically draws 2 cards and a 3rd one for player;
                  // doesn't wait for hit or stand it just plays it with 2 cards for the player aka it always stands not waiting for input
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace BlackJackV1
{
    
    public partial class Form1 : Form
    {
        // Maximum score before losing.
        const int g_maximumScore = 21;

        // Minimum score that the dealer has to have.
        const int g_minimumDealerScore = 17;

        const int initial_Number = 0;

        // default settings
        int count = 0;
        private bool? playerWantsHit = null;

        public Form1()
        { 
            InitializeComponent();
        }
        class Card
        {
            public enum Rank
            {
                Rank2,
                Rank3,
                Rank4,
                Rank5,
                Rank6,
                Rank7,
                Rank8,
                Rank9,
                Rank10,
                RankJack,
                RankQueen,
                RankKing,
                RankAce,

                MaxRanks
            }

            public enum Suit
            {
                Club,
                Diamond,
                Heart,
                Spade,

                MaxSuits
            }

            public Rank rank;
            public Suit suit;

            public Card(Rank rank, Suit suit)
            {
                this.rank = rank;
                this.suit = suit;
            }
            
            public void Print() // should be replaced by pictures 
            {
                switch (rank)
                {
                    case Rank.Rank2:
                        Console.Write("2");
                        break;
                    case Rank.Rank3:
                        Console.Write("3");
                        break;
                    case Rank.Rank4:
                        Console.Write("4");
                        break;
                    case Rank.Rank5:
                        Console.Write("5");
                        break;
                    case Rank.Rank6:
                        Console.Write("6");
                        break;
                    case Rank.Rank7:
                        Console.Write("7");
                        break;
                    case Rank.Rank8:
                        Console.Write("8");
                        break;
                    case Rank.Rank9:
                        Console.Write("9");
                        break;
                    case Rank.Rank10:
                        Console.Write("T");
                        break;
                    case Rank.RankJack:
                        Console.Write("J");
                        break;
                    case Rank.RankQueen:
                        Console.Write("Q");
                        break;
                    case Rank.RankKing:
                        Console.Write("K");
                        break;
                    case Rank.RankAce:
                        Console.Write("A");
                        break;
                    default:
                        Console.Write("?");
                        break;
                }

                switch (suit)
                {
                    case Suit.Club:
                        Console.Write("C");
                        break;
                    case Suit.Diamond:
                        Console.Write("D");
                        break;
                    case Suit.Heart:
                        Console.Write("H");
                        break;
                    case Suit.Spade:
                        Console.Write("S");
                        break;
                    default:
                        Console.Write("?");
                        break;
                }
            }
            public int Value() // textBox value static casted to String?
            {
                switch (rank)
                {
                    case Rank.Rank2:
                        return 2;
                    case Rank.Rank3:
                        return 3;
                    case Rank.Rank4:
                        return 4;
                    case Rank.Rank5:
                        return 5;
                    case Rank.Rank6:
                        return 6;
                    case Rank.Rank7:
                        return 7;
                    case Rank.Rank8:
                        return 8;
                    case Rank.Rank9:
                        return 9;
                    case Rank.Rank10:
                        return 10;
                    case Rank.RankJack:
                        return 10;
                    case Rank.RankQueen:
                        return 10;
                    case Rank.RankKing:
                        return 10;
                    case Rank.RankAce:
                        return 11;
                    default:
                        //Debug.Assert(false, "Shouldn't happen ever");
                        //assert(false && "should never happen");
                        return 0;
                }
            }
        }


        class Deck
        {

            private List<Card> m_deck = new List<Card>();
            private int m_cardIndex = 0;

            public Deck()
            {
                int index = 0;

                for (int suit = 0; suit < (int)Card.Suit.MaxSuits; ++suit)
                {
                    for (int rank = 0; rank < (int)Card.Rank.MaxRanks; ++rank)
                    {
                        m_deck.Add(new Card((Card.Rank)rank, (Card.Suit)suit));
                        ++index;
                    }
                }
            }

            public void Print()
            {
                foreach (var card in m_deck)
                {
                    card.Print();
                    Console.Write(' ');
                }

                Console.WriteLine();
            }

            public void Shuffle()
            {
                Random rand = new Random();
                for (int i = m_deck.Count - 1; i > 0; i--)
                {
                    int j = rand.Next(0, i + 1);
                    var temp = m_deck[i];
                    m_deck[i] = m_deck[j];
                    m_deck[j] = temp;
                }

                m_cardIndex = 0;
            }

            public Card DealCard()
            {
                if (m_cardIndex >= m_deck.Count)
                {
                    throw new InvalidOperationException("No more cards in the deck.");
                }

                return m_deck[m_cardIndex++];
            }
        }


        class Player
        {
            private int m_score;

            public Player()
            {
                m_score = 0;
            }

            public int DrawCard(Deck deck)
            {
                int value = deck.DealCard().Value();
                m_score += value;
                return value;
            }

            public int Score 
            {
                get { return m_score; }
            }

            public bool IsBust() // only bust if m_score > g_maximumScore is a true inequality
            {
                return (m_score > g_maximumScore);
            }
        }
        // end of classes and start of member and just functions

        bool PlayerWantsHit()
        {
            while (true)
            {
                //Takes care of the visuals (visible property of the cards)
                if (count == 1)
                {
                    pictureBox3.Visible = true;
                    //pictureBox6.Visible = true;
                    count++;
                }
                else if (count == 2)
                {
                    pictureBox4.Visible = true;
                    //pictureBox5.Visible = true;
                    count = 0;
                }
                    // Display the prompt message in the label

                    // Reset the playerWantsHit flag
                    playerWantsHit = null;

                    // Wait for the player to click one of the buttons
                    while (playerWantsHit == null)
                    {
                        Application.DoEvents();
                    }

                // Return the value of the playerWantsHit flag
                return playerWantsHit == true;
            }
        }
      
        bool PlayerTurn(Deck deck, Player player)
        {
            while (true)
            {
                if (player.IsBust()) // if true bust; returns wrong bool value IsBust() logic doesn't work
                {
                    MessageBox.Show("You busted. :( ");
                    return true;
                }
                else
                {
                    if (PlayerWantsHit()) // should not come in here until the user presses on the PlayerWantsHit function; player always wants hit cus it always returns true
                    {
                        var playerCard = player.DrawCard(deck); // using var instead of keyword auto as in C++
                        label1.Text = ($"You were dealt a {playerCard} and now have {player.Score}"); // predicts the next card

                    }
                    else
                    {
                        // No bust
                        return false;
                    }
                }
            }
        }
        bool DealerTurn(Deck deck, Player dealer)
        {
            while (dealer.Score < g_minimumDealerScore)
            {
                int dealerCard = dealer.DrawCard(deck);
                label2.Text = ("The dealer turned up a " + dealerCard + " and now has " + dealer.Score);
            }

            if (dealer.IsBust())
            {
                MessageBox.Show("The dealer busted!");
                return true;
            }
            return false;
        }

        bool PlayBlackjack(Deck deck)

        { 
            
            Player dealer = new Player();
            dealer.DrawCard(deck);
            pictureBox8.Visible = true;

            textBox2.Text = ("dealer: " + dealer.Score); // on second thought it's actually useful keep it

            Player player = new Player();
            player.DrawCard(deck); // should show the pictures when draws the card
            player.DrawCard(deck);
            textBox1.Text = ("player: " + player.Score); // 
            Thread.Sleep(500);
            if (PlayerTurn(deck, player))
            {
                return false;
            }

            if (DealerTurn(deck, dealer))
            {
                return true;
            }

            return (player.Score > dealer.Score);
        }
        //private void FormShown(object sender, EventArgs e)
        //{

        //}

        private void button2_MouseClick(object sender, MouseEventArgs e)
        {
            //PlayerWantsHit(); // doesn't do anything as it only returns a bool should call it with deck
            //number2 += 1;
            playerWantsHit = true;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            // doesn't do anything as it only returns a bool should call it with decks
            playerWantsHit = false;
        }

        private void FormLoaded(object sender, EventArgs e)
        {
            textBox1.Text = "";
            textBox2.Text = "";
            label1.Text = "";
            label2.Text = "";
            pictureBox1.Visible = true;
            pictureBox2.Visible = true;
           

            Deck deck = new Deck();
            deck.Shuffle();
            //await Task.Delay(2000);

            if (PlayBlackjack(deck)) // if true you win else you've lost
            {
                MessageBox.Show("You win!");
                // should loop the play Blackjack until the user presses on the back to main menu arrow
            }
            else
            {
                MessageBox.Show("You lose!");
                // should loop the play Blackjack until the user presses on the back to main menu arrow
            }

            // Equivalent of return 0; in C++
            //Environment.Exit(0);
        }
    }
}
