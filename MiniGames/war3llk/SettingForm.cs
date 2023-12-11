using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GDI1
{
    public partial class SettingForm : Form
    {
        public SettingForm()
        {
            InitializeComponent();
            this.FormBorderStyle = FormBorderStyle.Fixed3D;
        }

        private void btn_start_Click(object sender, EventArgs e)
        {
            if (radio8.Checked)
            {
                Form1.ROWPICS = 8;
                Form1.COLPICS = 8;
                Form1.SAMERATIO = 4;
            }
            if (radio12.Checked)
            {
                Form1.ROWPICS = 12;
                Form1.COLPICS = 8;
                Form1.SAMERATIO = 4;
            }
            if (rdm8.Checked)
            {
                Form1.ROWPICS = 8;
                Form1.COLPICS = 8;
                Form1.SAMERATIO = 2;
            }
            if(radio8c.Checked)
            {
                Form1.ROWPICS = 8;
                Form1.COLPICS = 8;
                Form1.SAMERATIO = 4;
                Form1.PRACTICE = true;
            }

            Form1 GameForm = new Form1();
            GameForm.Show();
            GameForm.MainForm = this;
            this.Hide();
        }
    }
}
