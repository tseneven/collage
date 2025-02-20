namespace prakt12_v4_kuznetsov
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        Komnata[] komnata = new Komnata[10];

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            Komnata komnata1 = new Komnata();

            double length = Convert.ToDouble(dlina.Text);
            double shirina = Convert.ToDouble(shirinaa.Text);
            double visota = Convert.ToDouble(visotaa.Text);
            double kol_okn = Convert.ToDouble(kol__okn.Text);
            double visota_okn = Convert.ToDouble(visota__okn.Text);
            double shirina_okn = Convert.ToDouble(shirina__okn.Text);
            double raz_oboi = Convert.ToDouble(numericUpDown1.Value);

            double s = komnata1.S_komnati(length, shirina);
            double s3 = komnata1.s3_komnati(length, shirina, visota);
            double about_room = komnata1.kol_mat(length, shirina, visota, kol_okn, visota_okn, shirina_okn, raz_oboi);

            listBox1.Items.Add($"Площадь комнаты: {s}");
            listBox1.Items.Add($"Объем команты: {s3}");
            listBox1.Items.Add($"Количество рулонов обоев: {about_room}");
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void textBox7_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox5_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox6_TextChanged(object sender, EventArgs e)
        {

        }

        private void numericUpDown1_ValueChanged(object sender, EventArgs e)
        {

        }
    }
}
