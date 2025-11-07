using System;
using System.IO;

namespace KuznetsovAlekasndr_Pr_33_Zd1
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string line;
            try
            {
                using (StreamReader sr = new StreamReader("input.txt"))
                {
                    line = sr.ReadLine();
                }

                string[] strings = line.Split(' ');

                int countR= int.Parse(strings[0]);
                int countF = int.Parse(strings[1]);

                if(countR < 1 || countR >100)
                {
                    using (StreamWriter sw = new StreamWriter("output.txt"))
                    {
                        sw.WriteLine("Не соответствует условию (1 ⩽ k ⩽ 100)");
                    }
                    return;
                }

                if (countF < 0 || countF > 10001) 
                {
                    using (StreamWriter sw = new StreamWriter("output.txt"))
                    {
                        sw.WriteLine("Не соответствует условию (0 ⩽ n ⩽ 10000)");
                    }
                    return;
                }

                int result = countF / countR;

                using(StreamWriter sw = new StreamWriter("output.txt"))
                {
                    sw.WriteLine(result.ToString());
                }
            }
            catch (Exception ex) {
                Console.WriteLine(ex.Message.ToString());
            }
        }
    }
}
