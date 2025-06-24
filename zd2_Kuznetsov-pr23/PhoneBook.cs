using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace zd2_Kuznetsov_pr23
{
    public class PhoneBook
    {
        private List<Contact> contacts = new List<Contact>();

        public List<Contact> Contacts { get => contacts; set => contacts = value; }

        // Метод поиска по имени с использованием LINQ
        public List<Contact> SearchForName(string name)
        {
            var search = contacts.Where(n => n.Name == name).ToList();
            return search;
        }

        // Метод добавление контакта в книгу
        public void AddToPhoneBook(string name, string phone)
        {
            Contact contact = new Contact();
            contact.Name = name;
            contact.Phone = phone;
            contacts.Add(contact);
        }

        // Метод удаления контакта из книги
        public void DeleteFormPhoneBook(string name)
        {
            contacts.RemoveAll(c => c.Name == name);
        }

        // Перегрузка метода добавления контакта в книгу
        public void AddToPhoneBook(string name)
        {
            Contact contact = new Contact();
            contact.Name = name;
            contact.Phone = "Неизвестный";
            contacts.Add(contact);
        }

        // Перегрузка метода удаления контакта из книги
        public void DeleteFormPhoneBook(string name, string phone)
        {
            contacts.RemoveAll(c => c.Phone == phone && c.Name == name);
        }
    }
}
