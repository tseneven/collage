using Kuznetsov_01_07;

public class Program
{
    private static void Main(string[] args)
    {
        Car mainCar = new Car();
        CarsWithYears mainCarWithYears = new CarsWithYears();
        int action = 0;
        while (true)
        {
            Console.WriteLine("1 - Добавить автомобиль");
            Console.WriteLine("2 - Удалить автомобиль");
            Console.WriteLine("3 - Удалить несколько автомобилей");
            Console.WriteLine("4 - Вывести среднее автомобилей");
            Console.WriteLine("5 - Вывести список автомобилей");
            Console.WriteLine("6 - Добавить автомобиль c годом выпуска");
            Console.WriteLine("7 - Удалить автомобиль с годом выпуска");
            Console.WriteLine("8 - Удалить несколько автомобилей с годом выпуска");
            Console.WriteLine("9 - Вывести список автомобилей c годом выпука");


            bool result = int.TryParse(Console.ReadLine(), out action);

            if (result == false)
            {
                Console.WriteLine("Нужно ввести цифру!");
            }
            else
            {
                switch (action)
                {
                    case 1:
                        {
                            string _carName;
                            int _carCapacity;
                            int _capacityPer100Km;
                            Console.WriteLine("Введите марку");
                            _carName = Console.ReadLine();
                            Console.WriteLine("Введите пробег");
                            result = int.TryParse(Console.ReadLine(), out _carCapacity);
                            if (result == false)
                            {
                                Console.WriteLine("Данные введены не верно");
                                break;
                            }
                            Console.WriteLine("Введите расход на 100 км");
                            result = int.TryParse(Console.ReadLine(), out _capacityPer100Km);
                            if (result == false)
                            {
                                Console.WriteLine("Данные введены не верно");
                                break;
                            }

                            Car _car = new Car(_carName, _carCapacity, _capacityPer100Km);

                            if (mainCar.addList(_car))
                            {
                                Console.WriteLine("Машина успешно добавлена в список!");
                                Console.WriteLine($" Q = {_car.Q()}\n");
                                Console.WriteLine(_car.carInfo());
                            }
                            else
                            {
                                Console.WriteLine("Валидация не прошла проверку и автомобиль не был добавлен в список");
                            }

                            break;
                        }
                    case 2:
                        {
                            Console.WriteLine("Введите id автомобиля");
                            int id;

                            result = int.TryParse(Console.ReadLine(), out id);

                            if (result == false)
                            {
                                Console.WriteLine("Введены неверные данные");
                                break;
                            }

                            if(id < 1)
                            {
                                Console.WriteLine($"Нельзя удалить автомобиль с id = {id}");
                            }

                            mainCar.RemoveAt(id);
                            Console.WriteLine("Автомобиль удален!");

                            break;
                        }
                    case 3:
                        {
                            int id;
                            int count;
                            Console.WriteLine("Введите id автомобиля и количество на удаление через пробел");

                            string tmp = Console.ReadLine();

                            string[] stringIds = tmp.Trim().Split(" ");

                            result = int.TryParse(stringIds[0],out id);
                            if (result == false)
                            {
                                Console.WriteLine("Введены неверные данные");
                                break;
                            }
                            result = int.TryParse(stringIds[1], out count);
                            if (result == false)
                            {
                                Console.WriteLine("Введены неверные данные");
                                break;
                            }

                            mainCar.RemoveAt(id, count);
                            Console.WriteLine("Автомобили удален!");

                            break;
                        }
                   case 4:
                        {
                            Console.WriteLine(mainCar.middleValuesQInList());
                            break;
                        }

                   case 5: 
                        {
                            foreach (string name in mainCar.infoAboutList())
                            {
                                Console.WriteLine(name);
                            }
                            break;
                        }
                    case 6:
                        {
                            string _carName;
                            int _carCapacity;
                            int _capacityPer100Km;
                            int _year;
                            Console.WriteLine("Введите марку");
                            _carName = Console.ReadLine();
                            Console.WriteLine("Введите пробег");
                            result = int.TryParse(Console.ReadLine(), out _carCapacity);
                            if (result == false)
                            {
                                Console.WriteLine("Данные введены не верно");
                                break;
                            }
                            Console.WriteLine("Введите расход на 100 км");
                            result = int.TryParse(Console.ReadLine(), out _capacityPer100Km);
                            if (result == false)
                            {
                                Console.WriteLine("Данные введены не верно");
                                break;
                            }

                            Console.WriteLine("Введите год");
                            result = int.TryParse(Console.ReadLine(), out _year);
                            if (result == false)
                            {
                                Console.WriteLine("Данные введены не верно");
                                break;
                            }


                            CarsWithYears _car = new CarsWithYears(_carName, _carCapacity, _capacityPer100Km, _year);

                            if (mainCarWithYears.addList(_car))
                            {
                                Console.WriteLine("Машина успешно добавлена в список!");
                                Console.WriteLine($" Q = {_car.Q()}\n");
                                Console.WriteLine(_car.carInfo());
                            }
                            else
                            {
                                Console.WriteLine("Валидация не прошла проверку и автомобиль не был добавлен в список");
                            }

                            break;
                        }
                    case 7:
                        {
                            Console.WriteLine("Введите id автомобиля");
                            int id;

                            result = int.TryParse(Console.ReadLine(), out id);

                            if (result == false)
                            {
                                Console.WriteLine("Введены неверные данные");
                                break;
                            }

                            if (id < 1)
                            {
                                Console.WriteLine($"Нельзя удалить автомобиль с id = {id}");
                            }

                            mainCarWithYears.RemoveAt(id);
                            Console.WriteLine("Автомобиль удален!");

                            break;
                        }
                    case 8:
                        {
                            int id;
                            int count;
                            Console.WriteLine("Введите id автомобиля и количество на удаление через пробел");

                            string tmp = Console.ReadLine();

                            string[] stringIds = tmp.Trim().Split(" ");

                            result = int.TryParse(stringIds[0], out id);
                            if (result == false)
                            {
                                Console.WriteLine("Введены неверные данные");
                                break;
                            }
                            result = int.TryParse(stringIds[1], out count);
                            if (result == false)
                            {
                                Console.WriteLine("Введены неверные данные");
                                break;
                            }

                            mainCarWithYears.RemoveAt(id, count);
                            Console.WriteLine("Автомобили удален!");

                            break;
                        }
                    case 9:
                        {
                            foreach (string name in mainCarWithYears.infoAboutList())
                            {
                                Console.WriteLine(name);
                            }
                            break;
                        }
                    default:
                        break;
                }
            }
        }
    }
}