namespace Kuznetsov_01_07
{
    public class Car
    {
        // Поля
        public string carName;
        private int carCapacity;
        private int capacityPer100km;

        static private List<Car> _cars = new List<Car>();

        // Конструктор
        public Car(string carName, int carCapacity, int capacityPer100km)
        {
            this.carName = carName;
            this.carCapacity = carCapacity;
            this.capacityPer100km = capacityPer100km;
        }

        // Перегрузка конструктора
        public Car()
        { }

        // Метод Q

        public virtual double Q()
        {
            return carCapacity / capacityPer100km;
        }

        // Вывод информации о машинах
        public virtual string carInfo()
        {
            return $"Марка: {carName}\nПробег: {carCapacity}\nРасход на 100 км: {capacityPer100km}";
        }

        // Проверка валидности
        public bool isValid()
        {
            if (string.IsNullOrEmpty(carName))
                return false;
            if (carCapacity < 0)
                return false;
            if (capacityPer100km < 0)
                return false;

            return true;
        }

        // Добавление машины
        public virtual bool addList(Car car)
        {

            if (car.isValid())
            {
                _cars.Add(car);
                return true;
            }
            return false;
        }

        // Удаление машины
        public virtual void RemoveAt(int id)
        {
            if (id < 1)
            {
                return;
            }
            _cars.RemoveAt(id - 1);
        } 
        
        // Удаление нескольких машин
        public virtual void RemoveAt(int id, int count)
        {
            _cars.RemoveRange(id - 1, count);
        }  
        
        // Расчет среднего Q
        public double middleValuesQInList()
        {
            double value = 0;
            foreach (Car car in _cars)
            {
                value += car.Q();
            }
            return value / _cars.Count();
        }

        //Информация о машинах в списке
        public virtual List<string> infoAboutList()
        {
            List<string> names = new List<string>();
            if (_cars.Count == 0)
            {
                names.Add("Список пуст");
                return names;
            }
            foreach (Car car in _cars)
            {
                names.Add(car.carName);
            }
            return names;
        }
    }
}
