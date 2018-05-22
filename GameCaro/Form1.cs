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
using System.IO;

namespace GameCaro
{
    public partial class Form1 : Form
    {
        public static Cursor CreateCursor(Bitmap bm, Size size)
        {
            bm = new Bitmap(bm, size);
            return new Cursor(bm.GetHicon());
        }
        private CaroChess caroChess;
        private Graphics gr;
        public Form1()
        {
            InitializeComponent();
            caroChess = new CaroChess();
            caroChess.KhoiTaoMangOCo();
            gr = panel1.CreateGraphics();
            playerVsComToolStripMenuItem.Click += new EventHandler(button2_Click);
            undoToolStripMenuItem.Click += new EventHandler(button3_Click);
            exitToolStripMenuItem.Click += new EventHandler(button5_Click);
            btnUndo.Click += new EventHandler(button3_Click);
            btnRedo.Click += new EventHandler(redoToolStripMenuItem_Click);
            panel1.Cursor = CreateCursor((Bitmap)imageList1.Images[0], new Size(32, 32));
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {
            caroChess.VeBanCo(gr);
            caroChess.VeLaiBanCo(gr);
        }

        private void panel1_MouseClick(object sender, MouseEventArgs e)
        {
            if (!caroChess.SanSang1) return;
            if (caroChess.DanhCo(e.X, e.Y, gr))
            {
                if (caroChess.CheDoChoi1 == 1)
                {
                    if (caroChess.KtChienThang())
                        caroChess.KetThucGame();
                }
                else
                if (caroChess.CheDoChoi1 == 2)
                {
                    caroChess.KhoiDongCom(gr);
                    if (caroChess.KtChienThang())
                        caroChess.KetThucGame();
                }
            }
        }

        // Player vs Player
        private void button1_Click(object sender, EventArgs e)
        {
            gr.Clear(panel1.BackColor);
            caroChess.StarPlayervsPlayer(gr);
        }

        //Exit
        private void button5_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        // Undo
        private void button3_Click(object sender, EventArgs e)
        {
            gr.Clear(panel1.BackColor);
            if (caroChess.CheDoChoi1 == 1)
                caroChess.Undo(gr);
            else
            {
                caroChess.Undo(gr);
                caroChess.Undo(gr);
            }                
        }

        //new game
        private void button4_Click(object sender, EventArgs e)
        {
            if (Properties.Settings.Default.PvsP == true)
            {
                gr.Clear(panel1.BackColor);
                caroChess.StarPlayervsPlayer(gr);
            }
            if (Properties.Settings.Default.PvsC == true)
            {
                gr.Clear(panel1.BackColor);
                caroChess.StarPlayervsCom(gr);
            }
        }

        //MenuStrip Player vs Player
        private void playerVsPlayerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            gr.Clear(panel1.BackColor);
            caroChess.StarPlayervsPlayer(gr);
        }



    

        //MenuStrip Redo
        private void redoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (caroChess.CheDoChoi1 == 1)
                caroChess.Redo(gr);
            else
            {
                caroChess.Redo(gr);
                caroChess.Redo(gr);
            }
        }

        //Player vs Com
        private void button2_Click(object sender, EventArgs e)
        {
            gr.Clear(panel1.BackColor);
            caroChess.StarPlayervsCom(gr);
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            DialogResult dr;
            dr = MessageBox.Show("Bạn có muốn thoát?", "Thông Báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dr == DialogResult.No)
            {
                e.Cancel = true;
            }

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            if (Properties.Settings.Default.PvsP == true)
            {
                gr.Clear(panel1.BackColor);
                caroChess.StarPlayervsPlayer(gr);
            }
            if (Properties.Settings.Default.PvsC == true)
            {
                gr.Clear(panel1.BackColor);
                caroChess.StarPlayervsCom(gr);
            }
        }
    }
}
