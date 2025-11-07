using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KuznetsovAleksandr_Pr_33_Zd5
{
    internal class Program
    {
        static void Main(string[] args)
        {
            int N = 0;
            int v = 0;
            int t = 0;
            List<string> values = new List<string>();
            try
            {
                using (StreamReader reader = new StreamReader("input.txt"))
                {
                    string line;
                    while ((line = reader.ReadLine()) != null)
                    {
                        values.Add(line);
                    }
                }

                N = int.Parse(values[0]);

                string[] values2 = values[1].Split(' ');

                v = int.Parse(values2[0]);
                t = int.Parse(values2[1]);


                int result = v * t;

                if(result == N) result = 0;

                using(StreamWriter writer = new StreamWriter("output.txt"))
                {
                    writer.WriteLine(result.ToString());
                }


                Console.ReadKey();

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message.ToString());
            }

        }
    }
}
