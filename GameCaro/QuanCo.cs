using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace GameCaro
{
    class QuanCo
    {
        public const int _Height = 25;
        public const int _Width = 25;
        private int _Row;
        private int _Column;
        private Point _ViTri;
        private int _SoHuu; //0 - chua danh, 
        public QuanCo()
        { }
        public QuanCo(int dong, int cot, Point vitri, int sohuu)
        {
            _Row = dong;
            _Column = cot;
            _ViTri = vitri;
            _SoHuu = sohuu;
        }
        public int Column
        {
            get
            {
                return _Column;
            }

            set
            {
                _Column = value;
            }
        }

        public int Row
        {
            get
            {
                return _Row;
            }

            set
            {
                _Row = value;
            }
        }

        public Point ViTri
        {
            get
            {
                return _ViTri;
            }

            set
            {
                _ViTri = value;
            }
        }

        public int SoHuu
        {
            get
            {
                return _SoHuu;
            }

            set
            {
                _SoHuu = value;
            }
        }
    }
}
