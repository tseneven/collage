using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace УП4
{
    public partial class autorizaton: Form
    {
        List<string[]> persons = new List<string[]>();
        int curr_person = 0;
        
        public autorizaton()
        {
            InitializeComponent();
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            register form = new register();
            form.Show();
            this.Hide();

        }

        private void autorizaton_Load(object sender, EventArgs e)
        {
            string line;
            StreamReader sr = new StreamReader("person.txt");


            while (!sr.EndOfStream)
            {
                string[] person = new string[4];

                line = sr.ReadLine();
                person[0] = line;

                for (int i = 1; i < 3; i++)
                {
                    person[i] = sr.ReadLine();
                }
                person[3] = sr.ReadLine();
                persons.Add(person);
                curr_person++;
            }
            sr.Close();
            curr_person = 0;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string login = textBox1.Text;
            string password = textBox2.Text;
            int indexPerson = -1;
            for (int i = 0; i < persons.Count; i++)
            {
                if (persons[i][0] == login)
                {
                    indexPerson = i;
                }
            }
            if(indexPerson != -1)
            {
                if(persons[indexPerson][2] == password)
                {
                    if (persons[indexPerson][3] == "admin")
                    {
                        adminMode form = new adminMode();
                        form.Show();
                        this.Hide();
                    }
                    else if (persons[indexPerson][3] == "student")
                    {
                        studentMode form = new studentMode(login);
                        form.Show();
                        this.Hide();
                    }
                }
                else
                {
                    MessageBox.Show("Неверный пароль");
                }
            }
            else
            {
                MessageBox.Show("Такого аккаунта не существует");
            }
        }
    }
}
