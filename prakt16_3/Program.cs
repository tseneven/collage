using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace prakt16_3
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("a)");
            double[] mass = new double[6];
            for(int i = 0; i < mass.Length; i++)
            {
                mass[i] = Convert.ToDouble(Console.ReadLine());
            }
            Console.WriteLine("Число Частота");
            var ch = mass.GroupBy(x => x).Select(g => new { num = g.Key, ch = g.Count() });
            foreach (var i in ch)
            {
                Console.WriteLine($"{i.num} - {i.ch}");
            }
            Console.WriteLine("b)");
            var newMass = mass.Select(x => x * ch.First(g => g.num == x).ch).ToArray();
            for (int i = 0; i < mass.Length; i++)
            {
                int chhs = ch.First(g => g.num == mass[i]).ch;
                Console.WriteLine($"{newMass[i]} - {chhs}");
            }
            Console.ReadKey();
        }
    }
}
