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
    public partial class studentMode: Form
    {
        private string userLogin;
        public studentMode(string login)
        {
            InitializeComponent();
            userLogin = login;
        }
        List<string[]> questions = new List<string[]>();
        int curr_question = 0;
        int correct_answ = 0;

        private void Form2_Load(object sender, EventArgs e)
        {
            string line;
            StreamReader sr = new StreamReader("que.txt");

            while (!sr.EndOfStream)
            {
                string[] question = new string[5];

                line = sr.ReadLine();
                question[0] = line;

                for (int i = 1; i <= 3; i++)
                {
                    question[i] = sr.ReadLine();
                }
                question[4] = sr.ReadLine();
                questions.Add(question);
                curr_question++;
            }
            sr.Close();
            curr_question = 0;

            this.Text = $"Вопрос{curr_question + 1}";
            label1.Text = questions[curr_question][0];
            radioButton1.Text = questions[curr_question][1];
            radioButton2.Text = questions[curr_question][2];
            radioButton3.Text = questions[curr_question][3];


        }

        private void button1_Click(object sender, EventArgs e)
        {
            string correctAnswer = questions[curr_question][4];
            if ((radioButton1.Checked && radioButton1.Text == correctAnswer) ||
                (radioButton2.Checked && radioButton2.Text == correctAnswer) ||
                (radioButton3.Checked && radioButton3.Text == correctAnswer))
            {
                correct_answ++;
            }

            curr_question++;


            if (curr_question < questions.Count)
            {
                this.Text = $"Вопрос{curr_question + 1}";
                label1.Text = questions[curr_question][0];
                radioButton1.Text = questions[curr_question][1];
                radioButton2.Text = questions[curr_question][2];
                radioButton3.Text = questions[curr_question][3];
            }
            else
            {
                button1.Enabled = false;
                if (curr_question == correct_answ)
                {
                    MessageBox.Show($"Верно на все {curr_question} вопросов");
                }
                else
                {
                    MessageBox.Show($"Верно на {correct_answ} вопросов");
                }
                StreamWriter sw = new StreamWriter("result.txt");
                sw.WriteLine(userLogin);
                sw.WriteLine(curr_question);
                sw.WriteLine(correct_answ);
                sw.Close();
            }
        }
    }
}
