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
    public partial class LAN : Form
    {
        private CaroChess caroChess;
        private Graphics gr;
        public LAN()
        {
            InitializeComponent();
            caroChess = new CaroChess();
            caroChess.KhoiTaoMangOCo();
            gr = pn.CreateGraphics();
        }

        private void pn_Paint(object sender, PaintEventArgs e)
        {
            caroChess.VeBanCo(gr);
            caroChess.VeLaiBanCo(gr);
        }
    }
}
