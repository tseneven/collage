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

namespace prKol_ind2_Kuznetsov_A_v_8
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (File.Exists(textBox1.Text))
            {
                Queue<string[]> over30 = new Queue<string[]>();
                Queue<string[]> under30 = new Queue<string[]>();
                List<string[]> persons = new List<string[]>();
                StreamReader sr = new StreamReader(textBox1.Text);
                while (!sr.EndOfStream)
                {
                    string[] person = new string[4];
                    person[0] = sr.ReadLine();
                    person[1] = sr.ReadLine();
                    person[2] = sr.ReadLine();
                    person[3] = sr.ReadLine();
                    if (Convert.ToInt32(person[2]) < 30)
                    {
                        under30.Enqueue(person);
                    }
                    else if (Convert.ToInt32(person[2]) >= 30)
                    {
                        over30.Enqueue(person);
                    }
                }
                sr.Close();

                while (under30.Count > 0)
                {
                    persons.Add(under30.Dequeue());
                }
                while (over30.Count > 0)
                {
                    persons.Add(over30.Dequeue());
                }
                foreach (var p in persons)
                {
                    listBox1.Items.Add($"{p[0]} {p[1]}, {p[2]} лет, {p[3]}");
                }
            }
            else
            {
                MessageBox.Show("Такого файла не существует");
            }

        }
    }
}
