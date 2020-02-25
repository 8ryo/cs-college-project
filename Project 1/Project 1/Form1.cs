using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Project_1
{
    public partial class Form1 : Form
    {
        OpenFileDialog openFileDialog1;
        public Form1()
        {
            InitializeComponent();
            openFileDialog1 = new OpenFileDialog()
            {
                FileName = "Select an Excel File",
                Filter = "Excel files (*.xlsx)|*.xlsx",
                Title = ""
            };

            //btnBrowse.Click += new EventHandler(BtnBrowse_Click_1);
            Controls.Add(btnBrowse);

        }

        private void Label1_Click(object sender, EventArgs e)
        {
            //Just text
        }

        private void BtnNext1_Click(object sender, EventArgs e)
        {
            this.Hide();
            Form2 frm2 = new Form2();
            frm2.ShowDialog();
        }

        private void BtnBrowse_Click_1(object sender, EventArgs e)
        {
            //Browse Dialogue
            //https://docs.microsoft.com/en-us/dotnet/framework/winforms/controls/how-to-open-files-using-the-openfiledialog-component
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    txtPath.Text = openFileDialog1.FileName;
                    txtPath.ReadOnly = true;
                    global.filepath = txtPath.Text;
                }

                catch
                {
                    MessageBox.Show("Error: Something went wrong :(\nTry again.");
                }
            }
        }

        private void BtnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            global.preload();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
