using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace project_1_back
{
    class Program
    {
        static void Main(string[] args)
        {
            int[] arrProcesslen = new int[] { 4, 6, 3 };
            int cycles = 4;
            int pointer = 0;
            int[,] arrRun = new int[arrProcesslen.Length, cycles];
            int[,] arrIdle = new int[arrProcesslen.Length, cycles];
            int[,] arrBlocked = new int[arrProcesslen.Length, cycles];
            int noprocess = arrProcesslen.Length;

            //------------------------ First Cycle ---------------------------------

            // ([i,a] where i is the process, a is the current cycle)

            for (int i = 0; i < noprocess; i++)
            {
                arrRun[i, 0] = pointer + arrProcesslen[i];
                pointer = arrRun[i, 0];
            }

            //--------------------------------- Second Cycle ---------------------------------
            for (int i = 0; i < noprocess; i++) //Remember to be setting to [i,1]
            {
                pointer = arrRun[i, 0]; //The end of the previous block on the current process
                if (i == 0)
                {
                    arrRun[i, 1] = arrProcesslen[i] + pointer; //end of current run block at the top
                }
                else
                {
                    if (arrRun[i, 0] < arrRun[i - 1, 1])
                    {
                        arrIdle[i, 1] = pointer + (arrRun[i - 1, 1] - arrRun[i, 0]);
                        arrRun[i, 1] = arrIdle[i, 1] + arrProcesslen[i];
                    }
                    else
                    {
                        arrRun[i, 1] = pointer + arrProcesslen[i];
                    }
                }
            }

            //--------------------------------- Third cycle onwards ---------------------------------

            for (int a = 2; a < cycles; a++)
            {
                for (int i = 0; i < noprocess; i++)
                {
                    pointer = arrRun[i, a - 1];
                    /*
                     * I'm going to make the rule that the pointer should always stay on
                     * the previous cycle of the current process.
                    */
                    if (i == 0) // First process
                    {
                        if (arrRun[i + 1, a - 2] > pointer) //blocked
                        {
                            arrBlocked[i, a] = arrRun[i + 1, a - 2];
                            arrRun[i, a] = arrBlocked[i, a] + arrProcesslen[i];
                        }
                        else
                        {
                            arrRun[i, a] = pointer + arrProcesslen[i];
                        }
                    }
                    else if (i == arrProcesslen.Length) // Last process
                    {
                        if (arrRun[i - 1, a] > arrRun[i, a - 1]) //idle
                        {
                            arrIdle[i, a] = arrRun[i - 1, a];
                            arrRun[i, a] = arrIdle[i, a] + arrProcesslen[i];
                        }
                        else
                        {
                            arrRun[i, a] = pointer + arrProcesslen[i];
                        }
                    }
                    else if (i != arrProcesslen.Length) // Any in between
                    {
                        try
                        {
                            if (arrRun[i + 1, a - 2] > pointer) //blocked
                            {
                                arrBlocked[i, a] = arrRun[i + 1, a - 2];
                                arrRun[i, a] = arrBlocked[i, a] + arrProcesslen[i];
                            }
                            else if (arrRun[i - 1, a] > arrRun[i, a - 1]) //idle
                            {
                                arrIdle[i, a] = arrRun[i - 1, a];
                                arrRun[i, a] = arrIdle[i, a] + arrProcesslen[i];
                            }
                            else if ((arrRun[i - 1, a] > arrRun[i, a - 1]) && (arrRun[i - 1, a] > arrRun[i, a - 1]))
                            {
                                arrBlocked[i, a] = pointer + (arrRun[i + 1, a - 2] - pointer);
                                arrIdle[i, a] = arrRun[i - 1, a];
                                if (arrIdle[i, a] > arrBlocked[i, a])
                                {
                                    arrRun[i, a] = arrProcesslen[i] - arrIdle[i, a];
                                }
                                else
                                {
                                    arrRun[i, a] = arrProcesslen[i] - arrBlocked[i, a];
                                }
                            }
                            else
                            {
                                arrRun[i, a] = pointer + arrProcesslen[i];
                            }
                        }

                        catch
                        {
                            if (arrRun[i - 1, a] > arrRun[i, a - 1]) //idle
                            {
                                arrIdle[i, a] = arrRun[i - 1, a];
                                arrRun[i, a] = arrIdle[i, a] + arrProcesslen[i];
                            }
                            else if ((arrRun[i - 1, a] > arrRun[i, a - 1]) && (arrRun[i - 1, a] > arrRun[i, a - 1]))
                            {
                                arrBlocked[i, a] = pointer + (arrRun[i + 1, a - 2] - pointer);
                                arrIdle[i, a] = arrRun[i - 1, a];
                                if (arrIdle[i, a] > arrBlocked[i, a])
                                {
                                    arrRun[i, a] = arrProcesslen[i] - arrIdle[i, a];
                                }
                                else
                                {
                                    arrRun[i, a] = arrProcesslen[i] - arrBlocked[i, a];
                                }
                            }
                            else
                            {
                                arrRun[i, a] = pointer + arrProcesslen[i];
                            }
                        }
                    }
                }
            }

            Console.WriteLine("-------------Blocked:----------------");
            Print2DArray(arrBlocked);
            Console.WriteLine("-------------Idle:----------------");
            Print2DArray(arrIdle);
            Console.WriteLine("-------------Run:----------------");
            Print2DArray(arrRun);
            Console.ReadLine();

            // I can just add all of the values in blocked and idle together. The run time should be the same, inside or outside of the loop

            // If this is completely unnecessary, I can just remove it later. For now, I will keep on doing this to cover my bases.

            int[] arrTotalBlocked = new int[noprocess];
            int[] arrTotalRun = new int[noprocess];
            int[] arrTotalIdle = new int[noprocess];

            /*
              So I am populating each element of the array with the sum of all of the elements 
              in the current process of the run idle blocked arrays
            */

            for (int i = 0; i < noprocess; i++)
            {
                arrTotalBlocked[i] = arrBlocked.Cast<int>().Sum();
            }

            Console.WriteLine(arrTotalBlocked);

        }
        public static void Print2DArray<T>(T[,] matrix)
        {
            for (int i = 0; i < matrix.GetLength(0); i++)
            {
                for (int j = 0; j < matrix.GetLength(1); j++)
                {
                    Console.Write(matrix[i, j] + "\t");
                }
                Console.WriteLine();
            }
        }
    }
}
