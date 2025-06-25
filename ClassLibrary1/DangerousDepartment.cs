using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Departments
{
    public class DangerousDepartment : Department
    {
        // Поля
        private int p;
        private string type;
        static public Dictionary<DangerousDepartment, string> dangerousDepartments = new Dictionary<DangerousDepartment, string>();

        //Конструктор
        public DangerousDepartment(string name, int base_salary, double coefficient, int p, string type, int numberOfEmployees, string creationDate) :base(name, base_salary, coefficient, numberOfEmployees, creationDate)
        { 
            P = p;
            Type = type;
        }

        //Геттеры и сеттеры
        public int P { get => p; set => p = value; }
        public string Type { get => type; set =>type = value; } // Свойство для нового поля

        //Метод расчета Q
        public override double Q()
        {
            return (Base_salary * (100 + Coefficient)) + ((Base_salary * (100 + Coefficient)) / P);
        }

        // Метод добавления
        static public string AddDepartment(DangerousDepartment department, string q)
        {
            if (!dangerousDepartments.Any(d => d.Key.Name == department.Name))
            {
                dangerousDepartments.Add(department, q);
                return "Отдел добавлен";
            }
            else
            {
                return "Такой отдел уже существует";
            }
        }

        //Перегрузка метода добавления
        static public string AddDepartment(DangerousDepartment department)
        {
            if (!dangerousDepartments.Any(d => d.Key.Name == department.Name))
            {
                dangerousDepartments.Add(department, "Undefind");
                return "Отдел добавлен";
            }
            else
            {
                return "Такой отдел уже существует";
            }
        }

        //Метод удадения
        static public string DeleteDepartment(string name)
        {
            
            var departmentToRemove = dangerousDepartments.Keys.FirstOrDefault(d => d.Name == name);

            if (departmentToRemove != null)
            {
                dangerousDepartments.Remove(departmentToRemove);
                return "Отдел удален";
            }
            else
            {
                return "Такой отдел не существует";
            }
        }
        //Перегрузка метода удаления
        static public string DeleteDepartment()
        {

            var departmentsToRemove = dangerousDepartments.Keys
                .Where(d => d.NumberOfEmployees < 1)
                .ToList();

            if (departmentsToRemove.Count > 0)
            {
                foreach (var department in departmentsToRemove)
                {
                    dangerousDepartments.Remove(department);
                }
                return $"{departmentsToRemove.Count} отделов удалено";
            }
            else
            {
                return "Нет отделов для удаления до указанной даты";
            }
        }
        
        //Метод для получения информации
        public override string Info()
        {
            return base.Info() + $" p = {P}, Тип опасности: {type}, Qr = {this.Q()}";
        }
    }
}
