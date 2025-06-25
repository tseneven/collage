    using ClassLibrary1;
    using Departments;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Data;
    using System.Drawing;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using System.Windows.Forms;
    using System.Xml.Linq;

    namespace zd3_Kuznetsovpr_23
    {
        public partial class Form1 : Form
        {
            // Поля
            DangerousDepartment dangerousDepartment;
            private List<DangerousDepartment> departments = new List<DangerousDepartment>();
            public Form1()
            {
                InitializeComponent();
            }
            
            // Для открытия меню
            private void Form1_MouseClick(object sender, MouseEventArgs e)
            {
                if (e.Button == MouseButtons.Right)
                {
                    contextMenuStrip1.Show(e.Location);
                }
            }

            //Метод обновления листбокс
            private void UpdateListBox()
            {
                listBox1.Items.Clear();
                foreach (var dep in DangerousDepartment.dangerousDepartments.Keys)
                {
                    listBox1.Items.Add(dep.Info());
                }

            }

            // Кнопка добавления
            private void добавитьToolStripMenuItem_Click(object sender, EventArgs e)
            {
            string name = textBox1.Text;
            int base_salary = Convert.ToInt32(textBox2.Text);
            double coefficient = Convert.ToDouble(textBox3.Text);
            int numberOfEmployees = Convert.ToInt32(textBox4.Text);
            DateTime creationDate = DateTime.Now;
            int p = Convert.ToInt32(textBox5.Text);
            string type = textBox6.Text;

                add(name, base_salary, coefficient, numberOfEmployees, p, type, creationDate);
                UpdateListBox();
                ClearTextBox();
            }

            // Кнопка удаления
            private void удалитьToolStripMenuItem_Click(object sender, EventArgs e)
            {
                delete(textBox1.Text);
                UpdateListBox();
                ClearTextBox();
                MessageBox.Show("1");
            }

            //Метод очистки текстбоксов
            private void ClearTextBox()
            {

                textBox1.Text = "";
                textBox2.Text = "";
                textBox3.Text = "";
                textBox4.Text = "";
                textBox5.Text = "";
                textBox6.Text = "";
                textBox7.Text = "";

            }

            private void Form1_Load(object sender, EventArgs e)
            {

            }
            
            // Кнопка удаления без сотрудников
            private void удалитьВсеОтделыДоToolStripMenuItem_Click(object sender, EventArgs e)
            {
                DangerousDepartment.DeleteDepartment();
                UpdateListBox();
                ClearTextBox();
            }

            // Метод удаления
            public string delete(string name)
            {

            if (Validation.textfild(name) == "")
                {      
                    DangerousDepartment.DeleteDepartment(name);
                    return "";
                }
                else
                {
                return Validation.textfild(name);
                }

            }
        // Метод добавления
        public string add(string name, int b_s, double b, int ne, int p, string t, DateTime creationDate )
        {

            if (Validation.textfild(name) == "" &&
                Validation.numericfild(b_s.ToString()) == "" &&
                Validation.numericfild(b.ToString()) == "" &&
                Validation.numericfild(ne.ToString()) == "" &&
                Validation.textfild(t) == "")
            {
                

                var dep = new DangerousDepartment(name, b_s, b, p, t, ne, creationDate.ToString("dd.MM.yyyy") );

                if (!departments.Any(d => d.Name == name))
                {
                    departments.Add(dep); 
                    DangerousDepartment.AddDepartment(dep);
                    MessageBox.Show(dep.Info());
                    return "";
                }
                else
                {
                    MessageBox.Show("Такой отдел уже существует.");
                    return "";
                }
            }
            else
            {
                return "Ошибка валидации";
            }
        }

    }
}
