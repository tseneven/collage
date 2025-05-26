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

namespace prakt16_4
{
    public partial class Form1 : Form
    {
        List<string[]> countrys = new List<string[]>();
        string[] cn = new string[7];
        int i = 0;
        


        public Form1() 
        {
            InitializeComponent();
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if(i < 7)
            {
                string[] country = new string[2];
                country[0] = cn[i];
                country[1] = textBox1.Text;
                countrys.Add(country);
                listBox1.Items.Add(country[0]);
                listBox1.Items.Add(country[1]);
                
                i++;
                if (i < 7)
                {
                    label3.Text = cn[i];
                }
                else
                {
                    label3.Text = "";
                    button1.Enabled = false;
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            var sorted1 = countrys.OrderBy(c => c[0].Length);
            var sorted = countrys.Where(p => long.Parse(p[1]) > 104000000).OrderBy(p=>p[0]);
            listBox1.Items.Clear();
            foreach (var cn in sorted)
            {
                listBox2.Items.Add(cn[0]);
                listBox2.Items.Add(cn[1]);
            }
            foreach (var cn in sorted1)
            {
                listBox1.Items.Add(cn[0]);
                listBox1.Items.Add(cn[1]);
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            StreamReader SW = new StreamReader("txt.txt");
            for(int i = 0; i < 7; i++)
            {
                cn[i] = SW.ReadLine();
            }
            SW.Close();
            label3.Text = cn[0];
        }
    }
}
