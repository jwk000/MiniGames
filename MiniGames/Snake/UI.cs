using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Snake
{

    public partial class GameUI : Form
    {
        //网格信息
        public class Grid
        {
            public int id;//编号
            public int sceneX;
            public int sceneY;
            public Rectangle rect;//绘图矩形
            public Color color;//填充颜色
        }

        public enum Direction
        {
            UP, DOWN, LEFT, RIGHT
        }

        //格子单位大小，像素
        const int kGridSize = 10;
        //画布起点
        Point kStartPoint = new Point(10, 10);
        //画布网格数
        const int kBgGridNum = 50;
        //画布大小
        Size kBgSize = new Size(kGridSize * kBgGridNum, kGridSize * kBgGridNum);
        //全部网格
        Grid[,] allGrids = new Grid[kBgGridNum, kBgGridNum];
        //记录蛇的身体
        List<Grid> snake = new List<Grid>();
        //苹果的位置
        Grid apple = null;
        //蛇头方向默认向右
        Direction dir = Direction.RIGHT;
        //随机数生成器，默认就使用时间种子
        Random randGen = new Random();

        public GameUI()
        {
            InitializeComponent();
            //窗口居中
            this.StartPosition = FormStartPosition.CenterScreen;
            //去掉最大化窗口
            this.MaximizeBox = false;
            //禁止拖动
            this.FormBorderStyle = FormBorderStyle.Fixed3D;
            //窗口大小
            this.ClientSize = new Size(kBgSize.Width + 20, kBgSize.Height+ 20);
            //窗口背景
            this.BackColor = Color.Chocolate;
            
            //双帧缓冲打开
            this.SetStyle(ControlStyles.DoubleBuffer | ControlStyles.UserPaint | ControlStyles.AllPaintingInWmPaint, true);
            this.UpdateStyles();

            //初始化网格位置和颜色
            for(int i=0;i<kBgGridNum;i++)
            {
                for(int j=0;j<kBgGridNum;j++)
                {
                    Grid g = new Grid()
                    {
                        id = i * kBgGridNum + j,
                        sceneX = j,
                        sceneY = i,
                        rect = new Rectangle(kStartPoint.X+ kGridSize*j,kStartPoint.Y+kGridSize*i,kGridSize,kGridSize),
                        color = Color.Black
                    };
                    allGrids[j, i] = g;
                }
            }
            //初始化蛇头
            snake.Add(getGridByPos(kBgGridNum / 2, kBgGridNum / 2));
            //初始化苹果
            apple = generateApple();
            //定时器
            this.UITimer.Enabled = true;
            this.UITimer.Interval = 100;
            this.UITimer.Tick += onTimer;
            this.UITimer.Start();
        }


        //处理绘图事件
        protected override void OnPaint(PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            //绘制边框
            drawBord(g);
            //绘制背景
            drawBack(g);
            //绘制蛇
            drawSnake(g);
            //绘制食物
            drawApple(g);
        }

        
        //处理键盘事件，修改蛇头的方向
        protected override void OnKeyDown(KeyEventArgs e)
        {
            base.OnKeyDown(e);
            switch (e.KeyCode)
            {
                case Keys.Up:
                    if (dir == Direction.DOWN) return;
                    dir = Direction.UP;
                    break;
                case Keys.Down:
                    if (dir == Direction.UP) return;
                    dir = Direction.DOWN;
                    break;
                case Keys.Left:
                    if (dir == Direction.RIGHT) return;
                    dir = Direction.LEFT;
                    break;
                case Keys.Right:
                    if (dir == Direction.LEFT) return;
                    dir = Direction.RIGHT;
                    break;
                default:
                    return ;
            }
        }

        void onTimer(object sender, EventArgs e)
        {
            //蛇头移动，全身移动
            Grid headGrid = snake.ElementAt(0);
            int headX=headGrid.sceneX;
            int headY=headGrid.sceneY;
            switch(dir)
            {
                case Direction.UP:
                    headY--;
                    if (headY < 0) headY += kBgGridNum;
                    break;
                case Direction.DOWN:
                    headY++;
                    if (headY >= kBgGridNum) headY -= kBgGridNum;
                    break;
                case Direction.LEFT:
                    headX--;
                    if (headX < 0) headX += kBgGridNum;
                    break;
                case Direction.RIGHT:
                    headX++;
                    if (headX >= kBgGridNum) headX -= kBgGridNum;
                    break;

            }
            headGrid = getGridByPos(headX, headY);
            snake.Insert(0, headGrid);

            //如果吃掉了苹果不移除尾部，相当于变长了
            if(headGrid == apple)
            {
                //重新生成苹果
                apple = generateApple();
            }
            else
            {
                snake.RemoveAt(snake.Count - 1);
            }

            //重绘整个窗口
            this.Invalidate(new Rectangle(0, 0, this.Size.Width, this.Size.Height));

            //如果蛇自己闭环了就失败了
            for (int i = 1; i < snake.Count;i++ )
            {
                if (snake[i] == headGrid)
                {
                    this.UITimer.Stop();
                    MessageBox.Show("这就很尴尬了……");
                    restart();
                }
            }
        }

        //重新开始游戏
        void restart()
        {
            snake.Clear();
            //初始化蛇头
            snake.Add(getGridByPos(kBgGridNum / 2, kBgGridNum / 2));
            //初始化苹果
            apple = generateApple();
            //计时器开始
            this.UITimer.Start();
        }

        //生成apple位置
        Grid generateApple()
        {
            List<Grid> appleGrids = new List<Grid>();
            //把所有除了snake之外的网格加入候选列表即可
            foreach(Grid grid in allGrids)
            {
                if (snake.Contains(grid)) continue;
                appleGrids.Add(grid);
            }
            //随机数
            int idx = randGen.Next(appleGrids.Count);
            return appleGrids[idx];
        }

        //逻辑位置得到grid
        Grid getGridByPos(int x, int  y)
        {
            return allGrids[x, y];
        }

        void drawBord(Graphics g)
        {
            //上下左右偏移10像素，宽和高按网格数计算
            g.DrawRectangle(new Pen(Color.White, 2), new Rectangle(kStartPoint,kBgSize));
        }

        void drawBack(Graphics g)
        {
            g.FillRectangle(new SolidBrush(Color.Black), new Rectangle(kStartPoint, kBgSize));
        }

        void drawSnake(Graphics g)
        {
            Brush headBrush = new SolidBrush(Color.Red);
            Brush bodyBrush = new SolidBrush(Color.DarkRed);
            Grid[] arr = snake.ToArray();
            for (int i = 0; i < snake.Count;i++ )
            {
                if (i == 0)
                {//红色的头
                    g.FillRectangle(headBrush, arr[i].rect);
                }
                else
                {//暗红身体
                    g.FillRectangle(bodyBrush, arr[i].rect);
                }
            }
        }

        void drawApple(Graphics g)
        {//绿色的苹果
            g.FillEllipse(new SolidBrush(Color.Green),apple.rect);
        }
    }
}
