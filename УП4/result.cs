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
    public partial class result: Form
    {
        List<string[]> datas = new List<string[]>();
        int curr_data = 0;
        public result()
        {
            InitializeComponent();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void result_Load(object sender, EventArgs e)
        {
            StreamReader sr = new StreamReader("result.txt");


            while (!sr.EndOfStream)
            {
                string[] data = new string[3];
                data[0] = sr.ReadLine();
                data[1] = sr.ReadLine();
                data[2] = sr.ReadLine();
                datas.Add(data);
                curr_data++;
            }
            sr.Close();
            curr_data = 0;

            dataGridView1.ColumnCount = 3;
            dataGridView1.RowCount = datas.Count;
            dataGridView1.Columns[0].HeaderCell.Value = "Имя";
            dataGridView1.Columns[1].HeaderCell.Value = "Количество вопросов";
            dataGridView1.Columns[2].HeaderCell.Value = "Количество правильных ответов";

            for (int i = 0; i < datas.Count; i++)
            {
                dataGridView1[0, i].Value = datas[i][0];
                dataGridView1[1, i].Value = datas[i][1];  
                dataGridView1[2, i].Value = datas[i][2];  
            }
        }
    }
}
