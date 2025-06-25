using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Departments
{
    internal class Department
    {
        private string name;
        private int base_salary;
        private double coefficient;
        private int numberOfEmployees; // Новое поле
        private DateTime creationDate; // Новое поле

        public string Name { get => name; set => name = value; }
        public int Base_salary { get => base_salary; set => base_salary = value; }
        public double Coefficient { get => coefficient; set => coefficient = value; }
        public int NumberOfEmployees { get => numberOfEmployees; set => numberOfEmployees = value; } // Свойство для нового поля
        public DateTime CreationDate { get => creationDate; set => creationDate = value; } // Свойство для нового поля

        internal Department(string name, int base_salary, double coefficient, int numberOfEmployees, DateTime creationDate)
        {
            Name = name;
            Base_salary = base_salary;
            Coefficient = coefficient;
            NumberOfEmployees = numberOfEmployees; // Инициализация нового поля
            CreationDate = creationDate; // Инициализация нового поля
        }

        public virtual double Q()
        {
            return base_salary * (100 + coefficient);
        }

        public virtual string Info()
        {
            return $"Название: {Name} Базовый оклад:{Base_salary} Коэффицент: {Coefficient} Количество сотрудников: {NumberOfEmployees} Дата создания: {CreationDate}, Q = {Q()}";
        }
    }
}
