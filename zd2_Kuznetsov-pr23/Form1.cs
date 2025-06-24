using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace zd2_Kuznetsov_pr23
{
    public partial class Form1 : Form
    {
        PhoneBook PhoneBook;
        public Form1()
        {
            InitializeComponent();
            PhoneBook = new PhoneBook();
            PhoneBookLoader.Load(PhoneBook, "contacts.csv");

            foreach (var line in PhoneBook.Contacts)
            {
                listBox1.Items.Add(line.Name);
                listBox1.Items.Add(line.Phone);
            }
        }
        // Кнопка поиска
        private void button1_Click(object sender, EventArgs e)
        {
            List<Contact> name = PhoneBook.SearchForName(textBox1.Text);
            if (name.Count > 0)
            {
                MessageBox.Show(name[0].Phone);
            }
            else
            {
                MessageBox.Show("Такого контакта нету");
            }
        }
        // Метод для обновления liitbox
        private void UpdateListBox()
        {
            listBox1.Items.Clear();
            foreach (var line in PhoneBook.Contacts)
            {
                listBox1.Items.Add(line.Name);
                listBox1.Items.Add(line.Phone);
            }
        }
        // Кнопка добавить
        private void button2_Click(object sender, EventArgs e)
        {
            if (textBox2.Text != "" && textBox3.Text != "")
            {
                PhoneBook.AddToPhoneBook(textBox2.Text, textBox3.Text);
                UpdateListBox();
            } 
            else if (textBox2.Text != "")
            { 
                PhoneBook.AddToPhoneBook(textBox2.Text);
                UpdateListBox();
            }
            else
            {
                MessageBox.Show("Нельзя добавлять контакты без номера и имени или только с номером");
            }
        }

        // Кнопка удалить
        private void button3_Click(object sender, EventArgs e)
        {
            if (textBox1.Text != "" && textBox4.Text != "")
            {
                PhoneBook.DeleteFormPhoneBook(textBox1.Text, textBox4.Text);
            }
            else if (textBox1.Text != "")
            {
                PhoneBook.DeleteFormPhoneBook(textBox1.Text);
            }
            else
            {
                MessageBox.Show("Нельзя удалять только по номеру или с пустыми полями");
            }
            UpdateListBox();
        }

        // Кнопка сохранения в CSV формат
        private void button4_Click(object sender, EventArgs e)
        {
            PhoneBookLoader.Save(PhoneBook, "contacts.csv");
            MessageBox.Show("Файл сохранен");
        }
        
        // Кнопка выход
        private void button5_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
