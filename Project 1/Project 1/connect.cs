using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Office.Interop.Excel;
using _Excel = Microsoft.Office.Interop.Excel;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace Project_1
{
    class Excel
    {
        _Application excel = new _Excel.Application();
        Workbook wb;
        public readonly Worksheet ws;
        public Excel(string path, int Sheet)
        {
            wb = excel.Workbooks.Open(path);
            ws = wb.Worksheets[Sheet];
        }

        public string readcell(int x, int y)
        {
            y++;
            x++;
            if (ws.Cells[y, x].Value2 != null) return Convert.ToString(ws.Cells[y, x].Value2);

            //Note, it is "y and x" rather than "x and y" because this is how excel interprets the integers. Really odd...

            else return "";
        }
    }
    public static class connect
    {
        static int ExcelColumnNameToNumber(string columnName)
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

        static bool validatecell(string input) // checking excel values now work !!!
        {
            bool alphafound = false;
            bool numfound = false;
            bool success = true;

            foreach (char c in input)
            {
                if (Regex.IsMatch(c.ToString(), @"^[a-zA-Z]+$") == true && alphafound == false) //(c.ToString(), @"\d")
                {
                    alphafound = true;
                }
                else if (c == Convert.ToChar(0) && numfound == false && alphafound == true)
                {
                    success = false;
                }
                else if (char.IsDigit(c) == true && numfound == false)
                {
                    numfound = true;
                }
                else if ((char.IsDigit(c) == true && alphafound == false) || (Regex.IsMatch(c.ToString(), @"^[a-zA-Z]+$") == true && numfound == true) ||
                     (c == Convert.ToChar(" ")))
                {
                    success = false;
                }
            }

            if (alphafound == true && numfound == true && success == true)
            {
                return true;
            }
            else if (alphafound == true && numfound == false || alphafound == false && numfound == true)
            {
                MessageBox.Show("Error: Coordinate does not contain both horizontal and vertical coordinate of cell.");
                return false;
            }
            else
            {
                MessageBox.Show("Error, invalid cell coordinates. Please input the coordinate in the form 'XY' (where X is " +
                    "the horizontal coordinate [i.e. the letter] and Y is the vertical coordinate [i.e. the number]. E.g. 'A3', 'Z4', 'AE14'." +
                    "[Y > 0][No spaces]. ");
                return false;
            }
        }

        static int[] splitandconvert(string input)
        {
            Regex re = new Regex(@"([a-zA-Z]+)(\d+)");
            Match result = re.Match(input);

            string alphaPart = result.Groups[1].Value;
            string numberPart = result.Groups[2].Value;

            int x = ExcelColumnNameToNumber(alphaPart);

            int[] arr = new int[] { x, Convert.ToInt32(numberPart) };
            return arr;
        }


        public static double[] getval(string input1, string input2, string path) //First cell and last cell of the range, respectively.
        {
            Excel excel = new Excel(path, 1);

            if (validatecell(input1) && validatecell(input2))
            {
                int[] firstcell = splitandconvert(input1);
                int[] lastcell = splitandconvert(input2);
                if (lastcell[0] > firstcell[0] && lastcell[1] > firstcell[1])
                {
                    int x1 = firstcell[0] - 1;
                    int y1 = firstcell[1] - 1;
                    int x2 = lastcell[0] - 1;
                    int y2 = lastcell[1] - 1;
                    string[] strings = new string[y2 - y1 + 1];

                    for (int i = 0; i < strings.Length; i++)
                    {
                        strings[i] = Convert.ToString(excel.readcell(x2, i + y1));
                    }

                    try
                    {
                        double[] processlen = Array.ConvertAll(strings, double.Parse);
                        return processlen;
                    }
                    catch
                    {
                        MessageBox.Show("Error: Not all of the values in the last column are numerical. Make sure " +
                            "all of the right column values are numerical and try again.");
                    }
                }
                else
                {
                    MessageBox.Show("Range is invalid, please make sure that the last cell is below and to the right of the first cell.");
                }
            }
            else
            {
                
            }

            double[] error = { -1 };
            return error;
        }
    }
}
