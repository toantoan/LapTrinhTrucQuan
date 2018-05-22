using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace GameCaro
{
    class BanCo
    {
        private int _SoDong;
        private int _SoCot;
        public BanCo()
        {
            _SoCot = 0;
            _SoDong = 0;
        }
        Color bgColor = Color.YellowGreen;
        public BanCo(int SoDong, int SoCot)
        {
            _SoDong = SoDong;
            _SoCot = SoCot;
        }
        Image imageO = new Bitmap(Properties.Resources.o);
        Image imageX = new Bitmap(Properties.Resources.x);

        public int SoCot
        {
            get
            {
                return _SoCot;
            }

        }

        public int SoDong
        {
            get
            {
                return _SoDong;
            }

        }

        public  void VeBanCo(Graphics gr)
        {
            Brush b = new SolidBrush(bgColor);
            gr.FillRectangle(b, 0, 0, 500, 500);
            Pen pen = new Pen(Color.Green);
            for (int i = 0; i <= SoCot; i++)
            {
                gr.DrawLine(pen, i * QuanCo._Width, 0, i * QuanCo._Width, _SoDong * QuanCo._Height);
            }
            for (int j = 0; j <= _SoDong; j++)
            {
                gr.DrawLine(pen, 0, j * QuanCo._Height, SoCot * QuanCo._Width, j * QuanCo._Height);
            }

            //gr.DrawImage(imageX, new Point(25, 25));
        }
        public void VeQuanCo(Graphics gr, Point point, Image image)
        {
            gr.DrawImage(image, point);
        }
    }
}
