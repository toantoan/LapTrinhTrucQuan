using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Media;
using System.Windows.Forms;

namespace GameCaro
{
    public enum KETTHUC
    {
        Hoa,
        Player1,
        Player2,
        Computer,
        You
    }
    class CaroChess
    {
        private BanCo _BanCo;
        private QuanCo[,] MangOCo;
        private Stack<QuanCo> StackQuanCoDaDi;
        private Stack<QuanCo> StackQuanCoDaUndo;
        private int Luotdi;
        private int CheDoChoi;
        private KETTHUC _Ketthuc;
        public static SolidBrush color;
        public static Image ImageO;
        public static Image ImageX;
        private bool SanSang;

        public bool SanSang1
        {
            get
            {
                return SanSang;
            }

            set
            {
                SanSang = value;
            }
        }

        public int CheDoChoi1
        {
            get
            {
                return CheDoChoi;
            }
        }

        public CaroChess()
        {
            ImageX = new Bitmap(Properties.Resources.x);
            ImageO = new Bitmap(Properties.Resources.o);
            _BanCo = new BanCo(20, 20);
            MangOCo = new QuanCo[_BanCo.SoDong, _BanCo.SoCot];
            StackQuanCoDaDi = new Stack<QuanCo>();
            StackQuanCoDaUndo = new Stack<QuanCo>();
            Luotdi = 1;
            color = new SolidBrush(Color.YellowGreen);
        }
        public void VeBanCo(Graphics gr)
        {
            _BanCo.VeBanCo(gr);

        }
        public void KhoiTaoMangOCo()
        {
            for (int i = 0; i < _BanCo.SoDong; i++)
            {
                for (int j = 0; j < _BanCo.SoCot; j++)
                {
                    MangOCo[i, j] = new QuanCo(i, j, new Point(j * QuanCo._Width, i * QuanCo._Height), 0);
                }
            }
        }
        public bool DanhCo(int MouseX, int MouseY, Graphics gr)
        {
            if (MouseX % QuanCo._Width == 0 || MouseY % QuanCo._Height == 0) return false;
            int Cot = MouseX / QuanCo._Width;
            int Dong = MouseY / QuanCo._Height;
            if (MangOCo[Dong, Cot].SoHuu != 0) return false;
            System.Media.SoundPlayer sound = new SoundPlayer(@"C://Users\pc\Downloads\Documents\Visual Studio 2015\Projects\GameCaro\GameCaro\Resources\DanhCoSound.WAV");
            switch (Luotdi)
            {
                case 1: MangOCo[Dong, Cot].SoHuu = 1;
                    _BanCo.VeQuanCo(gr, MangOCo[Dong, Cot].ViTri, ImageO);
                    Luotdi = 2;
                    sound.Play();
                    break;
                case 2: MangOCo[Dong, Cot].SoHuu = 2;
                    _BanCo.VeQuanCo(gr, MangOCo[Dong, Cot].ViTri, ImageX);
                    Luotdi = 1;
                    sound.Play();
                    break;
            }
            QuanCo quanco = new QuanCo(MangOCo[Dong, Cot].Row, MangOCo[Dong, Cot].Column, MangOCo[Dong, Cot].ViTri, MangOCo[Dong, Cot].SoHuu);
            StackQuanCoDaDi.Push(quanco);
            return true;
        }
        public void VeLaiBanCo(Graphics gr)
        {
            foreach (QuanCo quanco in StackQuanCoDaDi)
            {
                if (quanco.SoHuu == 1)
                    _BanCo.VeQuanCo(gr, quanco.ViTri, ImageO);
                else
                    if (quanco.SoHuu == 2)
                    _BanCo.VeQuanCo(gr, quanco.ViTri, ImageX);
            }
        }

        //Player vs Player
        public void StarPlayervsPlayer(Graphics gr)
        {
            SanSang = true;
            StackQuanCoDaDi = new Stack<QuanCo>();
            StackQuanCoDaUndo = new Stack<QuanCo>();
            Luotdi = 1;
            CheDoChoi = 1;
            KhoiTaoMangOCo();
            VeBanCo(gr);
        }

        //Player vs Computer
        public void StarPlayervsCom(Graphics gr)
        {
            SanSang = true;
            StackQuanCoDaDi = new Stack<QuanCo>();
            StackQuanCoDaUndo = new Stack<QuanCo>();
            Luotdi = 1;
            CheDoChoi = 2;
            KhoiTaoMangOCo();
            VeBanCo(gr);
            KhoiDongCom(gr);
        }
        #region AI
        private long[] MangPhongNgu = new long[7] { 0, 4, 28, 256, 2308, 20776, 186988 };
        private long[] MangTanCong = new long[7] { 0, 1, 9, 85, 769, 6925, 62329 };
        public void KhoiDongCom(Graphics gr)
        {
            if (StackQuanCoDaDi.Count == 0)
            {
                DanhCo(_BanCo.SoCot / 2 * QuanCo._Width + 1, _BanCo.SoDong / 2 * QuanCo._Height + 1, gr);
            }
            else
            {
                QuanCo quanco = TimNuocDi();
                DanhCo(quanco.ViTri.X+1, quanco.ViTri.Y+1, gr);
            }
        }

        private QuanCo TimNuocDi()
        {
            QuanCo QuancoKq = new QuanCo();
            long DiemMax = 0;
            for (int i = 0; i < _BanCo.SoDong; i++)
            {
                for (int j = 0; j < _BanCo.SoCot; j++)
                {
                    if (MangOCo[i, j].SoHuu == 0)
                    {
                        long DiemTc = DiemTc_DuyetNgang(i,j) + DiemTc_DuyetDoc(i, j) + DiemTc_DuyetCheoXuong(i, j) + DiemTc_DuyetCheoLen(i, j);
                        long DiemPn = DiemPn_DuyetNgang(i, j) + DiemPn_DuyetDoc(i, j) + DiemPn_DuyetCheoXuong(i, j) + DiemPn_DuyetCheoLen(i, j);
                        long DiemTam = DiemTc > DiemPn ? DiemTc : DiemPn;
                        long DiemTong = (DiemPn + DiemTc) > DiemTam ? (DiemPn + DiemTc) : DiemTam;
                        if (DiemMax < DiemTong)
                        {
                            DiemMax = DiemTong;
                            QuancoKq = new QuanCo(MangOCo[i, j].Row, MangOCo[i, j].Column, MangOCo[i, j].ViTri, MangOCo[i, j].SoHuu);
                        }
                    }
                }
            }
            return QuancoKq;
        }
        #region TanCong
        public long DiemTc_DuyetDoc(int currRow, int currColumn)
        {
            long DiemTong = 0;
            int SoQuanTa = 0;
            int SoQuanTa2 = 0;
            int SoQuanDich = 0;
            int SoQuanDich2 = 0;
            for (int Count = 1; Count < 6 && currRow + Count < _BanCo.SoDong;  Count++)
            {
                if (MangOCo[currRow + Count, currColumn].SoHuu == 1)
                    SoQuanTa++;
                else
                    if (MangOCo[currRow + Count, currColumn].SoHuu == 2)
                {
                    SoQuanDich++;
                    break;
                }
                else
                {
                    for (int Count2 = 2; Count2 < 6 && currRow + Count2 < _BanCo.SoDong; Count2++)
                    {
                        if (MangOCo[currRow + Count2, currColumn].SoHuu == 1) SoQuanTa2++;
                        else
                            if (MangOCo[currRow + Count2, currColumn].SoHuu == 2)
                        {
                            SoQuanDich2++;
                            break;
                        }
                        else
                            break;
                    }
                    break;
                }
            }

            for (int Count = 1; Count < 6 && currRow - Count >= 0; Count++)
            {
                if (MangOCo[currRow - Count, currColumn].SoHuu == 1)
                    SoQuanTa++;
                else
                    if (MangOCo[currRow - Count, currColumn].SoHuu == 2)
                {
                    SoQuanDich++;
                    break;
                }
                else
                {
                    for (int Count2 = 2; Count < 6 && currRow - Count2 >= 0; Count2++)
                    {
                        if (MangOCo[currRow - Count2, currColumn].SoHuu == 1)
                            SoQuanTa2++;
                        else
                            if (MangOCo[currRow - Count2, currColumn].SoHuu == 2)
                        {
                            SoQuanDich2++;
                            break;
                        }
                        else
                        {

                            break;
                        }

                    }

                    break;
                }
                    
            }

            if (SoQuanDich == 2)
                return 0;
            if (SoQuanDich == 0)
                DiemTong += MangTanCong[SoQuanTa] * 2;
            else
                DiemTong += MangTanCong[SoQuanTa];
            if (SoQuanDich2 == 0)
                DiemTong += MangTanCong[SoQuanTa2] * 2;
            else
                DiemTong += MangTanCong[SoQuanTa2];
            if (SoQuanTa >= SoQuanTa2)
                DiemTong -= 1;
            else
                DiemTong -= 2;
            if (SoQuanTa == 4)
                DiemTong *= 2;
            if (SoQuanTa == 0)
                DiemTong += MangPhongNgu[SoQuanDich] * 2;
            else
                DiemTong += MangPhongNgu[SoQuanDich];
            if (SoQuanTa2 == 0)
                DiemTong += MangPhongNgu[SoQuanDich2] * 2;
            else
                DiemTong += MangPhongNgu[SoQuanDich2];
            return DiemTong;
        }
        public long DiemTc_DuyetNgang(int currRow, int currColumn)
        {
            long DiemTong = 0;
            int SoQuanTa = 0;
            int SoQuanTa2 = 0;
            int SoQuanDich = 0;
            int SoQuanDich2 = 0;
            for (int Count = 1; Count < 6 && currColumn + Count < _BanCo.SoCot; Count++)
            {
                if (MangOCo[currRow, currColumn + Count].SoHuu == 1)
                    SoQuanTa++;
                else
                    if (MangOCo[currRow , currColumn + Count].SoHuu == 2)
                {
                    SoQuanDich++;
                    break;
                }
                else
                {
                    for (int Count2 = 2; Count2 < 6 && currColumn + Count2 < _BanCo.SoCot; Count2++)
                    {
                        if (MangOCo[currRow, currColumn + Count2].SoHuu == 1) SoQuanTa2++;
                        else
                            if (MangOCo[currRow, currColumn + Count2].SoHuu == 2)
                        {
                            SoQuanDich2++;
                            break;
                        }
                        else
                            break;
                    }
                    break;
                }
            }

            for (int Count = 1; Count < 6 && currColumn - Count >= 0; Count++)
            {
                if (MangOCo[currRow, currColumn - Count].SoHuu == 1)
                    SoQuanTa++;
                else
                    if (MangOCo[currRow, currColumn - Count].SoHuu == 2)
                {
                    SoQuanDich++;
                    break;
                }
                else
                {
                    for (int Count2 = 2; Count < 6 && currColumn - Count2 >= 0; Count2++)
                    {
                        if (MangOCo[currRow, currColumn - Count2].SoHuu == 1)
                            SoQuanTa2++;
                        else
                            if (MangOCo[currRow , currColumn - Count2].SoHuu == 2)
                        {
                            SoQuanDich2++;
                            break;
                        }
                        else
                        {

                            break;
                        }

                    }

                    break;
                }

            }

            if (SoQuanDich == 2)
                return 0;
            if (SoQuanDich == 0)
                DiemTong += MangTanCong[SoQuanTa] * 2;
            else
                DiemTong += MangTanCong[SoQuanTa];
            if (SoQuanDich2 == 0)
                DiemTong += MangTanCong[SoQuanTa2] * 2;
            else
                DiemTong += MangTanCong[SoQuanTa2];
            if (SoQuanTa >= SoQuanTa2)
                DiemTong -= 1;
            else
                DiemTong -= 2;
            if (SoQuanTa == 4)
                DiemTong *= 2;
            if (SoQuanTa == 0)
                DiemTong += MangPhongNgu[SoQuanDich] * 2;
            else
                DiemTong += MangPhongNgu[SoQuanDich];
            if (SoQuanTa2 == 0)
                DiemTong += MangPhongNgu[SoQuanDich2] * 2;
            else
                DiemTong += MangPhongNgu[SoQuanDich2];
            return DiemTong;
        }

        public long DiemTc_DuyetCheoLen(int currRow, int currColumn)
        {
            long DiemTong = 0;
            int SoQuanTa = 0;
            int SoQuanTa2 = 0;
            int SoQuanDich = 0;
            int SoQuanDich2 = 0;
            for (int Count = 1; Count < 6 && currColumn + Count < _BanCo.SoCot && currRow - Count >= 0; Count++)
            {
                if (MangOCo[currRow - Count, currColumn + Count].SoHuu == 1)
                    SoQuanTa++;
                else
                    if (MangOCo[currRow - Count, currColumn + Count].SoHuu == 2)
                {
                    SoQuanDich++;
                    break;
                }
                else
                {
                    for (int Count2 = 2; Count2 < 6 && currColumn + Count2 < _BanCo.SoCot && currRow - Count2 >= 0; Count2++)
                    {
                        if (MangOCo[currRow -Count2, currColumn + Count2].SoHuu == 1) SoQuanTa2++;
                        else
                            if (MangOCo[currRow - Count2, currColumn + Count2].SoHuu == 2)
                        {
                            SoQuanDich2++;
                            break;
                        }
                        else
                            break;
                    }
                    break;
                }
            }

            for (int Count = 1; Count < 6 && currColumn - Count >= 0 && currRow + Count < _BanCo.SoDong; Count++)
            {
                if (MangOCo[currRow + Count, currColumn - Count].SoHuu == 1)
                    SoQuanTa++;
                else
                    if (MangOCo[currRow + Count, currColumn - Count].SoHuu == 2)
                {
                    SoQuanDich++;
                    break;
                }
                else
                {
                    for (int Count2 = 2; Count < 6 && currColumn - Count2 >= 0 && currRow + Count2 < _BanCo.SoDong; Count2++)
                    {
                        if (MangOCo[currRow + Count2, currColumn - Count2].SoHuu == 1)
                            SoQuanTa2++;
                        else
                            if (MangOCo[currRow + Count2, currColumn - Count2].SoHuu == 2)
                        {
                            SoQuanDich2++;
                            break;
                        }
                        else
                        {

                            break;
                        }

                    }

                    break;
                }

            }

            if (SoQuanDich == 2)
                return 0;
            if (SoQuanDich == 0)
                DiemTong += MangTanCong[SoQuanTa] * 2;
            else
                DiemTong += MangTanCong[SoQuanTa];
            if (SoQuanDich2 == 0)
                DiemTong += MangTanCong[SoQuanTa2] * 2;
            else
                DiemTong += MangTanCong[SoQuanTa2];
            if (SoQuanTa >= SoQuanTa2)
                DiemTong -= 1;
            else
                DiemTong -= 2;
            if (SoQuanTa == 4)
                DiemTong *= 2;
            if (SoQuanTa == 0)
                DiemTong += MangPhongNgu[SoQuanDich] * 2;
            else
                DiemTong += MangPhongNgu[SoQuanDich];
            if (SoQuanTa2 == 0)
                DiemTong += MangPhongNgu[SoQuanDich2] * 2;
            else
                DiemTong += MangPhongNgu[SoQuanDich2];
            return DiemTong;
        }

        public long DiemTc_DuyetCheoXuong(int currRow, int currColumn)
        {
            long DiemTong = 0;
            int SoQuanTa = 0;
            int SoQuanTa2 = 0;
            int SoQuanDich = 0;
            int SoQuanDich2 = 0;
            for (int Count = 1; Count < 6 && currColumn + Count < _BanCo.SoCot && currRow + Count < _BanCo.SoDong; Count++)
            {
                if (MangOCo[currRow + Count, currColumn + Count].SoHuu == 1)
                    SoQuanTa++;
                else
                    if (MangOCo[currRow + Count, currColumn + Count].SoHuu == 2)
                {
                    SoQuanDich++;
                    break;
                }
                else
                {
                    for (int Count2 = 2; Count2 < 6 && currColumn + Count2 < _BanCo.SoCot && currRow + Count2 < _BanCo.SoDong; Count2++)
                    {
                        if (MangOCo[currRow + Count2, currColumn + Count2].SoHuu == 1) SoQuanTa2++;
                        else
                            if (MangOCo[currRow + Count2, currColumn + Count2].SoHuu == 2)
                        {
                            SoQuanDich2++;
                            break;
                        }
                        else
                            break;
                    }
                    break;
                }
            }

            for (int Count = 1; Count < 6 && currColumn - Count >= 0 && currRow - Count >= 0; Count++)
            {
                if (MangOCo[currRow - Count, currColumn - Count].SoHuu == 1)
                    SoQuanTa++;
                else
                    if (MangOCo[currRow - Count, currColumn - Count].SoHuu == 2)
                {
                    SoQuanDich++;
                    break;
                }
                else
                {
                    for (int Count2 = 2; Count < 6 && currColumn - Count2 >= 0 && currRow - Count2 >= 0; Count2++)
                    {
                        if (MangOCo[currRow - Count2, currColumn - Count2].SoHuu == 1)
                            SoQuanTa2++;
                        else
                            if (MangOCo[currRow - Count2, currColumn - Count2].SoHuu == 2)
                        {
                            SoQuanDich2++;
                            break;
                        }
                        else
                        {

                            break;
                        }

                    }

                    break;
                }

            }

            if (SoQuanDich == 2)
                return 0;
            if (SoQuanDich == 0)
                DiemTong += MangTanCong[SoQuanTa] * 2;
            else
                DiemTong += MangTanCong[SoQuanTa];
            if (SoQuanDich2 == 0)
                DiemTong += MangTanCong[SoQuanTa2] * 2;
            else
                DiemTong += MangTanCong[SoQuanTa2];
            if (SoQuanTa >= SoQuanTa2)
                DiemTong -= 1;
            else
                DiemTong -= 2;
            if (SoQuanTa == 4)
                DiemTong *= 2;
            if (SoQuanTa == 0)
                DiemTong += MangPhongNgu[SoQuanDich] * 2;
            else
                DiemTong += MangPhongNgu[SoQuanDich];
            if (SoQuanTa2 == 0)
                DiemTong += MangPhongNgu[SoQuanDich2] * 2;
            else
                DiemTong += MangPhongNgu[SoQuanDich2];
            return DiemTong;
        }
        #endregion



        #region PhongNgu

        public long DiemPn_DuyetDoc(int currRow, int currColumn)
        {
            long DiemTong = 0;
            int SoQuanTa = 0;
            int SoQuanTa2 = 0;
            int SoQuanDich = 0;
            int SoQuanDich2 = 0;
            for (int Count = 1; Count < 6 && currRow + Count < _BanCo.SoDong; Count++)
            {
                if (MangOCo[currRow + Count, currColumn].SoHuu == 1)
                {
                    SoQuanTa++;
                    break;
                }
                   
                else
                    if (MangOCo[currRow + Count, currColumn].SoHuu == 2)
                {
                    SoQuanDich++;
       
                }
                else
                {
                    for (int Count2 = 2; Count2 < 6 && currRow + Count2 < _BanCo.SoDong; Count2++)
                    {
                        if (MangOCo[currRow + Count2, currColumn].SoHuu == 1)
                        {
                            SoQuanTa2++;
                            break;
                        }
                        else
                            if (MangOCo[currRow + Count2, currColumn].SoHuu == 2)
                        {
                            SoQuanDich2++;
                        }
                        else
                            break;
                    }
                    break;
                }
            }

            for (int Count = 1; Count < 6 && currRow - Count >= 0; Count++)
            {
                if (MangOCo[currRow - Count, currColumn].SoHuu == 1)
                {
                    SoQuanTa++;
                    break;
                }  
                else
                    if (MangOCo[currRow - Count, currColumn].SoHuu == 2)
                {
                    SoQuanDich++;
                }
                else
                {
                    for (int Count2 = 2; Count < 6 && currRow - Count2 >= 0; Count2++)
                    {
                        if (MangOCo[currRow - Count2, currColumn].SoHuu == 1)
                        {
                            SoQuanTa2++;
                            break;
                        }   
                        else
                            if (MangOCo[currRow - Count2, currColumn].SoHuu == 2)
                        {
                            SoQuanDich2++;
                        }
                        else
                        {

                            break;
                        }

                    }

                    break;
                }

            }

            if (SoQuanTa == 2)
                return 0;
            if (SoQuanTa == 0)
                DiemTong += MangPhongNgu[SoQuanDich] * 2;
            else
                DiemTong += MangPhongNgu[SoQuanDich];

            if (SoQuanTa2 == 0)
                DiemTong += MangPhongNgu[SoQuanTa2] * 2;
            else
                DiemTong += MangPhongNgu[SoQuanTa2];

          
            if (SoQuanDich >= SoQuanDich2)
                DiemTong -= 1;
            else
                DiemTong -= 2;
            if (SoQuanDich == 4)
                DiemTong *= 2;
            return DiemTong;
        }

        public long DiemPn_DuyetNgang(int currRow, int currColumn)
        {
            long DiemTong = 0;
            int SoQuanTa = 0;
            int SoQuanTa2 = 0;
            int SoQuanDich = 0;
            int SoQuanDich2 = 0;
            for (int Count = 1; Count < 6 && currColumn + Count < _BanCo.SoCot; Count++)
            {
                if (MangOCo[currRow, currColumn + Count].SoHuu == 1)
                {
                    SoQuanTa++;
                    break;
                }

                else
                    if (MangOCo[currRow, currColumn + Count].SoHuu == 2)
                {
                    SoQuanDich++;
                }
                else
                {
                    for (int Count2 = 2; Count2 < 6 && currColumn + Count2 < _BanCo.SoCot; Count2++)
                    {
                        if (MangOCo[currRow, currColumn + Count2].SoHuu == 1)
                        {
                            SoQuanTa2++;
                            break;
                        }
                        else
                            if (MangOCo[currRow, currColumn + Count2].SoHuu == 2)
                        {
                            SoQuanDich2++;
                        }
                        else
                            break;
                    }
                    break;
                }
            }

            for (int Count = 1; Count < 6 && currColumn - Count >= 0; Count++)
            {
                if (MangOCo[currRow, currColumn - Count].SoHuu == 1)
                {
                    SoQuanTa++;
                    break;
                }

                else
                    if (MangOCo[currRow, currColumn - Count].SoHuu == 2)
                {
                    SoQuanDich++;
                }
                else
                {
                    for (int Count2 = 2; Count < 6 && currColumn - Count2 >= 0; Count2++)
                    {
                        if (MangOCo[currRow, currColumn - Count2].SoHuu == 1)
                        {
                            SoQuanTa2++;
                            break;
                        }
                      
                        else
                            if (MangOCo[currRow, currColumn - Count2].SoHuu == 2)
                        {
                            SoQuanDich2++;
                        }
                        else
                        {

                            break;
                        }

                    }

                    break;
                }

            }
            if (SoQuanTa == 2)
                return 0;
            if (SoQuanTa == 0)
                DiemTong += MangPhongNgu[SoQuanDich] * 2;
            else
                DiemTong += MangPhongNgu[SoQuanDich];

            if (SoQuanTa2 == 0)
                DiemTong += MangPhongNgu[SoQuanTa2] * 2;
            else
                DiemTong += MangPhongNgu[SoQuanTa2];

            if (SoQuanDich >= SoQuanDich2)
                DiemTong -= 1;
            else
                DiemTong -= 2;
            if (SoQuanDich == 4)
                DiemTong *= 2;
            return DiemTong;
        }
        public long DiemPn_DuyetCheoLen(int currRow, int currColumn)
        {
            long DiemTong = 0;
            int SoQuanTa = 0;
            int SoQuanTa2 = 0;
            int SoQuanDich = 0;
            int SoQuanDich2 = 0;
            for (int Count = 1; Count < 6 && currColumn + Count < _BanCo.SoCot && currRow - Count >= 0; Count++)
            {
                if (MangOCo[currRow - Count, currColumn + Count].SoHuu == 1)
                {
                    SoQuanTa++;
                    break;
                }
                   
                else
                    if (MangOCo[currRow - Count, currColumn + Count].SoHuu == 2)
                {
                    SoQuanDich++;
                }
                else
                {
                    for (int Count2 = 2; Count2 < 6 && currColumn + Count2 < _BanCo.SoCot && currRow - Count2 >= 0; Count2++)
                    {
                        if (MangOCo[currRow - Count2, currColumn + Count2].SoHuu == 1)
                        {
                            SoQuanTa2++;
                            break;
                        }
                        else
                            if (MangOCo[currRow - Count2, currColumn + Count2].SoHuu == 2)
                        {
                            SoQuanDich2++;
                        }
                        else
                            break;
                    }
                    break;
                }
            }

            for (int Count = 1; Count < 6 && currColumn - Count >= 0 && currRow + Count < _BanCo.SoDong; Count++)
            {
                if (MangOCo[currRow + Count, currColumn - Count].SoHuu == 1)
                {
                    SoQuanTa++;
                    break;
                }
                    
                else
                    if (MangOCo[currRow + Count, currColumn - Count].SoHuu == 2)
                {
                    SoQuanDich++;
                }
                else
                {
                    for (int Count2 = 2; Count < 6 && currColumn - Count2 >= 0 && currRow + Count2 < _BanCo.SoDong; Count2++)
                    {
                        if (MangOCo[currRow + Count2, currColumn - Count2].SoHuu == 1)
                        {
                            SoQuanTa2++;
                            break;
                        }
                        else
                            if (MangOCo[currRow + Count2, currColumn - Count2].SoHuu == 2)
                        {
                            SoQuanDich2++;
                        }
                        else
                        {

                            break;
                        }

                    }

                    break;
                }

            }

            if (SoQuanTa == 2)
                return 0;
            if (SoQuanTa == 0)
                DiemTong += MangPhongNgu[SoQuanDich] * 2;
            else
                DiemTong += MangPhongNgu[SoQuanDich];


            if (SoQuanTa2 == 0)
                DiemTong += MangPhongNgu[SoQuanTa2] * 2;
            else
                DiemTong += MangPhongNgu[SoQuanTa2];


            if (SoQuanDich >= SoQuanDich2)
                DiemTong -= 1;
            else
                DiemTong -= 2;
            if (SoQuanDich == 4)
                DiemTong *= 2;
            return DiemTong;
        }

        public long DiemPn_DuyetCheoXuong(int currRow, int currColumn)
        {
            long DiemTong = 0;
            int SoQuanTa = 0;
            int SoQuanTa2 = 0;
            int SoQuanDich = 0;
            int SoQuanDich2 = 0;
            for (int Count = 1; Count < 6 && currColumn + Count < _BanCo.SoCot && currRow + Count < _BanCo.SoDong; Count++)
            {
                if (MangOCo[currRow + Count, currColumn + Count].SoHuu == 1)
                {
                    SoQuanTa++;
                    break;
                }
                   
                else
                    if (MangOCo[currRow + Count, currColumn + Count].SoHuu == 2)
                {
                    SoQuanDich++;
                }
                else
                {
                    for (int Count2 = 2; Count2 < 6 && currColumn + Count2 < _BanCo.SoCot && currRow + Count2 < _BanCo.SoDong; Count2++)
                    {
                        if (MangOCo[currRow + Count2, currColumn + Count2].SoHuu == 1)
                        {
                            SoQuanTa2++;
                            break;
                        }
                        else
                            if (MangOCo[currRow + Count2, currColumn + Count2].SoHuu == 2)
                        {
                            SoQuanDich2++;
                        }
                        else
                            break;
                    }
                    break;
                }
            }

            for (int Count = 1; Count < 6 && currColumn - Count >= 0 && currRow - Count >= 0; Count++)
            {
                if (MangOCo[currRow - Count, currColumn - Count].SoHuu == 1)
                {
                    SoQuanTa++;
                    break;
                }

                else
                    if (MangOCo[currRow - Count, currColumn - Count].SoHuu == 2)
                {
                    SoQuanDich++;
                }
                else
                {
                    for (int Count2 = 2; Count < 6 && currColumn - Count2 >= 0 && currRow - Count2 >= 0; Count2++)
                    {
                        if (MangOCo[currRow - Count2, currColumn - Count2].SoHuu == 1)
                        {
                            SoQuanTa2++;
                            break;
                        }
                        else
                            if (MangOCo[currRow - Count2, currColumn - Count2].SoHuu == 2)
                        {
                            SoQuanDich2++;
                        }
                        else
                        {

                            break;
                        }

                    }

                    break;
                }

            }

            if (SoQuanTa == 2)
                return 0;
            if (SoQuanTa == 0)
                DiemTong += MangPhongNgu[SoQuanDich] * 2;
            else
                DiemTong += MangPhongNgu[SoQuanDich];


            if (SoQuanTa2 == 0)
                DiemTong += MangPhongNgu[SoQuanTa2] * 2;
            else
                DiemTong += MangPhongNgu[SoQuanTa2];


            if (SoQuanDich >= SoQuanDich2)
                DiemTong -= 1;
            else
                DiemTong -= 2;
            if (SoQuanDich == 4)
                DiemTong *= 2;
            return DiemTong;
        }


        #endregion
        #endregion


        public void Undo(Graphics g)
        {
            if (StackQuanCoDaDi.Count != 0)
            {
                QuanCo quanco = StackQuanCoDaDi.Pop();
                StackQuanCoDaUndo.Push(new QuanCo(quanco.Row, quanco.Column, quanco.ViTri, quanco.SoHuu));
                MangOCo[quanco.Row, quanco.Column].SoHuu = 0;
                if (Luotdi == 1)
                    Luotdi = 2;
                else
                    Luotdi = 1;
            }

            VeBanCo(g);
            VeLaiBanCo(g);
        }

        public void Redo(Graphics g)
        {
            if (StackQuanCoDaUndo.Count != 0)
            {
                QuanCo quanco = StackQuanCoDaUndo.Pop();
                StackQuanCoDaDi.Push(new QuanCo(quanco.Row, quanco.Column, quanco.ViTri, quanco.SoHuu));
                MangOCo[quanco.Row, quanco.Column].SoHuu = quanco.SoHuu;
                if (Luotdi == 1)
                    Luotdi = 2;
                else
                    Luotdi = 1;
                _BanCo.VeQuanCo(g, quanco.ViTri, quanco.SoHuu == 1 ? ImageO : ImageX);
            }

        }

        public void New(Graphics gr)
        {
            SanSang = false;
            StackQuanCoDaDi = new Stack<QuanCo>();
            VeBanCo(gr);
            KhoiTaoMangOCo();
        }

        #region Duyet Chien Thang
        public void KetThucGame()
        {
            System.Media.SoundPlayer sound1 = new SoundPlayer(@"C://Users\pc\Downloads\Documents\Visual Studio 2015\Projects\GameCaro\GameCaro\Resources\WinSound.WAV");
            switch (_Ketthuc)
            {

                case KETTHUC.Hoa:
                    MessageBox.Show("Hòa rồi nhé!!!", "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    break;
                case KETTHUC.Player1:
                    sound1.Play();
                    MessageBox.Show("Người chơi thứ nhất thắng!!!", "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    break;
                case KETTHUC.Player2:
                    sound1.Play();
                    MessageBox.Show("Người chơi thứ hai thắng!!!", "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    break;
                case KETTHUC.Computer:
                    System.Media.SoundPlayer sound = new SoundPlayer(@"C://Users\pc\Downloads\Documents\Visual Studio 2015\Projects\GameCaro\GameCaro\Resources\LoseSound.WAV");
                    sound.Play();
                    MessageBox.Show("Computer Win! Luyện tập thêm đi nhé con gà!!!", "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    break;
                case KETTHUC.You:
                    sound1.Play();
                    MessageBox.Show("Bạn đã chiến thắng - Khá đấy!!!", "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    break;
            }
            SanSang = false;
        }
        public bool KtChienThang()
        {
            if (StackQuanCoDaDi.Count == _BanCo.SoCot * _BanCo.SoDong)
            {
                _Ketthuc = KETTHUC.Hoa;
                return true;
            }
            foreach (QuanCo quanco in StackQuanCoDaDi)
            {
                if (DuyetDoc(quanco.Row, quanco.Column, quanco.SoHuu) || DuyetNgang(quanco.Row, quanco.Column, quanco.SoHuu) || DuyetCheoLen(quanco.Row, quanco.Column, quanco.SoHuu) || DuyetCheoXuong(quanco.Row, quanco.Column, quanco.SoHuu))
                {
                    if (CheDoChoi == 1)
                    {
                        _Ketthuc = quanco.SoHuu == 1 ? KETTHUC.Player1 : KETTHUC.Player2;
                        return true;
                    }
                    else if (CheDoChoi == 2)
                    {
                        _Ketthuc = quanco.SoHuu == 2 ? KETTHUC.You : KETTHUC.Computer;
                        return true;
                    }
                }

            }
            
            return false;
        }

        public bool DuyetDoc(int CurrentRow, int CurrentColumn, int CurrentSoHuu)
        {
            if (CurrentRow > _BanCo.SoDong - 5)
                return false;
            int Count;
            for (Count = 1; Count < 5; Count++)
            {
                if (MangOCo[CurrentRow + Count, CurrentColumn].SoHuu != CurrentSoHuu)
                    return false; 
            }
            if (CurrentRow == 0 || CurrentRow + Count == _BanCo.SoDong)
                return true;
            if (MangOCo[CurrentRow - 1, CurrentColumn].SoHuu == 0 || MangOCo[CurrentRow + Count, CurrentColumn].SoHuu == 0)
                return true;
            return false;
            
        }
        public bool DuyetNgang(int CurrentRow, int CurrentColumn, int CurrentSoHuu)
        {
            if (CurrentColumn > _BanCo.SoCot - 5)
                return false;
            int Count;
            for (Count = 1; Count < 5; Count++)
            {
                if (MangOCo[CurrentRow, CurrentColumn + Count].SoHuu != CurrentSoHuu)
                    return false;
            }
            if (CurrentColumn == 0 || CurrentColumn + Count == _BanCo.SoCot)
                return true;
            if (MangOCo[CurrentRow, CurrentColumn -1 ].SoHuu == 0 || MangOCo[CurrentRow, CurrentColumn + Count].SoHuu == 0)
                return true;
            return false;

        }

        public bool DuyetCheoXuong(int CurrentRow, int CurrentColumn, int CurrentSoHuu)
        {
            if (CurrentRow > _BanCo.SoDong - 5 || CurrentColumn > _BanCo.SoCot -5)
                return false;
            int Count;
            for (Count = 1; Count < 5; Count++)
            {
                if (MangOCo[CurrentRow + Count, CurrentColumn + Count].SoHuu != CurrentSoHuu)
                    return false;
            }
            if (CurrentColumn == 0 || CurrentColumn + Count == _BanCo.SoCot || CurrentRow == 0 || CurrentRow + Count == _BanCo.SoDong)
                return true;
            if (MangOCo[CurrentRow - 1, CurrentColumn -1].SoHuu == 0 || MangOCo[CurrentRow + Count, CurrentColumn + Count].SoHuu == 0)
                return true;
            return false;

        }
        public bool DuyetCheoLen(int CurrentRow, int CurrentColumn, int CurrentSoHuu)
        {
            if (CurrentRow < 4 || CurrentColumn > _BanCo.SoCot - 5)
                return false;
            int Count;
            for (Count = 1; Count < 5; Count++)
            {
                if (MangOCo[CurrentRow - Count, CurrentColumn + Count].SoHuu != CurrentSoHuu)
                    return false;
            }
            if (CurrentRow == _BanCo.SoDong - 1 || CurrentRow == 4 || CurrentColumn == 0 || CurrentColumn + Count == _BanCo.SoCot)
                return true;
            if (MangOCo[CurrentRow + 1 , CurrentColumn - 1].SoHuu == 0 || MangOCo[CurrentRow - Count, CurrentColumn + Count ].SoHuu == 0)
                return true;
            return false;

        }

        #endregion


    }
}
