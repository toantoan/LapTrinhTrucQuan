using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Media;

namespace GameCaro
{
    public partial class MenuGame : Form
    {
        public MenuGame()
        {
            InitializeComponent();
        }
        System.Media.SoundPlayer sound = new SoundPlayer(@"C://Users\pc\Downloads\Documents\Visual Studio 2015\Projects\GameCaro\GameCaro\Resources\NhacNen.WAV");
        private void MenuGame_Load(object sender, EventArgs e)
        {
           
            sound.Play();
        }


        private void MenuGame_FormClosing(object sender, FormClosingEventArgs e)
        {
            DialogResult dr;
            dr = MessageBox.Show("Bạn có muốn thoát?", "Thông Báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dr == DialogResult.No)
            {
                e.Cancel = true;
            }
        }

        private void lbPvsP_Click(object sender, EventArgs e)
        {
            Form1 fr = new Form1();
            Properties.Settings.Default.PvsP = true;
            this.Hide();
            sound.Stop();
            fr.ShowDialog();
            this.Show();
            sound.Play();
        }

        private void lbPvsC_Click(object sender, EventArgs e)
        {
            Form1 fr = new Form1();
            Properties.Settings.Default.PvsC = true;
            this.Hide();
            sound.Stop();
            fr.ShowDialog();
            this.Show();
            sound.Play();
        }

        private void lbLan_Click(object sender, EventArgs e)
        {
            LAN fr = new LAN();
            Properties.Settings.Default.Lan = true;
            this.Hide();
            sound.Stop();
            fr.ShowDialog();
            this.Show();
            sound.Play();
        }

        private void lbInf_Click(object sender, EventArgs e)
        {
            About rl = new About();
            rl.Show();
        }

        private void lbExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void lbPvsP_MouseHover(object sender, EventArgs e)
        {
            lbPvsP.Image = Properties.Resources.To2;
        }

        private void lbPvsP_MouseLeave(object sender, EventArgs e)
        {
            lbPvsP.Image = Properties.Resources.KhungTo;
        }

        private void lbPvsC_MouseHover(object sender, EventArgs e)
        {
            lbPvsC.Image = Properties.Resources.To2;
        }

        private void lbPvsC_MouseLeave(object sender, EventArgs e)
        {
            lbPvsC.Image = Properties.Resources.KhungTo;
        }

        private void lbLan_MouseHover(object sender, EventArgs e)
        {
            lbLan.Image = Properties.Resources.To2;
        }

        private void lbLan_MouseLeave(object sender, EventArgs e)
        {
            lbLan.Image = Properties.Resources.KhungTo;
        }

        private void lbInf_MouseHover(object sender, EventArgs e)
        {
            lbInf.Image = Properties.Resources.To2;
        }

        private void lbInf_MouseLeave(object sender, EventArgs e)
        {
            lbInf.Image = Properties.Resources.KhungTo;
        }

        private void lbExit_MouseHover(object sender, EventArgs e)
        {
            lbExit.Image = Properties.Resources.To2;
        }

        private void lbExit_MouseLeave(object sender, EventArgs e)
        {
            lbExit.Image = Properties.Resources.KhungTo;
        }
    }
}
