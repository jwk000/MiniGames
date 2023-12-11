using System.Drawing;
using System.Drawing.Drawing2D;

namespace GDI1
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.GameTimer = new System.Windows.Forms.Timer(this.components);
            this.picList = new System.Windows.Forms.ImageList(this.components);
            this.bar_time = new System.Windows.Forms.ProgressBar();
            this.lbl_lefttime = new System.Windows.Forms.Label();
            this.lbl_win = new System.Windows.Forms.Label();
            this.lbl_nomatch = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // picList
            // 
            this.picList.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("picList.ImageStream")));
            this.picList.TransparentColor = System.Drawing.Color.Transparent;
            this.picList.Images.SetKeyName(0, "war (1).gif");
            this.picList.Images.SetKeyName(1, "war (2).gif");
            this.picList.Images.SetKeyName(2, "war (3).gif");
            this.picList.Images.SetKeyName(3, "war (4).gif");
            this.picList.Images.SetKeyName(4, "war (5).gif");
            this.picList.Images.SetKeyName(5, "war (6).gif");
            this.picList.Images.SetKeyName(6, "war (7).gif");
            this.picList.Images.SetKeyName(7, "war (8).gif");
            this.picList.Images.SetKeyName(8, "war (9).gif");
            this.picList.Images.SetKeyName(9, "war (10).gif");
            this.picList.Images.SetKeyName(10, "war (11).gif");
            this.picList.Images.SetKeyName(11, "war (12).gif");
            this.picList.Images.SetKeyName(12, "war (13).gif");
            this.picList.Images.SetKeyName(13, "war (14).gif");
            this.picList.Images.SetKeyName(14, "war (15).gif");
            this.picList.Images.SetKeyName(15, "war (16).gif");
            this.picList.Images.SetKeyName(16, "war (17).gif");
            this.picList.Images.SetKeyName(17, "war (18).gif");
            this.picList.Images.SetKeyName(18, "war (19).gif");
            this.picList.Images.SetKeyName(19, "war (20).gif");
            this.picList.Images.SetKeyName(20, "war (21).gif");
            this.picList.Images.SetKeyName(21, "war (22).gif");
            this.picList.Images.SetKeyName(22, "war (23).gif");
            this.picList.Images.SetKeyName(23, "war (24).gif");
            this.picList.Images.SetKeyName(24, "war (25).gif");
            this.picList.Images.SetKeyName(25, "war (26).gif");
            this.picList.Images.SetKeyName(26, "war (27).gif");
            this.picList.Images.SetKeyName(27, "war (28).gif");
            this.picList.Images.SetKeyName(28, "war (29).gif");
            this.picList.Images.SetKeyName(29, "war (30).gif");
            this.picList.Images.SetKeyName(30, "war (31).gif");
            this.picList.Images.SetKeyName(31, "war (32).gif");
            this.picList.Images.SetKeyName(32, "war (33).gif");
            this.picList.Images.SetKeyName(33, "war (34).gif");
            this.picList.Images.SetKeyName(34, "war (35).gif");
            this.picList.Images.SetKeyName(35, "war (36).gif");
            this.picList.Images.SetKeyName(36, "war (37).gif");
            this.picList.Images.SetKeyName(37, "war (38).gif");
            this.picList.Images.SetKeyName(38, "war (39).gif");
            this.picList.Images.SetKeyName(39, "war (40).gif");
            this.picList.Images.SetKeyName(40, "war (41).gif");
            this.picList.Images.SetKeyName(41, "war (42).gif");
            this.picList.Images.SetKeyName(42, "war (43).gif");
            this.picList.Images.SetKeyName(43, "war (44).gif");
            this.picList.Images.SetKeyName(44, "war (45).gif");
            this.picList.Images.SetKeyName(45, "war (46).gif");
            this.picList.Images.SetKeyName(46, "war (47).gif");
            this.picList.Images.SetKeyName(47, "war (48).gif");
            this.picList.Images.SetKeyName(48, "war (49).gif");
            this.picList.Images.SetKeyName(49, "war (50).gif");
            this.picList.Images.SetKeyName(50, "war (51).gif");
            this.picList.Images.SetKeyName(51, "war (52).gif");
            this.picList.Images.SetKeyName(52, "war (53).gif");
            this.picList.Images.SetKeyName(53, "war (54).gif");
            this.picList.Images.SetKeyName(54, "war (55).gif");
            this.picList.Images.SetKeyName(55, "war (56).gif");
            this.picList.Images.SetKeyName(56, "war (57).gif");
            this.picList.Images.SetKeyName(57, "war (58).gif");
            this.picList.Images.SetKeyName(58, "war (59).gif");
            this.picList.Images.SetKeyName(59, "war (60).gif");
            this.picList.Images.SetKeyName(60, "war (61).gif");
            this.picList.Images.SetKeyName(61, "war (62).gif");
            this.picList.Images.SetKeyName(62, "war (63).gif");
            this.picList.Images.SetKeyName(63, "war (64).gif");
            this.picList.Images.SetKeyName(64, "war (65).gif");
            this.picList.Images.SetKeyName(65, "war (66).gif");
            this.picList.Images.SetKeyName(66, "war (67).gif");
            this.picList.Images.SetKeyName(67, "war (68).gif");
            this.picList.Images.SetKeyName(68, "war (69).gif");
            this.picList.Images.SetKeyName(69, "war (70).gif");
            this.picList.Images.SetKeyName(70, "war (71).gif");
            this.picList.Images.SetKeyName(71, "war (72).gif");
            this.picList.Images.SetKeyName(72, "war (73).gif");
            this.picList.Images.SetKeyName(73, "war (74).gif");
            this.picList.Images.SetKeyName(74, "war (75).gif");
            this.picList.Images.SetKeyName(75, "war (76).gif");
            this.picList.Images.SetKeyName(76, "war (77).gif");
            this.picList.Images.SetKeyName(77, "war (78).gif");
            this.picList.Images.SetKeyName(78, "war (79).gif");
            this.picList.Images.SetKeyName(79, "war (80).gif");
            this.picList.Images.SetKeyName(80, "war (81).gif");
            this.picList.Images.SetKeyName(81, "war (82).gif");
            this.picList.Images.SetKeyName(82, "war (83).gif");
            this.picList.Images.SetKeyName(83, "war (84).gif");
            this.picList.Images.SetKeyName(84, "war (85).gif");
            this.picList.Images.SetKeyName(85, "war (86).gif");
            this.picList.Images.SetKeyName(86, "war (87).gif");
            this.picList.Images.SetKeyName(87, "war (88).gif");
            this.picList.Images.SetKeyName(88, "war (89).gif");
            this.picList.Images.SetKeyName(89, "war (90).gif");
            this.picList.Images.SetKeyName(90, "war (91).gif");
            this.picList.Images.SetKeyName(91, "war (92).gif");
            this.picList.Images.SetKeyName(92, "war (93).gif");
            this.picList.Images.SetKeyName(93, "war (94).gif");
            this.picList.Images.SetKeyName(94, "war (95).gif");
            this.picList.Images.SetKeyName(95, "war (96).gif");
            this.picList.Images.SetKeyName(96, "war (97).gif");
            this.picList.Images.SetKeyName(97, "war (98).gif");
            this.picList.Images.SetKeyName(98, "war (99).gif");
            this.picList.Images.SetKeyName(99, "war (100).gif");
            this.picList.Images.SetKeyName(100, "war (101).gif");
            this.picList.Images.SetKeyName(101, "war (102).gif");
            this.picList.Images.SetKeyName(102, "war (103).gif");
            this.picList.Images.SetKeyName(103, "war (104).gif");
            this.picList.Images.SetKeyName(104, "war (105).gif");
            this.picList.Images.SetKeyName(105, "war (106).gif");
            this.picList.Images.SetKeyName(106, "war (107).gif");
            this.picList.Images.SetKeyName(107, "war (108).gif");
            this.picList.Images.SetKeyName(108, "war (109).gif");
            this.picList.Images.SetKeyName(109, "war (110).gif");
            this.picList.Images.SetKeyName(110, "war (111).gif");
            this.picList.Images.SetKeyName(111, "war (112).gif");
            this.picList.Images.SetKeyName(112, "war (113).gif");
            this.picList.Images.SetKeyName(113, "war (114).gif");
            this.picList.Images.SetKeyName(114, "war (115).gif");
            this.picList.Images.SetKeyName(115, "war (116).gif");
            this.picList.Images.SetKeyName(116, "war (117).gif");
            this.picList.Images.SetKeyName(117, "war (118).gif");
            this.picList.Images.SetKeyName(118, "war (119).gif");
            this.picList.Images.SetKeyName(119, "war (120).gif");
            this.picList.Images.SetKeyName(120, "war (121).gif");
            this.picList.Images.SetKeyName(121, "war (122).gif");
            this.picList.Images.SetKeyName(122, "war (123).gif");
            this.picList.Images.SetKeyName(123, "war (124).gif");
            this.picList.Images.SetKeyName(124, "war (125).gif");
            this.picList.Images.SetKeyName(125, "war (126).gif");
            this.picList.Images.SetKeyName(126, "war (127).gif");
            this.picList.Images.SetKeyName(127, "war (128).gif");
            // 
            // bar_time
            // 
            this.bar_time.BackColor = System.Drawing.Color.White;
            this.bar_time.ForeColor = System.Drawing.Color.Blue;
            this.bar_time.Location = new System.Drawing.Point(76, 12);
            this.bar_time.Name = "bar_time";
            this.bar_time.Size = new System.Drawing.Size(369, 12);
            this.bar_time.TabIndex = 0;
            // 
            // lbl_lefttime
            // 
            this.lbl_lefttime.AutoSize = true;
            this.lbl_lefttime.Location = new System.Drawing.Point(12, 12);
            this.lbl_lefttime.Name = "lbl_lefttime";
            this.lbl_lefttime.Size = new System.Drawing.Size(47, 12);
            this.lbl_lefttime.TabIndex = 1;
            this.lbl_lefttime.Text = "剩余0秒";
            // 
            // lbl_win
            // 
            this.lbl_win.AutoSize = true;
            this.lbl_win.Location = new System.Drawing.Point(108, 176);
            this.lbl_win.Name = "lbl_win";
            this.lbl_win.Size = new System.Drawing.Size(221, 12);
            this.lbl_win.TabIndex = 2;
            this.lbl_win.Text = "恭喜通关！点击关闭按钮可以重新开局！";
            // 
            // lbl_nomatch
            // 
            this.lbl_nomatch.AutoSize = true;
            this.lbl_nomatch.Font = new System.Drawing.Font("SimSun", 26F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lbl_nomatch.ForeColor = System.Drawing.Color.Red;
            this.lbl_nomatch.Location = new System.Drawing.Point(70, 9);
            this.lbl_nomatch.Name = "lbl_nomatch";
            this.lbl_nomatch.Size = new System.Drawing.Size(339, 35);
            this.lbl_nomatch.TabIndex = 3;
            this.lbl_nomatch.Text = "没有能匹配的图片了";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(457, 372);
            this.Controls.Add(this.lbl_nomatch);
            this.Controls.Add(this.lbl_win);
            this.Controls.Add(this.lbl_lefttime);
            this.Controls.Add(this.bar_time);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Form1";
            this.Text = "魔兽连连看-by jwk";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Timer GameTimer;
        private System.Windows.Forms.ImageList picList;
        private System.Windows.Forms.ProgressBar bar_time;
        private System.Windows.Forms.Label lbl_lefttime;
        private System.Windows.Forms.Label lbl_win;
        private System.Windows.Forms.Label lbl_nomatch;
    }
}

