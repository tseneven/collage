using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Departments
{
    public class Department
    {
        //Поля
        private string name;
        private int base_salary;
        private double coefficient;
        private int numberOfEmployees; // Новое поле
        private string creationDate; // Новое поле

        //Геттеры и сеттеры
        public string Name { get => name; set => name = value; }
        public int Base_salary { get => base_salary; set => base_salary = value; }
        public double Coefficient { get => coefficient; set => coefficient = value; }
        public int NumberOfEmployees { get => numberOfEmployees; set => numberOfEmployees = value; } // Свойство для нового поля
        public string CreationDate { get => creationDate; set => creationDate = value; } // Свойство для нового поля

        //Конструктор
        internal Department(string name, int base_salary, double coefficient, int numberOfEmployees, string creationDate)
        {
            Name = name;
            Base_salary = base_salary;
            Coefficient = coefficient;
            NumberOfEmployees = numberOfEmployees; // Инициализация нового поля
            CreationDate = creationDate; // Инициализация нового поля
        }
        
        //Метод Q
        public virtual double Q()
        {
            return base_salary * (100 + coefficient);
        }
        //Метод инфо
        public virtual string Info()
        {
            return $"Название: {Name} Базовый оклад:{Base_salary} Коэффицент: {Coefficient} Количество сотрудников: {NumberOfEmployees} Дата создания: {CreationDate}, Q = {Q()}";
        }
    }
}
