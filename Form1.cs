using static Card;
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
                pictureBox8.Visible = true;
                count++;
            }
            else if (count == 1)
            {
                pictureBox2.Visible = true;
                pictureBox7.Visible = true;
                count++;
            }
            else if (count == 2)
            {
                pictureBox3.Visible = true;
                pictureBox6.Visible = true;
                count++;
            }
            else if (count == 3)
            {
                pictureBox4.Visible = true;
                pictureBox5.Visible = true;
                count = 0;
            }


        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }
    }
}

class Card
{
    public
        enum Suit
    {
        club,
        diamond,
        heart,
        spade,

        max_suits
    };

    enum Rank
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
    };

    private
        Rank m_rank = 0;
        Suit m_suit = 0;
    public
        // default constructor in c#
        Card()
    {
    }

    Card(Rank rank, Suit suit)
    {
        m_rank = rank;
        m_suit = suit;
    }

    void print() 
    {
        switch (m_rank)
        {
        case rank_2:        textBox2.Text = '2';   break;
        case rank_3:        textBox2.Text = '3';   break;
        case rank_4:        textBox2.Text = '4';   break;
        case rank_5:        textBox2.Text = '5';   break;
        case rank_6:        textBox2.Text = '6';   break;
        case rank_7:        textBox2.Text = '7';   break;
        case rank_8:        textBox2.Text = '8';   break;
        case rank_9:        textBox2.Text = '9';   break;
        case rank_10:       textBox2.Text = 'T';   break;
        case rank_jack:     textBox2.Text = 'J';   break;
        case rank_queen:    textBox2.Text = 'Q';   break;
        case rank_king:     textBox2.Text < 'K';   break;
        case rank_ace:      textBox2.Text << 'A';   break;
        default:
            std::cout << '?';
            break;
        }
   
switch (m_suit)
{
    case club: std::cout << 'C'; break;
    case diamond: std::cout << 'D'; break;
    case heart: std::cout << 'H'; break;
    case spade: std::cout << 'S'; break;
    default:
        std::cout << '?';
        break;
}
    }

    int value() const
    {
        // should return value into textBox2 + place the corresponding image in textBox2 
        switch (m_rank)
        {
        case rank_2: return 2;
        case rank_3: return 3;
        case rank_4: return 4;
        case rank_5: return 5;
        case rank_6: return 6;
        case rank_7: return 7;
        case rank_8: return 8;
        case rank_9: return 9;
        case rank_10: return 10;
        case rank_jack: return 10;
        case rank_queen: return 10;
        case rank_king: return 10;
        case rank_ace: return 11;
    default:
            assert(false && "should never happen");
    return 0;
    }
        }
};
