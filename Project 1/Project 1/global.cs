using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project_1
{
    public static class global
    {
        public static string filepath;

        public static void preload()
        {
            Form2 f2 = new Form2();
            Form3 f3 = new Form3();

            f2.Show();
            f3.Show();
            f2.Hide();
            f3.Hide();

        }
    }
}
