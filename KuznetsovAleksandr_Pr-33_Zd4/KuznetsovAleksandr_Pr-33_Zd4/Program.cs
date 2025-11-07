using System;
using System.Collections.Generic;
using System.IO;

namespace KuznetsovAleksandr_Pr_33_Zd4
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

                if (values.Count < 3)
                {
                    using (StreamWriter writer = new StreamWriter("output.txt"))
                    {
                        writer.WriteLine("Ошибка: неверный формат входных данных.");
                    }
                    return;
                }

                string[] lengths = values[0].Split(' ');

                string[] noteWords = values[1].Split(' ');

                string[] paperWords = values[2].Split(' ');

                Dictionary<string, int> paperCount = new Dictionary<string, int>();

                for (int i = 0; i < paperWords.Length; i++)
                {
                    string word = paperWords[i];
                    if (paperCount.ContainsKey(word))
                        paperCount[word]++;
                    else
                        paperCount[word] = 1;
                }

                bool canMake = true;
                string missingWord = "";

                for (int i = 0; i < noteWords.Length; i++)
                {
                    string word = noteWords[i];

                    if (paperCount.ContainsKey(word) && paperCount[word] > 0)
                    {
                        paperCount[word]--;
                    }
                    else
                    {
                        canMake = false;
                        missingWord = word;
                        break;
                    }
                }

                using (StreamWriter writer = new StreamWriter("output.txt"))
                {
                    if (canMake)
                        writer.WriteLine("GOOD NOTE");
                    else
                        writer.WriteLine(missingWord);
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message.ToString());
            }
        }
    }
}