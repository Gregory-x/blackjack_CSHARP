// To do:           // instead of MessageBox when bust or win display a before hidden textBox with the text BUST or WIN : DONE
// Errors: displaying the wrong cards. : DONE
// works only for the first time after a change so we have to set something to 0 : DONE
// once winrate reaches 100% it remains 100% for some odd reason
// Animation + wait between dealing a second card for the dealer
// take ace as 1 or 10 logic to decide this and if player has blackjack dealer doesn't draw card
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
using System.Net.Security;
using static System.Windows.Forms.DataFormats;
using System.Reflection.Emit;
using System.Drawing.Text;

namespace BlackJackV1
{

    public partial class Form1 : Form
    {
        // default settings
        private int games_played = 0;
        private int player_wins = 1;
        private int player_losses = 1;
        private double winrate = 0;

        private int player_cardCount = 0;
        private int dealer_cardCount = 0;
        private bool? playerWantsHit = null;
        private bool stillPlaying = false;
        const int g_maximumScore = 21;        // Maximum score before losing
        const int g_minimumDealerScore = 17;
        const int initial_Number = 0;
        private DateTime _start;

        public Form1()
        {
            InitializeComponent();
            textBox1.Text = "";
            textBox2.Text = "";
            pictureBox8.Visible = true;
            pictureBox1.Visible = true;
            pictureBox2.Visible = true;
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
                Clubs,
                Diamonds,
                Hearts,
                Spades,

                MaxSuits
            }
            private Rank rank;
            private Suit suit;
            public Card()
            {
            }
            public Card(Rank rank, Suit suit)
            {
                this.rank = rank;
                this.suit = suit;
            }
            public string GetImagePath() // This version uses string interpolation to construct the file path based on the current suit and rank values. It first converts the suit and rank enums to lowercase strings, then combines them using string interpolation to create the file path. This approach removes the need for the large switch statement.
            {
                string suitName = suit.ToString().ToLower();
                string rankName = rank.ToString().ToLower();
                return $"../../../images/cards/{rankName}_of_{suitName}.png";
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
            public Card DealCard() // type card
            {
                if (m_cardIndex >= m_deck.Count)
                {
                    throw new InvalidOperationException("No more cards in the deck.");
                }
                Card dealtCard = m_deck[m_cardIndex]; // saves the card
                return m_deck[m_cardIndex++];
            }
        }
        class Player
        {
            private int m_score;
            private Card m_currentCard;
            // list which has all the players' cards
            public Player()
            {
                m_score = 0;
            }
            public int DrawCard(Deck deck)
            {
                m_currentCard = deck.DealCard();
                int value = m_currentCard.Value();
                m_score += value;
                return value;
            }
            public int Score
            {
                get { return m_score; }
            }
            public bool IsBust() // only bust if m_score > g_maximumScore is a true inequation
            {
                return (m_score > g_maximumScore);
            }
            public bool HasBlackjack()
            {
                if (m_score == g_maximumScore) return true;
                else return false;
            }
            public Card CurrentCard /// could be a problem here?
            {
                //Card dealtCard = m_deck[m_cardIndex]; // saves the card
                //return dealtCard;
                //return m_deck[m_cardIndex]; // saves the card
                get { return m_currentCard; }

            }
        }
        // end of classes and start of member and just functions
        bool PlayerWantsHit()
        {
            while (true)
            {
                playerWantsHit = null;                     // Reset the playerWantsHit flag
                // Wait for the player to click one of the buttons
                while (playerWantsHit == null)
                {
                    Application.DoEvents();
                }
                return playerWantsHit == true;                // Return the value of the playerWantsHit flag
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
                if (player.HasBlackjack())
                {
                    //goto PlayBlackjack(deck);
                    return false; // jmp to playerwin?
                }
                else
                {// problem here with showing the wrong score after 2 of the same cards FIXED
                    if (PlayerWantsHit()) // should not come in here until the user presses on the PlayerWantsHit function; player always wants hit cus it always returns true
                    {
                        player_cardCount++;
                        int playerCard = player.DrawCard(deck); // do not use var
                        switch (player_cardCount)
                        {
                            case 1:
                                {
                                    pictureBox3.Visible = true;
                                    string imagePath = player.CurrentCard.GetImagePath(); // not the current card, this just deals a random other card so the score and the displayed image of the card will not match
                                    pictureBox3.Image = Image.FromFile(imagePath);
                                    break;
                                }
                            case 2:
                                {
                                    pictureBox4.Visible = true;
                                    string imagePath = player.CurrentCard.GetImagePath();
                                    pictureBox4.Image = Image.FromFile(imagePath);
                                    break;
                                }
                        }
                        textBox1.Text = ($"player: {player.Score}"); // predicts the next card {playerCard
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
            if (dealer.IsBust())
            {
                MessageBox.Show("The dealer busted!");
                return true;
            }
            if (dealer.HasBlackjack())
            {
                return false;
            }
            while (dealer.Score < g_minimumDealerScore) // check whether this while cycle only runs once per game
            {
                dealer_cardCount++;
                int dealerCard = dealer.DrawCard(deck); // got to do with drawing the card itself(score) so also with GetImagePath()
                Thread.Sleep(200);
                switch (dealer_cardCount)
                {
                    case 1:
                        {
                            pictureBox7.Visible = true;
                            string imagePath = dealer.CurrentCard.GetImagePath();
                            pictureBox7.Image = Image.FromFile(imagePath);
                            break;
                        }
                    case 2:
                        {

                            pictureBox6.Visible = true;
                            string imagePath = dealer.CurrentCard.GetImagePath();
                            pictureBox6.Image = Image.FromFile(imagePath);
                            break;
                        }
                    case 3:
                        {
                            pictureBox5.Visible = true;
                            string imagePath = dealer.CurrentCard.GetImagePath();
                            pictureBox5.Image = Image.FromFile(imagePath);
                            break;
                        }
                }
                textBox2.Text = ("dealer: " + dealer.Score);
           
            }
             return false;
        }
        bool PlayBlackjack(Deck deck)
        {
            Player dealer = new Player();
            dealer.DrawCard(deck);
            string imagePath = dealer.CurrentCard.GetImagePath();
            pictureBox8.Image = Image.FromFile(imagePath);
            textBox2.Text = ("dealer: " + dealer.Score); // on second thought it's actually useful keep it
            Player player = new Player();
            player.DrawCard(deck); // should show the pictures when draws the card
            string imagePath2 = player.CurrentCard.GetImagePath();
            pictureBox1.Image = Image.FromFile(imagePath2);
            player.DrawCard(deck);
            string imagePath3 = player.CurrentCard.GetImagePath();
            pictureBox2.Image = Image.FromFile(imagePath3);
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
        private void timer1_Tick(object sender, EventArgs e)
        {
            TimeSpan duration = DateTime.Now - _start;
            label1.Text = "Time wasted: " + duration.ToString(@"hh\:mm\:ss");
        }
        private void button3_Click(object sender, EventArgs e)
        {
            stillPlaying = false;
            this.Close();
        }
        private void HitClicked(object sender, EventArgs e)
        {
            playerWantsHit = true;
        }
        private void StandClicked(object sender, EventArgs e)
        {
            playerWantsHit = false;
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            stillPlaying = true;
            _start = DateTime.Now;
            timer1.Start();
        }
        private void Winrate() // winrate somehow stays 100% after losing several times debug both values using labels
        {
            games_played++; 
            winrate = (double)((player_wins / games_played)) * 100;
            //if (winrate > 100) winrate = 100;
            label2.Text = "Winrate: " + winrate.ToString(@"0.##") + "%";
        }
        private void Form1_Shown(object sender, EventArgs e) // main
        {
            while (stillPlaying)
            {
                Deck deck = new Deck();
                deck.Shuffle();
                dealer_cardCount = 0;
                player_cardCount = 0;

                if (PlayBlackjack(deck)) // if true you win else you've lost
                {
                    textBox3.Visible = true;
                    textBox3.ForeColor = Color.Green;
                    textBox3.Text = "WIN";
                    MessageBox.Show("You win!"); // should loop the play Blackjack until the user presses on the back to main menu arrow
                    player_wins++;
                    //Thread.Sleep(100);
                }
                else
                {
                    textBox3.Visible = true;
                    textBox3.ForeColor = Color.Red;
                    textBox3.Text = "LOSS";
                    MessageBox.Show("You lose!"); // should loop the play Blackjack until the user presses on the back to main menu arrow
                    player_losses++;
                    //Thread.Sleep(100);
                }
                Winrate();
                textBox1.Text = "";
                textBox2.Text = "";
                textBox3.Text = "";
                textBox3.Visible = false;
                pictureBox7.Visible = false;
                pictureBox6.Visible = false;
                pictureBox5.Visible = false;
                pictureBox4.Visible = false;
                pictureBox3.Visible = false;
                Thread.Sleep(1000);
            }
        }

    }
}
