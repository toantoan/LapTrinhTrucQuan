using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GameCaro
{
    public partial class About : Form
    {
        public About()
        {
            InitializeComponent();
        }

        private void tm1_Tick(object sender, EventArgs e)
        {
            lbTxt.Location = new Point(lbTxt.Location.X, lbTxt.Location.Y - 1);
            if (lbTxt.Location.Y + lbTxt.Height < 0)
            {
                lbTxt.Location = new Point(lbTxt.Location.X, panel1.Height);
            
            }
        }

        private void About_Load(object sender, EventArgs e)
        {
            tm1.Enabled = true;
        }
    }
}
