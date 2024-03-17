using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Security.Principal;
using System.Drawing.Design;
using System.Reflection.Emit;


namespace BlackJackV1
{
    public partial class StartingScreen : Form
    {
        System.Windows.Forms.Timer timer = new System.Windows.Forms.Timer();
        int index = 0;
        string welcomeText = "Welcome, ";
        string currentUser = Environment.UserName;
        string duh;

        public StartingScreen()
        {
            InitializeComponent();
            InitializeTimer();
            duh = welcomeText + currentUser;
            CustomizeLabel();
        }
        private void InitializeTimer()
        {
            timer.Interval = 450; // animation speed
            timer.Tick += Timer_Tick;
            timer.Start();
        }
        private void Timer_Tick(object sender, EventArgs e)
        {
            if (index < duh.Length)
            {
                label1.Text += duh[index];
                index++;
            }
            else
            {
                timer.Stop(); // Stop the timer after animation completes
            }
        }
        private void CustomizeLabel()
        {
            label1.Text = "";
            label1.BackColor = Color.Transparent;
            label1.ForeColor = Color.White; // Set text color
            label1.Font = new Font("Arial", 16, FontStyle.Bold); // Set font and size
            label1.TextAlign = ContentAlignment.MiddleCenter; // Center text
            label1.UseCompatibleTextRendering = true; // Ensure text rendering compatibility
            //label1.Location = new Point((this.ClientSize.Width - label1.Width) / 2, 0); // Position at the top
            //label1.TextRenderingHint = System.Drawing.Text.TextRenderingHint.ClearTypeGridFit; // Set text rendering hint
        }

        private void LoadGame(object sender, EventArgs e)
        {
            Form1 gameWindow = new Form1(); // create an instance of the form inside the funcitons
            gameWindow.Show();
        }

        private void LoadHelp(object sender, EventArgs e)
        {
            HelpScreen helpWindow = new HelpScreen();
            helpWindow.Show();
            //this.Close();
        }

        private void LoadExit(object sender, EventArgs e)
        {
            Environment.Exit(0);
        }

      
    }
}
