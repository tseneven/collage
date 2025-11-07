using System;
using System.Collections.Generic;
using System.IO;

namespace KuznetsovAleksandr_Pr_33_Zd2
{
    internal class Program
    {
        static void Main(string[] args)
        {
            try
            {
                List<string> values = new List<string>();

                using (StreamReader sr = new StreamReader("input.txt"))
                {
                    string line;
                    while ((line = sr.ReadLine()) != null)
                    {
                        values.Add(line);
                    }
                }

                int count = int.Parse(values[0]);

                if(count < 1 || count > 20)
                {
                    using (StreamWriter sw = new StreamWriter("output.txt"))
                    {
                        sw.WriteLine("Несоответствие формату (1 ⩽ n ⩽ 20)");
                    }
                }

                for(int i = 1; i < count+1; i++)
                {
                    if (values[i].Length < 1 || values[i].Length > 255)
                    {
                        using (StreamWriter sw = new StreamWriter("output.txt"))
                        {
                            sw.WriteLine("Несоответствие формату (1 < word < 255)");
                        }
                        return;
                    }
                }

                using (StreamWriter sw = new StreamWriter("output.txt"))
                {
                    for (int i = 1; i <= count; i++)
                    {
                        string word = values[i];
                        bool isPalindrome = true;

                        for (int j = 0; j < word.Length / 2; j++)
                        {
                            if (word[j] != word[word.Length - j - 1])
                            {
                                isPalindrome = false;
                                break;
                            }
                        }

                        sw.WriteLine(isPalindrome ? "YES" : "NO");
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