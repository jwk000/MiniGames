using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MineSweeper
{
    public partial class Form1 : Form
    {
        enum GridState
        {
            UNKNOWN,//没点开
            BLANK,//空白和数字
            MINE,//标记是雷区
            MAYBE,//可能是雷区
            BOOM,//爆炸的雷区
        }
        class Grid
        {
            public int X;
            public int Y;
            public int num;//数字 -1雷 0空 >0雷数
            public Rectangle rect;
            public GridState state;//当前的显示状态

            public bool IsMineGrid
            {
                get { return num == -1; }
            }
            public void Reset()
            {
                num = 0;
                state = GridState.UNKNOWN;
            }
        }

        //配置选项
        const int kColumnNum = 10;
        const int kRowNum = 10;
        const int kGridSize = 21;

        
        //全局变量
        Grid[,] AllGrids = new Grid[kColumnNum, kRowNum];
        int UseTimeSec = 0;
        const int kClientLeftX = 10;
        const int kClientUpY = 35;
        Random randGen = new Random();
        int MineNum = 0;//雷数量
        int MarkMineCnt = 0;//标记雷的数量

        public Form1()
        {
            InitializeComponent();
            InitForm();
            InitGrids();
            GenerateMine();
        }

        void InitForm()
        {
            //窗口居中
            this.StartPosition = FormStartPosition.CenterScreen;
            //去掉最大化窗口
            this.MaximizeBox = false;
            //禁止拖动
            this.FormBorderStyle = FormBorderStyle.Fixed3D;
            //窗口大小
            this.ClientSize = new Size(kColumnNum * kGridSize + 20, kRowNum * kGridSize + 50);
            //窗口背景
            this.BackColor = Color.Black;
            //双帧缓冲打开
            this.SetStyle(ControlStyles.DoubleBuffer | ControlStyles.UserPaint | ControlStyles.AllPaintingInWmPaint, true);
            this.UpdateStyles();
            //定时器
            this.UITimer.Enabled = true;
            this.UITimer.Interval = 1000;
            this.UITimer.Tick += OnTimer;

        }


        //计时器
        void OnTimer(object sender, EventArgs e)
        {
            UseTimeSec++;
            this.UIUseTime.Text = string.Format("本局已使用时间：{0}秒", UseTimeSec);
        }

        //初始化网格
        void InitGrids()
        {
            for(int j=0;j<kRowNum;j++)
            {
                for(int i=0;i<kColumnNum;i++)
                {
                    AllGrids[i, j] = new Grid()
                    {
                        X = i,
                        Y = j,
                        num = 0,
                        rect = new Rectangle(kClientLeftX + i * kGridSize, kClientUpY + j * kGridSize, kGridSize - 1, kGridSize - 1),
                        state = GridState.UNKNOWN
                    };
                }
            }
        }

        //初始化布局
        void GenerateMine()
        {
            //随机雷数
            MineNum = randGen.Next(5, kRowNum * kColumnNum / 2);
            //安插到网格
            HashSet<int> mineIndex = new HashSet<int>();
            for(int i=0;i<MineNum;i++)
            {
                int idx = randGen.Next(0, kColumnNum * kRowNum);
                if(mineIndex.Contains(idx))
                {
                    i--;
                }else
                {
                    mineIndex.Add(idx);
                }
            }
            foreach(int idx in mineIndex)
            {
                int x = idx / kColumnNum;
                int y = idx % kColumnNum;
                AllGrids[x, y].num = -1;
            }
            //计算每个格子周围雷数
            for(int j=0;j<kRowNum;j++)
            {
                for(int i=0;i<kColumnNum;i++)
                {
                    if(0 == AllGrids[i,j].num)
                    {
                        //周围8个位置
                        for(int x=-1;x<=1;x++)
                        {
                            for(int y=-1;y<=1;y++)
                            {
                                if (i + x < 0 || i + x >= kColumnNum) continue;
                                if (j + y < 0 || j + y >= kRowNum) continue;
                                if(AllGrids[i+x,j+y].num == -1)
                                {
                                    AllGrids[i, j].num++;
                                }
                            }
                        }
                    }
                }
            }

        }
        //点击事件
        protected override void OnMouseClick(MouseEventArgs e)
        {
            base.OnMouseClick(e);
            //根据位置判断点击格子
            Grid grid = GetGridOnPos(e.X, e.Y);
            if (grid == null) return;

            if(e.Button == MouseButtons.Left)
            {
                //左击必须是未知状态
                if (grid.state != GridState.UNKNOWN) return;

                if (grid.IsMineGrid)
                {
                    //点到雷 全部爆炸
                    AllMineBoom();
                    GameOver(false);
                }else 
                {
                    OpenBlank(grid);
                }
            }else if(e.Button == MouseButtons.Right)
            {
                if(grid.state == GridState.UNKNOWN)
                {
                    grid.state = GridState.MINE;
                    if(grid.IsMineGrid)
                    {
                        MarkMineCnt++;
                        if(MarkMineCnt == MineNum)
                        {
                            GameOver(true);
                        }
                    }
                }
                else if(grid.state == GridState.MINE)
                {
                    grid.state = GridState.MAYBE;
                }
                else if(grid.state == GridState.MAYBE)
                {
                    grid.state = GridState.UNKNOWN;
                }
            }

            //重绘整个窗口
            this.Invalidate(new Rectangle(0, 0, this.Size.Width, this.Size.Height));
        }
        //根据点击位置获得格子
        Grid GetGridOnPos(int x, int y)
        {
            int X = (x - kClientLeftX) / kGridSize;
            int Y = (y - kClientUpY) / kGridSize;
            if (X < 0 || X >= kColumnNum) return null;
            if (Y < 0 || Y >= kRowNum) return null;
            return AllGrids[X, Y];
        }

        void AllMineBoom()
        {
            foreach(Grid g in AllGrids)
            {
                if(g.IsMineGrid)
                {
                    g.state = GridState.BOOM;
                }
            }
        }

        void OpenBlank(Grid g)
        {
            if (g.state != GridState.UNKNOWN) return;
            if (g.IsMineGrid) return;
            g.state = GridState.BLANK;
            if (g.num == 0)
            {
                //左
                if (g.X - 1 >= 0)
                {
                    OpenBlank(AllGrids[g.X - 1, g.Y]);
                }
                //右
                if (g.X + 1 < kColumnNum)
                {
                    OpenBlank(AllGrids[g.X + 1, g.Y]);
                }
                //上
                if (g.Y - 1 >= 0)
                {
                    OpenBlank(AllGrids[g.X, g.Y - 1]);
                }
                //下
                if (g.Y + 1 < kRowNum)
                {
                    OpenBlank(AllGrids[g.X, g.Y + 1]);
                }
            }

        }

        void GameOver(bool isWin)
        {
            //重绘整个窗口
            this.Invalidate(new Rectangle(0, 0, this.Size.Width, this.Size.Height));

            UITimer.Stop();
            DialogResult ret;
            if (isWin)
            {
                ret = MessageBox.Show("你找到了所有雷，点击重试重玩", "You Are Winner", MessageBoxButtons.RetryCancel);
            }else
            {
                ret = MessageBox.Show("你踩到了雷，重来一次吗？", "Game Over", MessageBoxButtons.RetryCancel, MessageBoxIcon.Error);
            }

            if(ret == DialogResult.Retry)
            {
                RestartGame();
            }else
            {
                Close();
            }
        }

        void RestartGame()
        {
            foreach (Grid g in AllGrids)
            {
                g.Reset();
            }
            GenerateMine();
            UseTimeSec = 0;
            UITimer.Start();
        }

        //绘图
        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            Graphics g = e.Graphics;
            //绘制边框
            DrawBoard(g);
            //绘制网格
            DrawGrids(g);
        }


        //绘制格子
        void DrawGrids(Graphics g)
        {
            for (int j = 0; j < kRowNum; j++)
            {
                for (int i = 0; i < kColumnNum; i++)
                {
                    Grid grid = AllGrids[i, j];
                    switch(grid.state)
                    {
                        case GridState.BLANK:
                            g.FillRectangle(new SolidBrush(Color.White), grid.rect);
                            if (grid.num > 0)
                            {
                                g.DrawString(string.Format("{0}", grid.num), new Font("Arial", 14), new SolidBrush(Color.DarkSeaGreen), grid.rect);
                            }
                            break;
                        case GridState.BOOM:
                            g.FillRectangle(new SolidBrush(Color.Red), grid.rect);
                            g.DrawString("雷", new Font("Arial", 11), new SolidBrush(Color.Black), grid.rect);
                            break;
                        case GridState.MAYBE:
                            g.FillRectangle(new SolidBrush(Color.Gray), grid.rect);
                            g.DrawString("？", new Font("Arial", 14), new SolidBrush(Color.Red), grid.rect);
                            break;
                        case GridState.MINE:
                            g.FillRectangle(new SolidBrush(Color.Gray), grid.rect);
                            g.DrawString("X", new Font("Arial", 14), new SolidBrush(Color.DodgerBlue), grid.rect);
                            break;
                        case GridState.UNKNOWN:
                            g.FillRectangle(new SolidBrush(Color.Gray), grid.rect);
                            break;
                    }
                }
            }
        }

        //绘制边框
        void DrawBoard(Graphics g)
        {
            g.DrawRectangle(new Pen(Color.White, 2), new Rectangle(kClientLeftX,kClientUpY,kGridSize*kColumnNum,kGridSize*kRowNum));
        }

        
    }
}
