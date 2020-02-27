using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Office.Interop.Excel;
using _Excel = Microsoft.Office.Interop.Excel;

namespace Connecting_to_excel
{
    class Excel
    {
        _Application excel = new _Excel.Application();
        Workbook wb;
        Worksheet ws;
        public Excel(string path, int Sheet)
        {
            wb = excel.Workbooks.Open(path);
            ws = wb.Worksheets[Sheet];
        }

        public string readcell(int i, int j)
        {
            //i++;
            //j++;
            if (ws.Cells[i, j].Value2 != null)
                return ws.Cells[i, j].Value2;
            else
                return "";

        }
    }
    class Program
    {
        static double convert(string str)
        {
            double x = double.Parse(str);
            return x;
        }
        static void Main(string[] args)
        {
            string path = @"C:\Users\Bryan\source\repos\test2.xlsx";
            Excel excel = new Excel(path, 1);


            Console.WriteLine(excel.readcell(0,0));
            

            // Index starts at 0 here
            int firstx = 1;
            int firsty = 2;

            int lastx = 3;
            int lasty = 5;

            int[] processlen = new int[lasty - firsty + 2];

            Console.WriteLine(excel.readcell(1, 1));

            Console.WriteLine(excel.readcell(3, 2)); //change

            for (int i = 0; i < processlen.Length; i++)
            {
                int x = Convert.ToInt32(excel.readcell(lastx, i+firsty));
                Console.WriteLine(x);
            }


            Console.WriteLine("{0}", string.Join(", ", processlen));



            Console.ReadLine();
        }

        
    }
}
