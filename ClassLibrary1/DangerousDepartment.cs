using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Departments
{
    internal class DangerousDepartment : Department
    {
        private int p;
        private string type;
        static Dictionary<DangerousDepartment, string> dangerousDepartments;


        internal DangerousDepartment(string name, int base_salary, double coefficient, int p, string type, int numberOfEmployees, DateTime creationDate) :base(name, base_salary, coefficient, numberOfEmployees, creationDate)
        { 
            P = p;
            Type = type;
        }

        public int P { get => p; set => p = value; }
        public string Type { get => type; set =>type = value; } // Свойство для нового поля

        public override double Q()
        {
            return (Base_salary * (100 + Coefficient)) + ((Base_salary * (100 + Coefficient)) / P);
        }

        // Метод 
        public string AddDepartment(DangerousDepartment department, string q)
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

        public string AddDepartment(DangerousDepartment department)
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

        public string DeleteDepartment(string name)
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

        public string DeleteDepartment(DateTime creationDate)
        {

            var departmentsToRemove = dangerousDepartments.Keys
                .Where(d => d.CreationDate < creationDate)
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

        public override string Info()
        {
            return base.Info() + $"p = {P}, Тип опасности: {type}, Qr = {Q()}";
        }
    }
}
