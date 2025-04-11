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
    public partial class adminMode : Form
    {
        public adminMode()
        {
            InitializeComponent();
        }

        List<string[]> questions = new List<string[]>();
        int cur_question = 0;


        private void Form1_Load(object sender, EventArgs e)
        {

            string line;
            StreamReader sr = new StreamReader("que.txt");

            
            while (!sr.EndOfStream)
            {
                string[] question = new string[5];

                line = sr.ReadLine();
                question[0] = line;
            
                for(int i = 1; i <=3; i++)
                {
                    question[i] = sr.ReadLine();
                }
                question[4] = sr.ReadLine();
                questions.Add(question);
                cur_question++;
            }
            sr.Close();

            cur_question = 0;
            listBox3.Items.Add(questions[0][0]);
            listBox4.Items.Add(questions[0][1]);
            listBox4.Items.Add(questions[0][2]);
            listBox4.Items.Add(questions[0][3]);
            listBox4.Items.Add($"Правильный ответ: {questions[0][4]}");

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            cur_question--;

            if (cur_question >= 0)
            {
                button2.Enabled = true;
                listBox3.Items.Clear();
                listBox4.Items.Clear();
                listBox3.Items.Add(questions[cur_question][0]);
                listBox4.Items.Add(questions[cur_question][1]);
                listBox4.Items.Add(questions[cur_question][2]);
                listBox4.Items.Add(questions[cur_question][3]);
                listBox4.Items.Add($"Правильный ответ: {questions[cur_question][4]}");
            }
            else
            {
                button1.Enabled = false;
            }

        }

        private void button2_Click(object sender, EventArgs e)
        {
            cur_question++;
            if (cur_question < questions.Count)
            {

                button1.Enabled = true;
                listBox3.Items.Clear();
                listBox4.Items.Clear();
                listBox3.Items.Add(questions[cur_question][0]);
                listBox4.Items.Add(questions[cur_question][1]);
                listBox4.Items.Add(questions[cur_question][2]);
                listBox4.Items.Add(questions[cur_question][3]);
                listBox4.Items.Add($"Правильный ответ: {questions[cur_question][4]}");
            }
            else
            {
                button2.Enabled = false;
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (listBox4.SelectedIndex == -1)
            {
                MessageBox.Show("Строка не выбрана");
            }
            else
            {
                if (listBox4.SelectedIndex - 1 < 0)
                {
                    listBox4.SelectedIndex = listBox4.Items.Count - 1;
                }
                else
                {
                    listBox4.SelectedIndex--;
                }
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (listBox4.SelectedIndex == -1)
            {
                MessageBox.Show("Строка не выбрана");
            }
            else
            {
                if (listBox4.SelectedIndex + 1 >= listBox4.Items.Count)
                {
                    listBox4.SelectedIndex = 0;
                }
                else
                {
                    listBox4.SelectedIndex++;
                }
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {

            if (listBox4.SelectedIndex == -1)
            {
                string quest = textBox2.Text;
                questions[cur_question][0] = quest;
            }
            else
            {
                string ans = textBox2.Text;
                int selectedIndex = listBox4.SelectedIndex + 1;
                questions[cur_question][selectedIndex] = ans;
            }


            listBox3.Items.Clear();
            listBox4.Items.Clear();
            listBox3.Items.Add(questions[cur_question][0]);
            listBox4.Items.Add(questions[cur_question][1]);
            listBox4.Items.Add(questions[cur_question][2]);
            listBox4.Items.Add(questions[cur_question][3]);
            listBox4.Items.Add($"Правильный ответ: {questions[cur_question][4]}");
            SaveChagnes();


        }

        private void button6_Click(object sender, EventArgs e)
        {
            string quest = textBox9.Text;
            string answer1 = textBox10.Text;
            string answer2 = textBox11.Text;
            string answer3 = textBox12.Text;
            if (quest == "" || answer1 == "" || answer2 == ""|| answer3 == "") 
            {
                MessageBox.Show("Поля не могут быть пустыми");
            }
            else
            {
                string correct_answer = "";
                if (checkBox6.Checked)
                {
                    correct_answer = textBox10.Text;
                }
                else if (checkBox5.Checked)
                {
                    correct_answer = textBox11.Text;
                }
                else if (checkBox4.Checked)
                {
                    correct_answer = textBox12.Text;
                }

                string[] question = new string[5];


                question[0] = quest;
                question[1] = answer1;
                question[2] = answer2;
                question[3] = answer3;
                question[4] = correct_answer;
                questions.Add(question);
                cur_question = questions.Count - 1;
                listBox3.Items.Clear();
                listBox4.Items.Clear();
                listBox3.Items.Add(questions[cur_question][0]);
                listBox4.Items.Add(questions[cur_question][1]);
                listBox4.Items.Add(questions[cur_question][2]);
                listBox4.Items.Add(questions[cur_question][3]);
                listBox4.Items.Add($"Правильный ответ: {questions[cur_question][4]}");
                SaveChagnes();
            }

        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            string correct_ans = questions[cur_question][1];
            questions[cur_question][4] = correct_ans;
            listBox3.Items.Clear();
            listBox4.Items.Clear();
            listBox3.Items.Add(questions[cur_question][0]);
            listBox4.Items.Add(questions[cur_question][1]);
            listBox4.Items.Add(questions[cur_question][2]);
            listBox4.Items.Add(questions[cur_question][3]);
            listBox4.Items.Add($"Правильный ответ: {questions[cur_question][4]}");

        }

        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {
            string correct_ans = questions[cur_question][2];
            questions[cur_question][4] = correct_ans;
            listBox3.Items.Clear();
            listBox4.Items.Clear();
            listBox3.Items.Add(questions[cur_question][0]);
            listBox4.Items.Add(questions[cur_question][1]);
            listBox4.Items.Add(questions[cur_question][2]);
            listBox4.Items.Add(questions[cur_question][3]);
            listBox4.Items.Add($"Правильный ответ: {questions[cur_question][4]}");

        }

        private void checkBox3_CheckedChanged(object sender, EventArgs e)
        {
            string correct_ans = questions[cur_question][3];
            questions[cur_question][4] = correct_ans;
            listBox3.Items.Clear();
            listBox4.Items.Clear();
            listBox3.Items.Add(questions[cur_question][0]);
            listBox4.Items.Add(questions[cur_question][1]);
            listBox4.Items.Add(questions[cur_question][2]);
            listBox4.Items.Add(questions[cur_question][3]);
            listBox4.Items.Add($"Правильный ответ: {questions[cur_question][4]}");

        }
        public void SaveChagnes()
        {
            StreamWriter sw = new StreamWriter("que.txt");
            for(int i = 0; i < questions.Count; i++)
            {
                for (int j = 0; j < 5; j++)
                {
                    sw.WriteLine(questions[i][j]);
                }
            }
            sw.Close();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            result form = new result();
            form.Show();
        }

        private void button8_Click(object sender, EventArgs e)
        {
            if (questions.Count == 0)
            {
                MessageBox.Show("Нет вопросов для удаления");
                return;
            }

            questions.RemoveAt(cur_question-1);

            if (questions.Count == 0)
            {
                listBox3.Items.Clear();
                listBox4.Items.Clear();
                button1.Enabled = false;
                button2.Enabled = false; 
                MessageBox.Show("Все вопросы удалены");
            }
            else
            {
                
                if (cur_question >= questions.Count)
                {
                    cur_question = questions.Count - 1; 
                }

                listBox3.Items.Clear();
                listBox4.Items.Clear();
                listBox3.Items.Add(questions[cur_question][0]);
                listBox4.Items.Add(questions[cur_question][1]);
                listBox4.Items.Add(questions[cur_question][2]);
                listBox4.Items.Add(questions[cur_question][3]);
                listBox4.Items.Add($"Правильный ответ: {questions[cur_question][4]}");

                button1.Enabled = cur_question > 0; 
                button2.Enabled = cur_question < questions.Count - 1; 
            }

            SaveChagnes();
        }
    }
}
