using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace _2048
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            BackColor = Color.White;
            DoubleBuffered = true;
            ClientSize = new Size(200 + BoardSize * TileSize, 200 + BoardSize * TileSize);
            Init();
        }



        
        //空格随机出现2、4、8
        //方向键控制数字移动
        //每次合并相邻的2个格子

        Timer timer = new Timer();
        const int MarginSize = 100;
        const int TileSize = 100;
        const int BoardSize = 4;
        const int AnimTime = 300;//ms
        Tile[,] Tiles = new Tile[BoardSize, BoardSize];
        static Brush[] BgBrushs = new Brush[]
        {
            Brushes.LightYellow,//2
            Brushes.LightYellow,//4
            Brushes.Orange,//8
            Brushes.Orange,//16
            Brushes.GreenYellow,//32
            Brushes.GreenYellow,//64
            Brushes.OrangeRed,//128
            Brushes.OrangeRed,//256
            Brushes.Pink,//512
            Brushes.Pink,//1024
            Brushes.Red,//2048
        };
        class Tile
        {
            public int Num = 0;//0是空格子
            public Brush BgBrush;
            public Point FromPos;
            public Point ToPos;
            public Point Pos;
            public int ToNum = 0;
            public void SetNum(int n)
            {
                Num = n;
                ToNum = n;
                if (Num == 0)
                {
                    BgBrush = Brushes.White;
                }
                else
                {
                    for (int i = 0; i < BgBrushs.Length; i++)
                    {
                        if (Num == (2 << i))
                        {
                            BgBrush = BgBrushs[i];
                            break;
                        }
                    }
                }
            }
        }

        void Init()
        {
            for (int x = 0; x < BoardSize; x++)
            {
                for (int y = 0; y < BoardSize; y++)
                {
                    Tiles[x, y] = new Tile();
                    Tiles[x, y].SetNum(0);
                    Tiles[x, y].FromPos = new Point(MarginSize + x * TileSize, MarginSize + y * TileSize);
                    Tiles[x, y].Pos = Tiles[x, y].ToPos = Tiles[x, y].FromPos;
                }
            }
            Next();
            timer.Enabled = true;
            timer.Interval = 50;
            timer.Tick += OnTick;
            timer.Stop();
        }

        int timerTicks = 0;
        private void OnTick(object sender, EventArgs e)
        {
            timerTicks += timer.Interval;
            if (timerTicks > AnimTime)
            {
                OnMoveEnd();
                return;
            }
            double ratio = 1.0 * timerTicks / AnimTime;
            //格子移动动画，所有格子0.8秒移动完毕
            foreach (Tile tile in Tiles)
            {
                tile.Pos.X = (int)(tile.FromPos.X * (1 - ratio) + tile.ToPos.X * ratio);
                tile.Pos.Y = (int)(tile.FromPos.Y * (1 - ratio) + tile.ToPos.Y * ratio);
            }
            Invalidate();
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            //绘制board
            for (int i = 0; i <= BoardSize; i++)
            {
                e.Graphics.DrawLine(Pens.Gray, MarginSize, MarginSize + i * TileSize, MarginSize + BoardSize * TileSize, MarginSize + i * TileSize);
            }
            for (int j = 0; j <= BoardSize; j++)
            {
                e.Graphics.DrawLine(Pens.Gray, MarginSize + j * TileSize, MarginSize, MarginSize + j * TileSize, MarginSize + BoardSize * TileSize);
            }

            Size tileSize = new Size(TileSize - 4, TileSize - 4);
            Size posOffset = new Size(2, 2);
            Font drawFont = new Font(FontFamily.GenericMonospace, 20);
            //绘制tile
            foreach (Tile tile in Tiles)
            {
                if (tile.Num > 0)
                {
                    e.Graphics.FillRectangle(tile.BgBrush, new Rectangle(tile.Pos + posOffset, tileSize));

                    // 获取字符串的像素宽度
                    SizeF stringSize = e.Graphics.MeasureString(tile.Num.ToString(), drawFont);
                    e.Graphics.DrawString(tile.Num.ToString(), drawFont, Brushes.Black,
                        new PointF(tile.Pos.X + TileSize / 2 - stringSize.Width / 2, tile.Pos.Y + TileSize / 2 - stringSize.Height / 2));
                }
            }
        }

        protected override void OnKeyDown(KeyEventArgs e)
        {
            base.OnKeyDown(e);
            if (e.KeyCode == Keys.Up)
            {
                MoveUp();
            }
            else if (e.KeyCode == Keys.Down)
            {
                MoveDown();
            }
            else if (e.KeyCode == Keys.Left)
            {
                MoveLeft();
            }
            else if (e.KeyCode == Keys.Right)
            {
                MoveRight();
            }
        }

        void MoveLeft()
        {
            for (int y = 0; y < BoardSize; y++)
            {
                int left = 0;
                for (int x = 1; x < BoardSize; x++)
                {
                    if (Tiles[x, y].Num > 0)
                    {
                        if (Tiles[left, y].ToNum == 0)//空白
                        {
                            Tiles[left, y].ToNum = Tiles[x, y].Num;
                            Tiles[x, y].ToNum = 0;
                            Tiles[x, y].ToPos.X = Tiles[left, y].Pos.X;
                        }
                        else//不空
                        {
                            if (Tiles[left, y].ToNum == Tiles[x, y].Num)//合并
                            {
                                Tiles[left, y].ToNum *= 2;
                                Tiles[x, y].ToNum = 0;
                                left++;
                                Tiles[x, y].ToPos.X = Tiles[left, y].Pos.X;
                            }
                            else //不合并
                            {
                                left++;
                                if (left != x)
                                {
                                    Tiles[x, y].ToPos.X = Tiles[left, y].Pos.X;
                                    Tiles[left, y].ToNum = Tiles[x, y].Num;
                                    Tiles[x, y].ToNum = 0;
                                }
                            }
                        }
                    }
                }
            }
            timerTicks = 0;
            timer.Start();
        }

        void MoveRight()
        {
            for (int y = 0; y < BoardSize; y++)
            {
                int right = BoardSize - 1;
                for (int x = right - 1; x >= 0; x--)
                {
                    if (Tiles[x, y].Num > 0)
                    {
                        if (Tiles[right, y].ToNum == 0)//空白
                        {
                            Tiles[right, y].ToNum = Tiles[x, y].Num;
                            Tiles[x, y].ToNum = 0;
                            Tiles[x, y].ToPos.X = Tiles[right, y].Pos.X;
                        }
                        else//不空
                        {
                            if (Tiles[right, y].ToNum == Tiles[x, y].Num)//合并
                            {
                                Tiles[right, y].ToNum *= 2;
                                Tiles[x, y].ToNum = 0;
                                right--;
                                Tiles[x, y].ToPos.X = Tiles[right, y].Pos.X;
                            }
                            else //不合并
                            {
                                right--;
                                if (x != right)
                                {
                                    Tiles[x, y].ToPos.X = Tiles[right, y].Pos.X;
                                    Tiles[right, y].ToNum = Tiles[x, y].Num;
                                    Tiles[x, y].ToNum = 0;
                                }

                            }
                        }
                    }
                }
            }
            timerTicks = 0;
            timer.Start();
        }

        void MoveUp()
        {
            for (int x = 0; x < BoardSize; x++)
            {
                int top = 0;
                for (int y = 1; y < BoardSize; y++)
                {
                    if (Tiles[x, y].Num > 0)
                    {
                        if (Tiles[x, top].ToNum == 0)//空白
                        {
                            Tiles[x, top].ToNum = Tiles[x, y].Num;
                            Tiles[x, y].ToNum = 0;
                            Tiles[x, y].ToPos = Tiles[x, top].Pos;
                        }
                        else//不空
                        {
                            if (Tiles[x, top].ToNum == Tiles[x, y].Num)//合并
                            {
                                Tiles[x, top].ToNum *= 2;
                                Tiles[x, y].ToNum = 0;
                                top++;
                                Tiles[x, y].ToPos = Tiles[x, top].Pos;
                            }
                            else //不合并
                            {
                                top++;
                                if (y != top)
                                {
                                    Tiles[x, y].ToPos = Tiles[x, top].Pos;
                                    Tiles[x, top].ToNum = Tiles[x, y].Num;
                                    Tiles[x, y].ToNum = 0;
                                }
                            }
                        }
                    }
                }
            }
            timerTicks = 0;
            timer.Start();
        }

        void MoveDown()
        {
            for (int x = 0; x < BoardSize; x++)
            {
                int bottom = BoardSize - 1;
                for (int y = bottom - 1; y >= 0; y--)
                {
                    if (Tiles[x, y].Num > 0)
                    {
                        if (Tiles[x, bottom].ToNum == 0)//空白
                        {
                            Tiles[x, bottom].ToNum = Tiles[x, y].Num;
                            Tiles[x, y].ToNum = 0;
                            Tiles[x, y].ToPos = Tiles[x, bottom].Pos;
                        }
                        else//不空
                        {
                            if (Tiles[x, bottom].ToNum == Tiles[x, y].Num)//合并
                            {
                                Tiles[x, bottom].ToNum *= 2;
                                Tiles[x, y].ToNum = 0;
                                bottom--;
                                Tiles[x, y].ToPos = Tiles[x, bottom].Pos;
                            }
                            else //不合并
                            {
                                bottom--;
                                if (bottom != y)
                                {
                                    Tiles[x, y].ToPos = Tiles[x, bottom].Pos;
                                    Tiles[x, bottom].ToNum = Tiles[x, y].Num;
                                    Tiles[x, y].ToNum = 0;
                                }
                            }
                        }
                    }
                }
            }
            timerTicks = 0;
            timer.Start();
        }
        Random random = new Random();
        bool Next()
        {
            List<Tile> emptiles = new List<Tile>();
            foreach (Tile t in Tiles)
            {
                if (t.Num == 0)
                {
                    emptiles.Add(t);
                }
            }
            if (emptiles.Count == 0)
            {
                MessageBox.Show("你输了");
                return false;
            }
            int r = random.Next(0, emptiles.Count);
            emptiles[r].SetNum(2);
            return true;
        }
        void OnMoveEnd()
        {
            timer.Stop();
            //数字合并
            foreach (Tile t in Tiles)
            {
                t.SetNum(t.ToNum);
            }

            //生成数字
            if (!Next())
            {
                return;
            }
            //位置复原
            foreach (Tile t in Tiles)
            {
                t.Pos = t.FromPos;
                t.ToPos = t.FromPos;
            }
            Invalidate();
        }

    }
}
