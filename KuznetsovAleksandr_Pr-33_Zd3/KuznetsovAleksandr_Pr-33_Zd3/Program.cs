using System;
using System.Collections.Generic;
using System.IO;

namespace KuznetsovAleksandr_Pr_33_Zd3
{
    internal class Program
    {
        static void Main(string[] args)
        {
            try
            {
                List<string> values = new List<string>();

                using (StreamReader reader = new StreamReader("input.txt"))
                {
                    string line;
                    while ((line = reader.ReadLine()) != null)
                    {
                        values.Add(line);
                    }
                }

                int n = int.Parse(values[0]);
                string[] parts = values[1].Split(new char[] { ' ', '\t' }, StringSplitOptions.RemoveEmptyEntries);
                int total = n * 2;

                if (parts.Length != total)
                {
                    using (StreamWriter sw = new StreamWriter("output.txt"))
                    {
                        sw.WriteLine("Ошибка: количество диаметров не совпадает с 2n.");
                    }
                    return;
                }

                int[,] balls = new int[total, 2];
                for (int i = 0; i < total; i++)
                {
                    balls[i, 0] = int.Parse(parts[i]);
                    balls[i, 1] = i + 1;
                }

                for (int i = 0; i < total - 1; i++)
                {
                    for (int j = 0; j < total - i - 1; j++)
                    {
                        if (balls[j, 0] > balls[j + 1, 0])
                        {
                            int tempD = balls[j, 0];
                            int tempI = balls[j, 1];
                            balls[j, 0] = balls[j + 1, 0];
                            balls[j, 1] = balls[j + 1, 1];
                            balls[j + 1, 0] = tempD;
                            balls[j + 1, 1] = tempI;
                        }
                    }
                }

                int[,] pairs = new int[n, 2];
                for (int i = 0, k = 0; i < total; i += 2, k++)
                {
                    int bottom = balls[i + 1, 1]; 
                    int top = balls[i, 1];       
                    pairs[k, 0] = bottom;
                    pairs[k, 1] = top;
                }

                for (int i = 0; i < n - 1; i++)
                {
                    for (int j = 0; j < n - i - 1; j++)
                    {
                        if (pairs[j, 0] > pairs[j + 1, 0])
                        {
                            int temp1 = pairs[j, 0];
                            int temp2 = pairs[j, 1];
                            pairs[j, 0] = pairs[j + 1, 0];
                            pairs[j, 1] = pairs[j + 1, 1];
                            pairs[j + 1, 0] = temp1;
                            pairs[j + 1, 1] = temp2;
                        }
                    }
                }

                using (StreamWriter writer = new StreamWriter("output.txt"))
                {
                    for (int i = 0; i < n; i++)
                    {
                        writer.WriteLine(pairs[i, 0] + " " + pairs[i, 1]);
                    }
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message.ToString());
            }
        }
    }
}