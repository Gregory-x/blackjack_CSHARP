namespace BlackJackV1
{
    partial class StartingScreen
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            button1 = new Button();
            button2 = new Button();
            button3 = new Button();
            label1 = new Label();
            SuspendLayout();
            // 
            // button1
            // 
            button1.Anchor = AnchorStyles.None;
            button1.AutoSize = true;
            button1.BackColor = Color.Green;
            button1.FlatStyle = FlatStyle.Popup;
            button1.Font = new Font("Lucida Fax", 14.25F, FontStyle.Bold, GraphicsUnit.Point);
            button1.ForeColor = Color.Black;
            button1.Location = new Point(343, 172);
            button1.Margin = new Padding(10);
            button1.Name = "button1";
            button1.Padding = new Padding(1);
            button1.RightToLeft = RightToLeft.No;
            button1.Size = new Size(100, 50);
            button1.TabIndex = 0;
            button1.Text = "PLAY";
            button1.UseVisualStyleBackColor = false;
            button1.Click += LoadGame;
            // 
            // button2
            // 
            button2.Anchor = AnchorStyles.None;
            button2.AutoSize = true;
            button2.BackColor = Color.FromArgb(192, 255, 192);
            button2.FlatStyle = FlatStyle.Popup;
            button2.Font = new Font("Lucida Fax", 14.25F, FontStyle.Bold, GraphicsUnit.Point);
            button2.ForeColor = Color.Black;
            button2.Location = new Point(343, 257);
            button2.Margin = new Padding(10);
            button2.Name = "button2";
            button2.Size = new Size(100, 50);
            button2.TabIndex = 1;
            button2.Text = "HELP";
            button2.UseVisualStyleBackColor = false;
            button2.Click += LoadHelp;
            // 
            // button3
            // 
            button3.Anchor = AnchorStyles.None;
            button3.AutoSize = true;
            button3.BackColor = Color.Red;
            button3.FlatStyle = FlatStyle.Popup;
            button3.Font = new Font("Lucida Fax", 14.25F, FontStyle.Bold, GraphicsUnit.Point);
            button3.ForeColor = Color.Black;
            button3.Location = new Point(343, 343);
            button3.Name = "button3";
            button3.Size = new Size(100, 50);
            button3.TabIndex = 2;
            button3.Text = "EXIT";
            button3.UseVisualStyleBackColor = false;
            button3.Click += LoadExit;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(395, 60);
            label1.Name = "label1";
            label1.Size = new Size(0, 15);
            label1.TabIndex = 3;
            // 
            // StartingScreen
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackgroundImage = Properties.Resources.gamescreenbg;
            BackgroundImageLayout = ImageLayout.Stretch;
            ClientSize = new Size(800, 450);
            Controls.Add(label1);
            Controls.Add(button3);
            Controls.Add(button2);
            Controls.Add(button1);
            Name = "StartingScreen";
            Text = "StartingScreen";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button button1;
        private Button button2;
        private Button button3;
        private Label label1;
    }
}