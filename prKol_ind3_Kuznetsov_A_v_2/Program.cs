using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace prKol_ind3_Kuznetsov_A_v_2
{
    internal class Program
    {
        static void Main(string[] args)
        {
            ArrayList complexNumbers = new ArrayList();

            while (true)
            {
                Console.WriteLine("\nМеню:");
                Console.WriteLine("1. Добавить комплексное число");
                Console.WriteLine("2. Сложение двух чисел");
                Console.WriteLine("3. Вычитание двух чисел");
                Console.WriteLine("4. Умножение двух чисел");
                Console.WriteLine("5. Показать все числа");
                Console.WriteLine("0. Выход");
                Console.Write("Ваш выбор: ");
                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        Console.Write("Введите действительную часть: ");
                        double real = Convert.ToDouble(Console.ReadLine());
                        Console.Write("Введите мнимую часть: ");
                        double imaginary = Convert.ToDouble(Console.ReadLine());
                        complexNumbers.Add(new ComplexNumber(real, imaginary));
                        break;

                    case "2":
                        PerformOperation(complexNumbers, "add");
                        break;

                    case "3":
                        PerformOperation(complexNumbers, "subtract");
                        break;

                    case "4":
                        PerformOperation(complexNumbers, "multiply");
                        break;

                    case "5":
                        Console.WriteLine("Список комплексных чисел:");
                        for (int i = 0; i < complexNumbers.Count; i++)
                        {
                            Console.WriteLine($"{i}: {complexNumbers[i]}");
                        }
                        break;

                    case "0":
                        return;

                    default:
                        Console.WriteLine("Неверный выбор.");
                        break;
                }
            }
        }

        static void PerformOperation(ArrayList list, string operation)
        {
            if (list.Count < 2)
            {
                Console.WriteLine("Недостаточно чисел для операции.");
                return;
            }

            Console.Write("Введите индекс первого числа: ");
            int index1 = Convert.ToInt32(Console.ReadLine());
            Console.Write("Введите индекс второго числа: ");
            int index2 = Convert.ToInt32(Console.ReadLine());

            if (index1 < 0 || index1 >= list.Count || index2 < 0 || index2 >= list.Count)
            {
                Console.WriteLine("Некорректный индекс.");
                return;
            }

            ComplexNumber c1 = (ComplexNumber)list[index1];
            ComplexNumber c2 = (ComplexNumber)list[index2];
            ComplexNumber result = null;

            switch (operation)
            {
                case "add":
                    result = c1.Add(c2);
                    Console.WriteLine($"Результат: {result}");
                    break;
                case "subtract":
                    result = c1.Subtract(c2);
                    Console.WriteLine($"Результат: {result}");
                    break;
                case "multiply":
                    result = c1.Multiply(c2);
                    Console.WriteLine($"Результат: {result}");
                    break;
            }
        }
    }
}
