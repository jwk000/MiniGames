namespace GDI1
{
    partial class SettingForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SettingForm));
            this.btn_start = new System.Windows.Forms.Button();
            this.radio8 = new System.Windows.Forms.RadioButton();
            this.radio12 = new System.Windows.Forms.RadioButton();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.rdm8 = new System.Windows.Forms.RadioButton();
            this.radio8c = new System.Windows.Forms.RadioButton();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // btn_start
            // 
            this.btn_start.Location = new System.Drawing.Point(68, 185);
            this.btn_start.Name = "btn_start";
            this.btn_start.Size = new System.Drawing.Size(150, 48);
            this.btn_start.TabIndex = 0;
            this.btn_start.Text = "开始游戏";
            this.btn_start.UseVisualStyleBackColor = true;
            this.btn_start.Click += new System.EventHandler(this.btn_start_Click);
            // 
            // radio8
            // 
            this.radio8.AutoSize = true;
            this.radio8.Checked = true;
            this.radio8.Location = new System.Drawing.Point(21, 24);
            this.radio8.Name = "radio8";
            this.radio8.Size = new System.Drawing.Size(47, 16);
            this.radio8.TabIndex = 1;
            this.radio8.TabStop = true;
            this.radio8.Text = "初级";
            this.radio8.UseVisualStyleBackColor = true;
            // 
            // radio12
            // 
            this.radio12.AutoSize = true;
            this.radio12.Location = new System.Drawing.Point(21, 68);
            this.radio12.Name = "radio12";
            this.radio12.Size = new System.Drawing.Size(47, 16);
            this.radio12.TabIndex = 3;
            this.radio12.TabStop = true;
            this.radio12.Text = "高级";
            this.radio12.UseVisualStyleBackColor = true;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.radio8);
            this.groupBox1.Controls.Add(this.rdm8);
            this.groupBox1.Controls.Add(this.radio8c);
            this.groupBox1.Controls.Add(this.radio12);
            this.groupBox1.Location = new System.Drawing.Point(68, 34);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(150, 127);
            this.groupBox1.TabIndex = 5;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "选择难度";
            // 
            // rdm8
            // 
            this.rdm8.AutoSize = true;
            this.rdm8.Location = new System.Drawing.Point(21, 46);
            this.rdm8.Name = "rdm8";
            this.rdm8.Size = new System.Drawing.Size(47, 16);
            this.rdm8.TabIndex = 2;
            this.rdm8.TabStop = true;
            this.rdm8.Text = "中级";
            this.rdm8.UseVisualStyleBackColor = true;
            // 
            // radio8c
            // 
            this.radio8c.AutoSize = true;
            this.radio8c.Location = new System.Drawing.Point(21, 90);
            this.radio8c.Name = "radio8c";
            this.radio8c.Size = new System.Drawing.Size(71, 16);
            this.radio8c.TabIndex = 3;
            this.radio8c.TabStop = true;
            this.radio8c.Text = "练习模式";
            this.radio8c.UseVisualStyleBackColor = true;
            // 
            // SettingForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 261);
            this.Controls.Add(this.btn_start);
            this.Controls.Add(this.groupBox1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "SettingForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "设置";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btn_start;
        private System.Windows.Forms.RadioButton radio8;
        private System.Windows.Forms.RadioButton radio12;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.RadioButton rdm8;
        private System.Windows.Forms.RadioButton radio8c;
    }
}