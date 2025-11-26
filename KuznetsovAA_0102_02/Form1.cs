using Kuznetsov_Lib;

namespace KuznetsovAA_0102_02
{
    public partial class Form1 : Form
    {
        List<ComputerQ> computersQ;
        List<ComputerQp> computersQp;
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (!radioButton1.Checked && !radioButton2.Checked)
            {
                MessageBox.Show("Нужно выбрать хотя бы 1 класс");
                return;
            }
            action();


        }

        public void action()
        {
            if (radioButton1.Checked && !radioButton2.Checked)
            {
                if (!string.IsNullOrEmpty(textBox1.Text) && !string.IsNullOrEmpty(textBox2.Text) && !string.IsNullOrEmpty(textBox3.Text))
                {
                    double cpuValue;
                    int ozu;
                    if (double.TryParse(textBox2.Text, out cpuValue) && int.TryParse(textBox3.Text, out ozu))
                    {
                        ComputerQ computerQ = new ComputerQ(textBox1.Text, textBox2.Text, textBox3.Text);
                        listBox1.Items.Add(computerQ.cpuName + " " + computerQ.cpuValue + " " + computerQ.ozu);
                        listBox1.Items.Add(computerQ.Q().ToString());
                        computersQ.Add(computerQ);
                    }

                    else
                    {
                        MessageBox.Show("Строки имеют не числовое значение");
                    }


                }
                else
                {
                    MessageBox.Show("1");
                    MessageBox.Show("Одно из полей пустое");
                }
            }

            if (!radioButton1.Checked && radioButton2.Checked)
            {
                if (!string.IsNullOrEmpty(textBox1.Text) && !string.IsNullOrEmpty(textBox2.Text) && !string.IsNullOrEmpty(textBox3.Text) && !string.IsNullOrEmpty(hdd_textBox.Text))
                {
                    double cpuValue;
                    int ozu;
                    int hdd;
                    if (double.TryParse(textBox2.Text, out cpuValue) && int.TryParse(textBox3.Text, out ozu) && int.TryParse(hdd_textBox.Text, out hdd))
                    {
                        ComputerQp computerQp = new ComputerQp(textBox1.Text, textBox2.Text, textBox3.Text, hdd_textBox.Text);
                        listBox2.Items.Add(computerQp.cpuName + " " + computerQp.cpuValue + " " + computerQp.ozu);
                        listBox2.Items.Add(computerQp.Q().ToString());
                        computersQp.Add(computerQp);
                    }

                    else
                    {
                        MessageBox.Show("Строки имеют не числовое значение");
                    }


                }
                else
                {
                    MessageBox.Show("Одно из полей пустое");
                }
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            hdd.Enabled = false;
            hdd_textBox.Enabled = false;
            computersQ = new List<ComputerQ>();
            computersQp = new List<ComputerQp>();
        }



        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            hdd.Enabled = true;
            hdd_textBox.Enabled = true;
        }

        private void radioButton1_CheckedChanged_1(object sender, EventArgs e)
        {
            hdd.Enabled = false;
            hdd_textBox.Enabled = false;
        }
    }
}
