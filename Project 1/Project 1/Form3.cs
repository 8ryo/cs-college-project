using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Project_1
{
    public partial class Form3 : Form
    {
        public Form3()
        {
            InitializeComponent();
        }

        private void Form3_Load(object sender, EventArgs e)
        {
            this.Show();
            for (int i = 0; i < 100; i++)
            {
                progressBar1.Value += 1;
            }
            btnLoaded.Visible = true;
            label1.Text = "Complete!";
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Hide();
            Results nextform = new Results();
            nextform.ShowDialog();
        }

        private void label1_Click(object sender, EventArgs e)
        {
            //na
        }
    }
}
