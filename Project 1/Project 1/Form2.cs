using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Text.RegularExpressions;

namespace Project_1
{
    public partial class Form2 : Form
    {
        Form1 frm1 = new Form1();
        public Form2()
        {
            InitializeComponent();
        }

        private void Form2_Load(object sender, EventArgs e)
        {

        }

        private void BtnBack2_Click(object sender, EventArgs e)
        {
            this.Hide();
            frm1.ShowDialog();
        }

        private void btnNext2_Click(object sender, EventArgs e)
        {
            double[] processlen = connect.getval(txtFirstCell.Text, txtLastCell.Text, global.filepath);

            if (Convert.ToString(txtFirstCell.Text) == "" || Convert.ToString(txtLastCell) == "" || Convert.ToString(txtCycles) == "")
            {
                MessageBox.Show("Error: Not all fields have a valid entry.");
            }
            else if (processlen[0] != -1)
            {

            }
            else
            {
                global.arrProcesslen = processlen;
                global.cycles = Convert.ToInt32(txtCycles.Text);
                this.Hide();
                Form3 frm3 = new Form3();
                frm3.ShowDialog();
            }
        }

        private void axWindowsMediaPlayer1_Enter(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }
    }
}
