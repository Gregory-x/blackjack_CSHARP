using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using static BlackJackV1.Card;
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
    }


    //const int g_minimumDealerScore = 17;

    class Card
    {
        public enum Suit
        {
            club,
            diamond,
            heart,
            spade,

            max_suits
        }

        public enum Rank
        {
            rank_2,
            rank_3,
            rank_4,
            rank_5,
            rank_6,
            rank_7,
            rank_8,
            rank_9,
            rank_10,
            rank_jack,
            rank_queen,
            rank_king,
            rank_ace,

            max_ranks
        }

        private Rank m_rank;
        private Suit m_suit;

        public Card()
        {
            m_rank = 0;
            m_suit = 0;
        }

        public Card(Rank rank, Suit suit)
        {
            m_rank = rank;
            m_suit = suit;
        }

        public void Print()
        {
            /*switch (m_rank)
            {
                case Rank.rank_2: Console.Write('2'); break;
                case Rank.rank_3: Console.Write('3'); break;
                case Rank.rank_4: Console.Write('4'); break;
                case Rank.rank_5: Console.Write('5'); break;
                case Rank.rank_6: Console.Write('6'); break;
                case Rank.rank_7: Console.Write('7'); break;
                case Rank.rank_8: Console.Write('8'); break;
                case Rank.rank_9: Console.Write('9'); break;
                case Rank.rank_10: Console.Write('T'); break;
                case Rank.rank_jack: Console.Write('J'); break;
                case Rank.rank_queen: Console.Write('Q'); break;
                case Rank.rank_king: Console.Write('K'); break;
                case Rank.rank_ace: Console.Write('A'); break;
                default:
                    Console.Write('?');
                    break;
            }

            switch (m_suit)
            {
                case Suit.club: Console.Write('C'); break;
                case Suit.diamond: Console.Write('D'); break;
                case Suit.heart: Console.Write('H'); break;
                case Suit.spade: Console.Write('S'); break;
                default:
                    Console.Write('?');
                    break;
            }*/
        }

        public int Value()
        {
            switch (m_rank)
            {
                case Rank.rank_2: return 2;
                case Rank.rank_3: return 3;
                case Rank.rank_4: return 4;
                case Rank.rank_5: return 5;
                case Rank.rank_6: return 6;
                case Rank.rank_7: return 7;
                case Rank.rank_8: return 8;
                case Rank.rank_9: return 9;
                case Rank.rank_10: return 10;
                case Rank.rank_jack: return 10;
                case Rank.rank_queen: return 10;
                case Rank.rank_king: return 10;
                case Rank.rank_ace: return 11;
                default:
                    throw new Exception("should never happen");
            }
        }
    }

    class Deck
    {
        private List<Card> m_deck;
        private int m_cardIndex = 0;

        public Deck()
        {
            m_deck = new List<Card>();

            for (int suit = 0; suit < Card.max_suits; ++suit)
            {
                for (int rank = 0; rank < Card.max_ranks; ++rank)
                {
                    m_deck.Add(new Card((Card.Rank)rank, (Card.Suit)suit));
                }
            }
        }

        public void Print()
        {
            foreach (Card card in m_deck)
            {
                card.Print();
                Console.Write(" ");
            }

            Console.WriteLine();
        }

        public void Shuffle()
        {
            Random rng = new Random();
            int n = m_deck.Count;
            while (n > 1)
            {
                n--;
                int k = rng.Next(n + 1);
                Card card = m_deck[k];
                m_deck[k] = m_deck[n];
                m_deck[n] = card;
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





}
