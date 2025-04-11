using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ScrollBar;

namespace УП4
{
    public partial class register: Form
    {
        public register()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string login = textBox1.Text;
            string email = textBox2.Text;
            string password = textBox3.Text;
            if (textBox3.Text == textBox4.Text)
            {
                if (password.Length > 7)
                {
                    int upperChar = 0;
                    int isNumber = 0;
                    for(int i = 0; i < password.Length; i++)
                    {
                        if (char.IsUpper(password[i]))
                        {
                            upperChar++;
                        }
                        else if (char.IsDigit(password[i]))
                        {
                            isNumber++;
                        }
                    }
                    if(upperChar >= 1)
                    {
                        if(isNumber >= 1)
                        {
                            if(login.Length > 7)
                            {
                                int atIndex = email.IndexOf('@');
                                int dotIndex = email.LastIndexOf('.');

                                if (atIndex > 0 && dotIndex > atIndex + 1 && dotIndex < email.Length - 1)
                                {
                                    if (radioButton1.Checked) 
                                    {
                                        using (StreamWriter sw = new StreamWriter("person.txt", true)) 
                                        {
                                            sw.WriteLine(login);
                                            sw.WriteLine(email);
                                            sw.WriteLine(password);
                                            sw.WriteLine("student");
                                        }
                                        MessageBox.Show("Регистрация прошла успешно!");
                                    }
                                    else if (radioButton2.Checked)
                                    {
                                        using (StreamWriter sw = new StreamWriter("person.txt", true)) 
                                        {
                                            sw.WriteLine(login);
                                            sw.WriteLine(email);
                                            sw.WriteLine(password);
                                            sw.WriteLine("admin");
                                        }
                                        MessageBox.Show("Регистрация прошла успешно!");
                                        autorizaton form = new autorizaton();
                                        form.Show();
                                        this.Hide();
                                    }
                                }
                                else
                                {
                                    MessageBox.Show("Почта невалидная");
                                }
                            }
                            else
                            {
                                MessageBox.Show("Логин должен быть больше 7 символов");
                            }
                        }
                        else
                        {
                            MessageBox.Show("Должна быть хотя бы 1 цифра");
                        }
                    }
                    else
                    {
                        MessageBox.Show("Должна быть хотя бы 1 заглавная буква");
                    }
                }
                else
                {
                    MessageBox.Show("Пароль должен быть больше 7 символов");
                }    
            }
            else
            {
                MessageBox.Show("Пароли должны совпадать");
            }

        }
    }
}
