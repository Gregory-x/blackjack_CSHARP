using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace BlackJackV1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }


        // default settings
        int count = 0;


        private void button2_MouseClick(object sender, MouseEventArgs e)
        {
            //Takes care of the visuals (visible property of the cards)
            if (count == 0)
            {
                pictureBox1.Visible = true;
                count++;
            }
            else if (count == 1)
            {
                pictureBox2.Visible = true;
                //pictureBox7.Visible = true;
                count++;
            }
            else if (count == 2)
            {
                pictureBox3.Visible = true;
                //pictureBox6.Visible = true;
                count++;
            }
            else if (count == 3)
            {
                pictureBox4.Visible = true;
                //pictureBox5.Visible = true;
                count = 0;
            }


        }

        private void button1_Click(object sender, EventArgs e)
        {
            textBox2.Text = "kutya";
        }

        private void button2_Click(object sender, EventArgs e)
        {

        }

        // Maximum score before losing.
        const int g_maximumScore = 21;

        // Minimum score that the dealer has to have.
        const int g_minimumDealerScore = 17;

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

            public void Print()
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
            public int Value()
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

                for (int suit = 0; suit < (int)Suit.MaxSuits; ++suit)
                {
                    for (int rank = 0; rank < (int)Rank.MaxRanks; ++rank)
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

            public bool IsBust()
            {
                return (m_score > g_maximumScore);
            }
        }
        // end of classes and start of member and just functions

        bool PlayerWantsHit()
        {
            while (true)
            {
                Console.Write("(h) to hit, or (s) to stand: ");
                char ch = Console.ReadKey().KeyChar;
                Console.WriteLine();

                switch (ch)
                {
                    case 'h':
                    case 'H':
                        return true;
                    case 's':
                    case 'S':
                        return false;
                }
            }
        }

        bool PlayerTurn(Deck deck, Player player)
        {
            while (true)
            {
                if (player.IsBust())
                {
                    Console.WriteLine("You busted. :( ");
                    return true;
                }
                else
                {
                    if (PlayerWantsHit())
                    {
                        Card playerCard = player.DrawCard(deck);
                        Console.WriteLine($"You were dealt a {playerCard} and now have {player.Score()}");

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
                Console.WriteLine("The dealer turned up a " + dealerCard + " and now has " + dealer.Score);
            }

            if (dealer.IsBust())
            {
                Console.WriteLine("The dealer busted!");
                return true;
            }
            return false;
        }

        bool PlayBlackjack(Deck deck)
        {
            Player dealer = new Player();
            dealer.DrawCard(deck);

            Console.WriteLine("The dealer is showing: " + dealer.Score);

            Player player = new Player();
            player.DrawCard(deck);
            player.DrawCard(deck);

            Console.WriteLine("You have: " + player.Score);

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

        class Program
        {
            static void Main()
            {
                Deck deck = new Deck();

                deck.Shuffle();

                if (PlayBlackjack(deck))
                {
                    Console.WriteLine("You win!");
                }
                else
                {
                    Console.WriteLine("You lose!");
                }
            }
        }
    }
}
