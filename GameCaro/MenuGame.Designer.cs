namespace GameCaro
{
    partial class MenuGame
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MenuGame));
            this.lbPvsC = new System.Windows.Forms.Label();
            this.lbLan = new System.Windows.Forms.Label();
            this.lbInf = new System.Windows.Forms.Label();
            this.lbExit = new System.Windows.Forms.Label();
            this.lbPvsP = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // lbPvsC
            // 
            this.lbPvsC.Font = new System.Drawing.Font("Monotype Corsiva", 15.75F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbPvsC.Image = global::GameCaro.Properties.Resources.KhungTo;
            this.lbPvsC.Location = new System.Drawing.Point(44, 158);
            this.lbPvsC.Name = "lbPvsC";
            this.lbPvsC.Size = new System.Drawing.Size(231, 56);
            this.lbPvsC.TabIndex = 6;
            this.lbPvsC.Text = "Player vs Computer";
            this.lbPvsC.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lbPvsC.Click += new System.EventHandler(this.lbPvsC_Click);
            this.lbPvsC.MouseLeave += new System.EventHandler(this.lbPvsC_MouseLeave);
            this.lbPvsC.MouseHover += new System.EventHandler(this.lbPvsC_MouseHover);
            // 
            // lbLan
            // 
            this.lbLan.Font = new System.Drawing.Font("Monotype Corsiva", 15.75F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbLan.Image = global::GameCaro.Properties.Resources.KhungTo;
            this.lbLan.Location = new System.Drawing.Point(44, 214);
            this.lbLan.Name = "lbLan";
            this.lbLan.Size = new System.Drawing.Size(231, 62);
            this.lbLan.TabIndex = 7;
            this.lbLan.Text = "LAN";
            this.lbLan.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lbLan.Click += new System.EventHandler(this.lbLan_Click);
            this.lbLan.MouseLeave += new System.EventHandler(this.lbLan_MouseLeave);
            this.lbLan.MouseHover += new System.EventHandler(this.lbLan_MouseHover);
            // 
            // lbInf
            // 
            this.lbInf.Font = new System.Drawing.Font("Monotype Corsiva", 15.75F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbInf.Image = global::GameCaro.Properties.Resources.KhungTo;
            this.lbInf.Location = new System.Drawing.Point(44, 276);
            this.lbInf.Name = "lbInf";
            this.lbInf.Size = new System.Drawing.Size(231, 54);
            this.lbInf.TabIndex = 8;
            this.lbInf.Text = "About Us";
            this.lbInf.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lbInf.Click += new System.EventHandler(this.lbInf_Click);
            this.lbInf.MouseLeave += new System.EventHandler(this.lbInf_MouseLeave);
            this.lbInf.MouseHover += new System.EventHandler(this.lbInf_MouseHover);
            // 
            // lbExit
            // 
            this.lbExit.Font = new System.Drawing.Font("Monotype Corsiva", 15.75F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbExit.Image = global::GameCaro.Properties.Resources.KhungTo;
            this.lbExit.Location = new System.Drawing.Point(44, 330);
            this.lbExit.Name = "lbExit";
            this.lbExit.Size = new System.Drawing.Size(231, 57);
            this.lbExit.TabIndex = 9;
            this.lbExit.Text = "Exit";
            this.lbExit.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lbExit.Click += new System.EventHandler(this.lbExit_Click);
            this.lbExit.MouseLeave += new System.EventHandler(this.lbExit_MouseLeave);
            this.lbExit.MouseHover += new System.EventHandler(this.lbExit_MouseHover);
            // 
            // lbPvsP
            // 
            this.lbPvsP.Font = new System.Drawing.Font("Monotype Corsiva", 15.75F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbPvsP.Image = global::GameCaro.Properties.Resources.KhungTo;
            this.lbPvsP.Location = new System.Drawing.Point(41, 96);
            this.lbPvsP.Name = "lbPvsP";
            this.lbPvsP.Size = new System.Drawing.Size(234, 53);
            this.lbPvsP.TabIndex = 5;
            this.lbPvsP.Text = "Player vs Player";
            this.lbPvsP.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lbPvsP.Click += new System.EventHandler(this.lbPvsP_Click);
            this.lbPvsP.MouseLeave += new System.EventHandler(this.lbPvsP_MouseLeave);
            this.lbPvsP.MouseHover += new System.EventHandler(this.lbPvsP_MouseHover);
            // 
            // MenuGame
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.ClientSize = new System.Drawing.Size(316, 407);
            this.Controls.Add(this.lbExit);
            this.Controls.Add(this.lbInf);
            this.Controls.Add(this.lbLan);
            this.Controls.Add(this.lbPvsC);
            this.Controls.Add(this.lbPvsP);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "MenuGame";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "MenuGame";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MenuGame_FormClosing);
            this.Load += new System.EventHandler(this.MenuGame_Load);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Label lbPvsP;
        private System.Windows.Forms.Label lbPvsC;
        private System.Windows.Forms.Label lbLan;
        private System.Windows.Forms.Label lbInf;
        private System.Windows.Forms.Label lbExit;
    }
}