namespace MineSweeper
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
            this.UITimer = new System.Windows.Forms.Timer(this.components);
            this.UIUseTime = new System.Windows.Forms.Label();
            this.UIConfig = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // UIUseTime
            // 
            this.UIUseTime.AutoSize = true;
            this.UIUseTime.Font = new System.Drawing.Font("Microsoft YaHei", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.UIUseTime.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(0)))));
            this.UIUseTime.Location = new System.Drawing.Point(13, 13);
            this.UIUseTime.Name = "UIUseTime";
            this.UIUseTime.Size = new System.Drawing.Size(144, 17);
            this.UIUseTime.TabIndex = 0;
            this.UIUseTime.Text = "本局已使用时间：00：00";
            // 
            // UIConfig
            // 
            this.UIConfig.AutoSize = true;
            this.UIConfig.Font = new System.Drawing.Font("Microsoft YaHei", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.UIConfig.ForeColor = System.Drawing.Color.Blue;
            this.UIConfig.Location = new System.Drawing.Point(181, 13);
            this.UIConfig.Name = "UIConfig";
            this.UIConfig.Size = new System.Drawing.Size(32, 17);
            this.UIConfig.TabIndex = 1;
            this.UIConfig.Text = "简单";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 262);
            this.Controls.Add(this.UIConfig);
            this.Controls.Add(this.UIUseTime);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Form1";
            this.Text = "扫雷";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Timer UITimer;
        private System.Windows.Forms.Label UIUseTime;
        private System.Windows.Forms.Label UIConfig;
    }
}

