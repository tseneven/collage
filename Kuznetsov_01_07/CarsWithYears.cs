using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kuznetsov_01_07
{
    public class CarsWithYears : Car
    {
        // Поля
        int _year;
        static private List<CarsWithYears> _carsWithYear = new List<CarsWithYears>();
        
        // Конструктор
        public CarsWithYears(string carName, int carCapacity, int capacityPer100Km, int year) : base(carName, carCapacity, capacityPer100Km) 
        {
            _year = year;
        }
        
        // Перегрузка конструктор
        public CarsWithYears() : base() { }
        // Перегружен: Проверка валидности

        public bool isValid()
        {
            if(!base.isValid())
                return false;
            if(_year < 0)
                return false;
            return true;
        }

        // Перегружен: Добавление машины в список
        public bool addList(CarsWithYears car)
        {
            if (car.isValid())
            {
                _carsWithYear.Add(car);
                return true;
            }
            return false;
        }
        // Переопределенный: Информация о машине
        public override string carInfo()
        {
            return base.carInfo() +  $"\nГод выпуска: {_year}";
        }
        // Переопределенный: Удаление машины из списка
        public override void RemoveAt(int id)
        {
            if (id < 1)
            {
                return;
            }
            _carsWithYear.RemoveAt(id - 1);

        }

        // Переопределенный: Удаление нескольких машин из списка
        public override void RemoveAt(int id, int count)
        {
            _carsWithYear.RemoveRange(id - 1, count);
        }

        // Переопределенный: Информация о машинах в списке
        public override List<string> infoAboutList()
        {
            List<string> names = new List<string>();
            if (_carsWithYear.Count == 0)
            {
                names.Add("Список пуст");
                return names;
            }
            foreach (Car car in _carsWithYear)
            {
                names.Add(car.carName);
            }
            return names;
        }

        // Переопределенный метод Q
        public override double Q()
        {
            if(_year < 5)
            {
                return base.Q() * 1.15 * _year;
            }
            return base.Q() * 1.7 * _year;

        }


    }
}
