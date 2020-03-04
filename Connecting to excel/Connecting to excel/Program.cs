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

        public string readcell(int j, int i)
        {
            i++;
            j++;
            if (ws.Cells[i, j].Value2 != null)
                return Convert.ToString(ws.Cells[i, j].Value2);
            else
                return "";

        }
    }
    class Program
    {
        public static int ExcelColumnNameToNumber(string columnName)
        {
            if (string.IsNullOrEmpty(columnName)) throw new ArgumentNullException("columnName");

            columnName = columnName.ToUpperInvariant();

            int sum = 0;

            for (int i = 0; i < columnName.Length; i++)
            {
                sum *= 26;
                sum += (columnName[i] - 'A' + 1);
            }

            return sum;
        }
        static double convert(string str)
        {
            double x = double.Parse(str);
            return x;
        }

        static bool check(string co)
        {

        }


        static void Main(string[] args)
        {
            string path = @"H:\CS Project\cs-college-project-master\test2.xlsx";
            Excel excel = new Excel(path, 1);

            string coordinate = "A1";

        

            Console.WriteLine(ExcelColumnNameToNumber(""));

            // Index starts at 0 here
            int firstx = 1;
            int firsty = 2;

            int lastx = 3;
            int lasty = 5;

            string[] strings = new string[lasty - firsty + 1]; 

            for (int i = 0; i < strings.Length; i++)
            {
                strings[i] = excel.readcell(lastx, i + firsty);
            }

            double[] processlen = Array.ConvertAll(strings, double.Parse);

            Console.WriteLine("[{0}]", string.Join(", ", processlen));

            Console.ReadLine();
        }

        
    }
}
