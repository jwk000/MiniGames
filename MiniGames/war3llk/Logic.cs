using System;
using System.Drawing;

namespace Game
{

    public class PicState
    {
        public bool bShown = true;
        public int nPicId = 0;//图片id
        public Rectangle picRectangle;//绘图矩形
        public int sceneX = 0;
        public int sceneY = 0;
    }

    public class SceneState
    {
        public bool bLinkable = true;
        public Point scenePoint;
    }

    public class PicSelect
    {
        public bool bLinkOk = false;
        public int[] picStateId = new int[2];//被选择的两个图片的下标
        public int curSelector = 0;//当前要选择那个图片（0、1）
        public Point[] linkPoints = new Point[4];//最多有4个点

    }

    public class Logic
    {
        public Logic()
        {
        }
    }

}
