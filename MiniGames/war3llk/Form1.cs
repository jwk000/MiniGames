using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing.Imaging;
using System.Threading;
using Game;

namespace GDI1
{
    public partial class Form1 : Form
    {

        public Form1()
        {
            InitializeComponent();
            //窗口居中
            this.StartPosition = FormStartPosition.CenterScreen;
            //去掉最大化窗口
            this.MaximizeBox = false;
            //禁止拖动
            this.FormBorderStyle = FormBorderStyle.Fixed3D;
            //计算场景大小
            this.Size = formSize;
            //双缓冲设置
            this.SetStyle(ControlStyles.DoubleBuffer | ControlStyles.UserPaint | ControlStyles.AllPaintingInWmPaint, true);
            this.UpdateStyles();
            //禁止显示胜利文字
            this.lbl_win.Hide();
            this.lbl_nomatch.Hide();
            this.bar_time.Width = PICSIZE * ROWPICS;

            if(PRACTICE)
            {
                this.bar_time.Hide();
                this.lbl_lefttime.Hide();
            }
            //开始计时器
            this.GameTimer.Interval = 1000;
            this.GameTimer.Start();
            this.GameTimer.Tick += OnTimer;

        }

        static int PICCOUNT = 128;//素材数量
        public static bool PRACTICE = false;
        public static int PICSIZE = 48;//图片尺寸
        public static int ROWPICS = 8;//一行的图片数
        public static int COLPICS = 8;//一列的图片数
        public static int SAMERATIO = 2;//重复率系数(25%)必须能整除
        static int TOTALPOINTCOUNT = (ROWPICS + 2) * (COLPICS + 2);//场景里面全部的点数量
        Random ran = new Random((int)DateTime.Now.ToBinary());//随机数发生器
        Size formSize = new Size((PICSIZE + 2) * (ROWPICS + 2), (PICSIZE + 2) * (COLPICS + 2));
        public SettingForm MainForm { get; set; }

        SceneState[,] sceneState = new SceneState[ROWPICS + 2, COLPICS + 2];//场景里面全部的点
        Image[] pics = new Image[PICCOUNT];//素材库
        int[] usePicIds = new int[ROWPICS * COLPICS / SAMERATIO];//被选中的图片id
        PicState[] picState = new PicState[ROWPICS * COLPICS];//图片状态
        PicSelect picSelector = new PicSelect();
        Pen LinePen = new Pen(Color.Yellow, 2);
        int UseTime = 0;
        int DeadTime = 90;

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            //初始化场景全部点坐标
            for (int i = 0; i < COLPICS + 2; i++)
            {
                for (int j = 0; j < ROWPICS + 2; j++)
                {
                    sceneState[j, i] = new SceneState();
                    sceneState[j, i].scenePoint = new Point(j * (PICSIZE + 2) + PICSIZE / 2 + 1, i * (PICSIZE + 2) + PICSIZE / 2 + 1);
                }
            }
            //加载图片资源
            for (int i = 0; i < PICCOUNT; i++)
            {
                //pics[i] = Image.FromFile(string.Format("item/war ({0}).gif", i + 1));
                pics[i] = this.picList.Images[i];
            }
            //选出图片id
            for (int i = 0; i < usePicIds.Length; i++)
            {
                int id = ran.Next(0, PICCOUNT);
                usePicIds[i] = id;
            }
            //分配图片id
            for (int i = 0; i < ROWPICS * COLPICS; i++)
            {
                picState[i] = new PicState();
                picState[i].nPicId = usePicIds[i % usePicIds.Length];
            }
            //图片定位
            for (int i = 0; i < COLPICS; i++)
            {
                for (int j = 0; j < ROWPICS; j++)
                {
                    picState[i * ROWPICS + j].sceneX = j + 1;
                    picState[i * ROWPICS + j].sceneY = i + 1;
                    sceneState[j + 1, i + 1].bLinkable = false;
                    picState[i * ROWPICS + j].picRectangle = new Rectangle(PICSIZE + j * (PICSIZE + 2), PICSIZE + i * (PICSIZE + 2), PICSIZE, PICSIZE);
                }
            }
            //打乱图片
            shuffle_pics();
            while(!check_pic_match())
            {
                shuffle_pics();
            }
        }
        protected override void OnClosed(EventArgs e)
        {
            base.OnClosed(e);
            MainForm.Show();
        }
        protected void OnTimer(object sender, EventArgs e)
        {
            UseTime++;
            if(UseTime== showNoMatchTime)
            {
                Thread.Sleep(1000);
                lbl_nomatch.Hide();
                shuffle_pics();
            }
            if (PRACTICE) return;
            if (UseTime == DeadTime)
            {
                this.GameTimer.Enabled = false;
                DialogResult ret = MessageBox.Show(this, "你超时了！能不能快点啊！", "你输了", MessageBoxButtons.RetryCancel);
                if (ret == DialogResult.Retry)
                {
                    this.Close();
                }else if (ret == DialogResult.Cancel)
                {
                    this.Close();
                }
                return;
            }
            this.lbl_lefttime.Text = string.Format("剩余{0}秒", DeadTime - UseTime);
            this.bar_time.Value = (int)((DeadTime - UseTime) * 1.0 / DeadTime * 100);
            double param = UseTime*1.0 / DeadTime;
            this.bar_time.ForeColor = Color.FromArgb((int)(255 * param), (int)(255 * (1 - param)), 0);
        }
        protected override void OnMouseDown(MouseEventArgs e)
        {
            base.OnMouseDown(e);
            int clickPicId = get_select_pic_idx(e.X, e.Y);
            if (clickPicId < 0) return;
            if (picState[clickPicId].bShown == false) return;

            //判断点击的图片是否连通
            if(picSelector.curSelector==0)
            {
                picSelector.curSelector = 1;
                picSelector.picStateId[0] = clickPicId;
                //绘制选中线
                force_redraw();
                return;
            }
            if (picSelector.curSelector==1)
            {
                if(picSelector.picStateId[0] == clickPicId)
                {
                    picSelector.curSelector = 0;//回滚状态
                    force_redraw();
                    return;
                }
                
                picSelector.picStateId[1] = clickPicId;
                if(is_pic_link(picSelector.picStateId[0],picSelector.picStateId[1]))
                {
                    picSelector.bLinkOk = true;
                    UseTime--;
                }
                else
                {
                    picSelector.curSelector = 0;
                }
            }
            force_redraw();
        }
        int showNoMatchTime = 0;
        protected override void OnMouseUp(MouseEventArgs e)
        {
            base.OnMouseUp(e);
            //隐藏已经连通的图片
            if (picSelector.bLinkOk)
            {
                picSelector.bLinkOk = false;
                picSelector.curSelector = 0;
                for(int i = 0; i < 2; i++)
                {
                    PicState ps = picState[picSelector.picStateId[i]];
                    ps.bShown = false;
                    sceneState[ps.sceneX, ps.sceneY].bLinkable = true;
                }
                
                force_redraw();

                if (!check_pic_match())
                {
                    if (check_win())
                    {
                        this.GameTimer.Enabled = false;
                        this.lbl_win.Show();
                    }
                    else
                    {
                        lbl_nomatch.Show();
                        showNoMatchTime = UseTime + 1;
                    }
                }
            }
        }
        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            Graphics dc = e.Graphics;
            for (int i = 0; i < COLPICS; i++)
            {
                for (int j = 0; j < ROWPICS; j++)
                {
                    PicState ps = picState[i * ROWPICS + j];
                    if (ps.bShown == false) continue;
                    Image image = pics[ps.nPicId];
                    dc.DrawImage(image, ps.picRectangle);
                }
            }
            //绘制选中线
            if(picSelector.curSelector==0&& picState[picSelector.picStateId[1]].bShown)
            {
                dc.DrawRectangle(LinePen, picState[picSelector.picStateId[1]].picRectangle);
            }
            if(picSelector.curSelector==1)
            {
                dc.DrawRectangle(LinePen, picState[picSelector.picStateId[0]].picRectangle);
            }
            //绘制连通线
            if(picSelector.bLinkOk)
            {
                dc.DrawRectangle(LinePen, picState[picSelector.picStateId[1]].picRectangle);
                dc.DrawLines(LinePen, picSelector.linkPoints);
            }
        }

        //强制重绘
        void force_redraw()
        {
            this.Invalidate(new Rectangle(0, 0, formSize.Width, formSize.Height));
        }

        //打乱图片布局
        void shuffle_pics()
        {
            for (int i = 0; i < ROWPICS * COLPICS; i++)
            {
                if (picState[i].bShown == false) continue;
                int t = picState[i].nPicId;
                int x = ran.Next(0, ROWPICS * COLPICS);

                if (picState[x].bShown == false)
                {
                    //如果随机到的图片已经消除就顺序找下一个
                    for (int j = i + 1; j < ROWPICS * COLPICS; j++)
                    {
                        if (picState[j].bShown)
                        {
                            x = j;
                            break;
                        }
                    }
                }

                picState[i].nPicId = picState[x].nPicId;
                picState[x].nPicId = t;
            }
            //强制重绘
            force_redraw();
        }
        //根据坐标计算图片状态数组下标
        int get_select_pic_idx(int x, int y)
        {
            int r = y / (PICSIZE + 2) + y % (PICSIZE + 2) % 1 - 1;
            if (r < 0 || r > COLPICS - 1) return -1;
            int l = x / (PICSIZE + 2) + x % (PICSIZE + 2) % 1 - 1;
            if (l < 0 || l > ROWPICS - 1) return -1;

            return r * ROWPICS + l;
        }
        //检测是否有可以匹配的图片
        bool check_pic_match()
        {
            for (int i = 0; i < picState.Length; i++)
            {
                if (picState[i].bShown)
                {
                    int id = picState[i].nPicId;
                    for (int j = i + 1; j < picState.Length; j++)
                    {
                        if (picState[j].bShown && picState[j].nPicId == id && is_pic_link(i, j))
                        {
                            return true;
                        }
                    }
                }
            }
            return false;
        }
        //检测是否通关
        bool check_win()
        {
            for (int i = 0; i < picState.Length; i++) 
            {
                if(picState[i].bShown)
                {
                    return false;
                }
            }
            return true;
        }
        //判断两个图片是否可联通
        bool is_pic_link(int stid1, int stid2)//picstate的下标
        {
            if (picState[stid1].nPicId != picState[stid2].nPicId) return false;
            int x1 = picState[stid1].sceneX;
            int y1 = picState[stid1].sceneY;
            int x2 = picState[stid2].sceneX;
            int y2 = picState[stid2].sceneY;
            //先把两个点设为可连通
            sceneState[x1, y1].bLinkable = true;
            sceneState[x2, y2].bLinkable = true;
            //x不变y递减
            if (!(x1 == x2 && y1 < y2))
            {
                for (int y = y1 - 1; y >= 0; y--)
                {
                    //一旦不能连通就放弃这个方向
                    if (!sceneState[x1, y].bLinkable) break;
                    //假定y可用时测试x方向连通
                    if (x1 == x2 || (x1 < x2 && is_link_line_x(x1, x2, y)) || (x1 > x2 && is_link_line_x(x2, x1, y)))
                    {
                        //x=x2时y方向连通
                        if (y == y2 || (y < y2 && is_link_line_y(y, y2, x2)) || (y2 < y && is_link_line_y(y2, y, x2)))
                        {

                            picSelector.linkPoints[0] = (sceneState[x1, y1].scenePoint);
                            picSelector.linkPoints[1] = (sceneState[x1, y].scenePoint);
                            picSelector.linkPoints[2] = (sceneState[x2, y].scenePoint);
                            picSelector.linkPoints[3] = (sceneState[x2, y2].scenePoint);
                            return true;
                        }
                    }
                }
            }
            //x不变y递增
            if (!(x1 == x2 && y1 > y2))
            {
                for (int y = y1 + 1; y < COLPICS + 2; y++)
                {
                    if (!sceneState[x1, y].bLinkable) break;
                    //假定y可用时测试x方向连通
                    if (x1 == x2 || (x1 < x2 && is_link_line_x(x1, x2, y)) || (x1 > x2 && is_link_line_x(x2, x1, y)))
                    {
                        //x=x2时y方向连通
                        if (y == y2 || (y < y2 && is_link_line_y(y, y2, x2)) || (y2 < y && is_link_line_y(y2, y, x2)))
                        {
                            picSelector.linkPoints[0] = (sceneState[x1, y1].scenePoint);
                            picSelector.linkPoints[1] = (sceneState[x1, y].scenePoint);
                            picSelector.linkPoints[2] = (sceneState[x2, y].scenePoint);
                            picSelector.linkPoints[3] = (sceneState[x2, y2].scenePoint);
                            return true;
                        }
                    }
                }
            }
            //y不变x递减
            if (!(y1 == y2 && x1 < x2))
            {
                for (int x = x1 - 1; x >= 0; x--)
                {
                    if (!sceneState[x, y1].bLinkable) break;
                    //假定x可用时测试y方向连通
                    if (y1 == y2 || (y1 < y2 && is_link_line_y(y1, y2, x)) || (y2 < y1 && is_link_line_y(y2, y1, x)))
                    {
                        //y=y2时测试x方向连通
                        if (x == x2 || (x < x2 && is_link_line_x(x, x2, y2)) || (x > x2 && is_link_line_x(x2, x, y2)))
                        {
                            picSelector.linkPoints[0] = (sceneState[x1, y1].scenePoint);
                            picSelector.linkPoints[1] = (sceneState[x, y1].scenePoint);
                            picSelector.linkPoints[2] = (sceneState[x, y2].scenePoint);
                            picSelector.linkPoints[3] = (sceneState[x2, y2].scenePoint);
                            return true;
                        }
                    }
                }
            }
            //y不变x递增
            if (!(y1 == y2 && x1 > x2))
            {
                for (int x = x1 + 1; x < ROWPICS + 2; x++)
                {
                    if (!sceneState[x, y1].bLinkable) break;
                    //假定x可用时测试y方向连通
                    if (y1 == y2 || (y1 < y2 && is_link_line_y(y1, y2, x)) || (y2 < y1 && is_link_line_y(y2, y1, x)))
                    {
                        //y=y2时测试x方向连通
                        if (x == x2 || (x < x2 && is_link_line_x(x, x2, y2)) || (x > x2 && is_link_line_x(x2, x, y2)))
                        {
                            picSelector.linkPoints[0] = (sceneState[x1, y1].scenePoint);
                            picSelector.linkPoints[1] = (sceneState[x, y1].scenePoint);
                            picSelector.linkPoints[2] = (sceneState[x, y2].scenePoint);
                            picSelector.linkPoints[3] = (sceneState[x2, y2].scenePoint);
                            return true;
                        }
                    }
                }
            }

            //不联通就设置回去
            sceneState[x1, y1].bLinkable = false;
            sceneState[x2, y2].bLinkable = false;

            return false;
        }
        //固定y测试x方向连通
        bool is_link_line_x(int x1, int x2, int y)//x1<x2
        {
            for (int x = x1; x <= x2; x++)
            {
                if (!sceneState[x, y].bLinkable) return false;
            }
            return true;
        }
        //固定x测试y方向联通
        bool is_link_line_y(int y1, int y2, int x)//x1<x2
        {
            for (int y = y1; y <= y2; y++)
            {
                if (!sceneState[x, y].bLinkable) return false;
            }
            return true;
        }

    }
}
