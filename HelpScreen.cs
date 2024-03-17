using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Linq;
using System.Net;
using System.IO;
using System.Threading;
using System.Security.Principal;

namespace BlackJackV1
{
    public partial class HelpScreen : Form
    {
        public HelpScreen()
        {
            InitializeComponent();
        }

       
            private void linkLabel1_LinkClicked_1(object sender, LinkLabelLinkClickedEventArgs e)
            {
                try
                {
                    ProcessStartInfo psi = new ProcessStartInfo();
                    psi.UseShellExecute = true;
                    psi.FileName = "https://www.blackjack.org/blackjack/how-to-play/";
                    Process.Start(psi);
                }
                catch (Exception ex)
                {
                    // Handle the exception here, e.g. display an error message
                    MessageBox.Show("Error: " + ex.Message);
                }
            }

        

        private void button1_Click(object sender, EventArgs e)
        {
            //Form1 blackjack = new Form1();
            //blackjack.Show();
            this.Close();
            //this.Close();
        }

    }
}
