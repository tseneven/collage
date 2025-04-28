using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace prKol_ind2_Кузнецов_а_Пр_23
{
    class Program
    {
        static void Main(string[] args)
        {
            if (File.Exists("txt.txt"))
            {
                Queue<string[]> famale = new Queue<string[]>();
                Queue<string[]> male = new Queue<string[]>();
                List<string[]> persons = new List<string[]>();
                StreamReader sr = new StreamReader("txt.txt");
                while (!sr.EndOfStream)
                {
                    string[] person = new string[4];
                    person[0] = sr.ReadLine();
                    person[1] = sr.ReadLine();
                    person[2] = sr.ReadLine();
                    person[3] = sr.ReadLine();
                    if (person[1] == "М")
                    {
                        male.Enqueue(person);
                    }
                    else if (person[1] == "Ж")
                    {
                        famale.Enqueue(person);
                    }
                }
                sr.Close();

                while (male.Count > 0)
                {
                    persons.Add(male.Dequeue());
                }
                while (famale.Count > 0)
                {
                    persons.Add(famale.Dequeue());
                }
                foreach (var p in persons)
                {
                    Console.WriteLine(p[0]);
                    Console.WriteLine(p[1]);
                    Console.WriteLine(p[2]);
                    Console.WriteLine(p[3]);
                    Console.WriteLine("Нажмите Enter для следующей персоны");
                    Console.ReadKey();
                }
            }
            else
            {
                Console.WriteLine("Такого файла нету");
            }
        }
    }
}
