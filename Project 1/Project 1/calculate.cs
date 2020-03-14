using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project_1
{
    public static class calculate
    {
        public static void rib(double[] arrProcesslen, int cycles)
        {
            double pointer = 0;
            double[,] arrRun = new double[arrProcesslen.Length, cycles];
            double[,] arrIdle = new double[arrProcesslen.Length, cycles];
            double[,] arrBlocked = new double[arrProcesslen.Length, cycles];
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
            //
            //
            double[] arrTotalBlocked = new double[noprocess];
            double[] arrTotalRun = new double[noprocess];
            double[] arrTotalIdle = new double[noprocess];

            for (int a = 0; a < noprocess; a++)
            {
                for (int i = 0; i < cycles; i++)
                {
                    arrTotalBlocked[a] += arrBlocked[a, i];
                    arrTotalRun[a] += arrRun[a, i];
                    arrTotalIdle[a] += arrIdle[a, i];
                }
            }

            int timemult = 1;
            int outmult = 1;

            double outputFrequency = ((double)arrRun[noprocess, cycles] * timemult) / (cycles * outmult);
            double outputRate = (cycles * outmult) / ((double)arrRun[noprocess, cycles] * timemult);

        }

    }
}
