using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
namespace Sudoku
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            this.DoubleBuffered = true;
            _ResetGraphic();
            this.ClientSizeChanged += Form1_ClientSizeChanged;
            _ResetNums();
            this.timer1.Interval = 1000;
            this.timer1.Tick += Timer1_Tick;
            this.timer1.Start();
        }


        private void Timer1_Tick(object? sender, EventArgs e)
        {
            mUseTime++;
            Invalidate();
        }


        void _ResetNums()
        {
            mSelectX = -1;
            mSelectY = -1;
            mFillNum = 0;
            mUseTime = 0;
            Nums = new int[9, 9];
            for (int x = 0; x < 9; x++)
            {
                for (int y = 0; y < 9; y++)
                {
                    Pools[x, y] = new List<int> { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9 };
                }
            }
            Prepare2();
            Gen();
            KickNums();
        }


        void _ResetGraphic()
        {
            mGridSize = (int)(Math.Min(ClientSize.Width, ClientSize.Height) / 9 * 0.8);
            mTopLeft.X = (this.ClientSize.Width - mGridSize * 9) / 2;
            mTopLeft.Y = (this.ClientSize.Height - mGridSize * 9) / 2;
            mBottom.X = mTopLeft.X;
            mBottom.Y = mTopLeft.Y + (int)(mGridSize * 9.2f);


            for (int i = 0; i < 9; i++)
            {
                for (int j = 0; j < 9; j++)
                {
                    if (Grids[i, j] == null)
                    {
                        Grids[i, j] = new Grid(i, j, mGridSize, mTopLeft);
                    }
                    else
                    {
                        Grids[i, j].ResetUI(i, j, mGridSize, mTopLeft);
                    }
                }
            }
        }
        private void Form1_ClientSizeChanged(object? sender, EventArgs e)
        {
            _ResetGraphic();
            Invalidate();
        }
        Random mRand = new Random();
        int mGridSize = 20;
        Point mTopLeft = new Point();
        Point mBottom = new Point();
        Grid[,] Grids = new Grid[9, 9];
        int[,] Nums = new int[9, 9];
        int mFillNum = 0;
        List<int>[,] Pools = new List<int>[9, 9];
        int mSelectX = -1;
        int mSelectY = -1;
        int mUseTime = 0;


        void Prepare(int top, int left)
        {
            List<int> p = new List<int> { 1, 2, 3, 4, 5, 6, 7, 8, 9 };
            for (int x = left; x < left + 3; x++)
            {
                for (int y = top; y < top + 3; y++)
                {
                    int r = mRand.Next(0, p.Count);
                    int v = p[r];
                    p.RemoveAt(r);
                    Nums[x, y] = v;
                    mFillNum++;
                    Pools[x, y][0] = v;
                    RemovePoolNum(x, y, v);
                }
            }
        }
        void Prepare1()
        {
            for (int y = 0; y < 9; y++)
            {
                Nums[0, y] = y + 1;
                Pools[0, y][0] = y + 1;
                RemovePoolNum(0, y, y + 1);
                mFillNum++;
            }
        }
        void Prepare2()
        {
            List<int> p = new List<int> { 1, 2, 3, 4, 5, 6, 7, 8, 9 };
            for (int y = 0; y < 9; y++)
            {
                int r = mRand.Next(0, p.Count);
                int v = p[r];
                p.RemoveAt(r);


                Nums[0, y] = v;
                Pools[0, y][0] = v;
                RemovePoolNum(0, y, v);
                mFillNum++;
            }
        }


        bool _FindZero(ref int a, ref int b)
        {
            for (int x = 0; x < 9; x++)
            {
                for (int y = 0; y < 9; y++)
                {
                    if (Nums[x, y] == 0)//空白
                    {
                        a = x;
                        b = y;
                        return true;
                    }
                }
            }
            return false;
        }
        bool Gen()
        {
            //第一个空白处尝试一个数字，寻找下个空白继续尝试，失败了尝试下一个数字
            int a = 0;
            int b = 0;


            if (!_FindZero(ref a, ref b))
            {
                return false;
            }
            var p = Pools[a, b];
            int v = 0;
            while (v == 0)
            {
                p[0]++;
                if (p[0] == 10)
                {
                    p[0] = 0;
                    return false;
                }
                v = p[p[0]];
            }
            Nums[a, b] = v;
            mFillNum++;
            if (mFillNum == 81)
            {
                return true;
            }
            RemovePoolNum(a, b, v);
            bool ok = Gen();
            if (!ok)
            {
                Nums[a, b] = 0;
                mFillNum--;
                AddPoolNum(a, b, v);
                return Gen();
            }
            return true;
        }
        void AddPoolNum(int a, int b, int v)
        {
            ChangePool(a, b, v, true);
        }
        void RemovePoolNum(int a, int b, int v)
        {
            ChangePool(a, b, v, false);
        }
        bool CanAdd(int a, int b, int v)
        {
            for (int x = 0; x < 9; x++)
            {
                if (Nums[x, b] == v) return false;
            }
            for (int y = 0; y < 9; y++)
            {
                if (Nums[a, y] == v) return false;
            }
            int _x = a / 3 * 3;
            int _y = b / 3 * 3;
            for (int _xx = _x; _xx < _x + 3; _xx++)
            {
                for (int _yy = _y; _yy < _y + 3; _yy++)
                {
                    if (Nums[_xx, _yy] == v) return false;
                }
            }
            return true;
        }
        void ChangePool(int a, int b, int v, bool add)
        {
            //踢出已经选出的数字
            for (int _x = 0; _x < 9; _x++)
            {
                var _p = Pools[_x, b];
                if (add && CanAdd(_x, b, v))
                {
                    _p[v] = v;
                }
                else if (!add)
                {
                    _p[v] = 0;
                }
            }
            for (int _y = 0; _y < 9; _y++)
            {
                var _p = Pools[a, _y];
                if (add && CanAdd(a, _y, v))
                {
                    _p[v] = v;
                }
                else if (!add)
                {
                    _p[v] = 0;
                }
            }
            {
                int _x = a / 3 * 3;
                int _y = b / 3 * 3;
                for (int _xx = _x; _xx < _x + 3; _xx++)
                {
                    for (int _yy = _y; _yy < _y + 3; _yy++)
                    {
                        var _p = Pools[_xx, _yy];
                        if (add && CanAdd(_xx, _yy, v))
                        {
                            _p[v] = v;
                        }
                        else if (!add)
                        {
                            _p[v] = 0;
                        }
                    }
                }
            }
        }
        Font numFount = new Font(FontFamily.GenericSansSerif, 20);
        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            var g = e.Graphics;
            //顶部计时器
            g.DrawString($"{mUseTime / 3600}:{mUseTime / 60}:{mUseTime % 60}", numFount, Brushes.Crimson, mTopLeft.X + mGridSize * 4 - 10, mTopLeft.Y - mGridSize * 0.8f);
            Size numOffset = new Size(mGridSize / 2, mGridSize / 2);
            //绘制9x9网格
            for (int x = 0; x < 9; x++)
            {
                for (int y = 0; y < 9; y++)
                {
                    int n = Nums[x, y];
                    Grid grid = Grids[x, y];
                    if ((x / 3 + y / 3) % 2 == 0)
                    {
                        g.FillRectangle(Brushes.LightGray, grid.rect);
                    }
                    if (grid.tofill)
                    {
                        g.FillRectangle(Brushes.LightYellow, grid.rect);
                    }
                    g.DrawRectangle(Pens.Black, grid.rect);
                    if (n > 0)
                    {
                        g.DrawString(n.ToString(), numFount, Brushes.Black, grid.rect.X + mGridSize / 2 - 12, grid.rect.Y + mGridSize / 2 - 15);
                    }
                }
            }
            {
                Grid grid = Grids[mSelectX, mSelectY];
                g.DrawRectangle(Pens.DarkOrange, grid.rect);
            }




            //候选数字
            var p = Pools[mSelectX, mSelectY];
            for (int i = 1; i <= 9; i++)
            {
                Rectangle rect = new Rectangle(mBottom.X + mGridSize * (i - 1), mBottom.Y, (int)(mGridSize * 0.8), (int)(mGridSize * 0.8));
                g.FillRectangle(Brushes.LightSlateGray, rect);
                if (p[i] > 0)
                {
                    g.DrawString(p[i].ToString(), numFount, Brushes.DarkOrange, rect.X + mGridSize / 2 - 16, rect.Y + mGridSize / 2 - 19);
                }
            }
        }
        //默认选中第一个空白位置，键盘选择相邻空白
        protected override void OnKeyDown(KeyEventArgs e)
        {
            base.OnKeyDown(e);
            if (e.KeyCode == Keys.Escape)
            {
                _ResetNums();
            }
            else if (e.KeyCode >= Keys.NumPad1 && e.KeyCode <= Keys.NumPad9)
            {
                int n = e.KeyCode - Keys.NumPad0;
                SetNum(n);
            }
            else if (e.KeyCode >= Keys.D1 && e.KeyCode <= Keys.D9)
            {
                int n = e.KeyCode - Keys.D0;
                SetNum(n);
            }
            else if (e.KeyCode == Keys.Space)
            {
                ClearNum();
            }
            else if (e.KeyCode == Keys.Enter)
            {
                CheckWin();
            }
            else if (e.KeyCode == Keys.Left)
            {
                for (int x = mSelectX - 1; x >= 0; x--)
                {
                    if (Grids[x, mSelectY].tofill)
                    {
                        mSelectX = x;
                        break;
                    }
                }
            }
            else if (e.KeyCode == Keys.Right)
            {
                for (int x = mSelectX + 1; x < 9; x++)
                {
                    if (Grids[x, mSelectY].tofill)
                    {
                        mSelectX = x;
                        break;
                    }
                }
            }
            else if (e.KeyCode == Keys.Up)
            {
                for (int y = mSelectY - 1; y >= 0; y--)
                {
                    if (Grids[mSelectX, y].tofill)
                    {
                        mSelectY = y;
                        break;
                    }
                }
            }
            else if (e.KeyCode == Keys.Down)
            {
                for (int y = mSelectY + 1; y < 9; y++)
                {
                    if (Grids[mSelectX, y].tofill)
                    {
                        mSelectY = y;
                        break;
                    }
                }
            }
            Invalidate();
        }


        protected override void OnMouseDown(MouseEventArgs e)
        {
            base.OnMouseDown(e);
            int x = (e.X - mTopLeft.X) / mGridSize;
            int y = (e.Y - mTopLeft.Y) / mGridSize;
            if (x >= 0 && x < 9 && y >= 0 && y < 9 && Grids[x, y].tofill)
            {
                mSelectX = x;
                mSelectY = y;
            }
            Invalidate();
        }


        bool CanSetNum(int n)
        {
            if (n < 1 || n > 9)
            {
                return false;
            }
            var p = Pools[mSelectX, mSelectY];
            for (int i = 1; i <= 9; i++)
            {
                if (p[i] == n)
                {
                    return true;
                }
            }
            return false;
        }


        void SetNum(int n)
        {
            if (CanSetNum(n))
            {
                RemovePoolNum(mSelectX, mSelectY, n);
                Nums[mSelectX, mSelectY] = n;
            }
        }


        void ClearNum()
        {
            int v = Nums[mSelectX, mSelectY];
            Nums[mSelectX, mSelectY] = 0;
            AddPoolNum(mSelectX, mSelectY, v);
        }


        void CheckWin()
        {
            foreach (int n in Nums)
            {
                if (n == 0)
                {
                    MessageBox.Show("未完成！");
                    return;
                }
            }
            MessageBox.Show($"你赢了！用时{mUseTime}秒");
        }


        //随机踢出数字，生成题目
        void KickNums()
        {
            for (int x = 0; x < 9; x++)
            {
                for (int y = 0; y < 9; y++)
                {
                    Grids[x, y].tofill = false;
                    int r = mRand.Next(0, 100);
                    if (r < 50)
                    {
                        int v = Nums[x, y];
                        Nums[x, y] = 0;
                        AddPoolNum(x, y, v);
                        Grids[x, y].tofill = true;
                        if (mSelectX == -1)
                        {
                            mSelectX = x;
                            mSelectY = y;
                        }
                    }
                }
            }
        }


    }
    class Grid
    {
        public Grid(int x, int y, int w, Point offset)
        {
            ResetUI(x, y, w, offset);
        }
        public void ResetUI(int x, int y, int w, Point offset)
        {
            rect.X = x * w + offset.X;
            rect.Y = y * w + offset.Y;
            rect.Width = w;
            rect.Height = w;
        }
        public Rectangle rect = new Rectangle();
        public bool tofill = false;
    }
}