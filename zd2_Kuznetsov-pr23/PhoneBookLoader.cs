using System;
using System.Collections.Generic;
using System.Deployment.Application;
using System.Diagnostics.Contracts;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace zd2_Kuznetsov_pr23
{
    public static class PhoneBookLoader
    {
        // Метод загрузки из CSV файла
        public static void Load(PhoneBook phoneBook, string fileName) 
        {
            var lines = File.ReadAllLines(fileName);
            foreach (var line in lines)
            {
                List<string> cells = line.Split(';').ToList();
                Contact contact = new Contact();
                contact.Name = cells[0];
                contact.Phone = cells[1];
                phoneBook.Contacts.Add(contact);
            }
        }

        // Метод загрудки в CSV файла
        public static void Save(PhoneBook phoneBook, string fileName)
        {
            using (StreamWriter writer = new StreamWriter(fileName))
            {
                foreach (var contact in phoneBook.Contacts)
                {
                    writer.WriteLine($"{contact.Name};{contact.Phone}");
                }
            }
        }
    }
}
