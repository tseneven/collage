namespace prakt12_v4_kuznetsov
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            panel1 = new Panel();
            listBox1 = new ListBox();
            label1 = new Label();
            button1 = new Button();
            dlina = new TextBox();
            shirinaa = new TextBox();
            visotaa = new TextBox();
            kol__okn = new TextBox();
            visota__okn = new TextBox();
            shirina__okn = new TextBox();
            label2 = new Label();
            shirina = new Label();
            visota = new Label();
            kol_okn = new Label();
            visota_okn = new Label();
            shirina_okn = new Label();
            numericUpDown1 = new NumericUpDown();
            label8 = new Label();
            panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)numericUpDown1).BeginInit();
            SuspendLayout();
            // 
            // panel1
            // 
            panel1.Controls.Add(listBox1);
            panel1.Controls.Add(label1);
            panel1.Location = new Point(52, 64);
            panel1.Name = "panel1";
            panel1.Size = new Size(250, 272);
            panel1.TabIndex = 0;
            // 
            // listBox1
            // 
            listBox1.FormattingEnabled = true;
            listBox1.Location = new Point(0, 0);
            listBox1.Name = "listBox1";
            listBox1.Size = new Size(250, 284);
            listBox1.TabIndex = 1;
            listBox1.SelectedIndexChanged += listBox1_SelectedIndexChanged;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(88, 24);
            label1.Name = "label1";
            label1.Size = new Size(50, 20);
            label1.TabIndex = 0;
            label1.Text = "label1";
            label1.Click += label1_Click;
            // 
            // button1
            // 
            button1.Location = new Point(336, 252);
            button1.Name = "button1";
            button1.Size = new Size(257, 84);
            button1.TabIndex = 1;
            button1.Text = "Вычислить";
            button1.UseVisualStyleBackColor = true;
            button1.Click += button1_Click;
            // 
            // dlina
            // 
            dlina.Location = new Point(308, 64);
            dlina.Name = "dlina";
            dlina.Size = new Size(96, 27);
            dlina.TabIndex = 2;
            dlina.TextChanged += textBox1_TextChanged;
            // 
            // shirinaa
            // 
            shirinaa.Location = new Point(422, 64);
            shirinaa.Name = "shirinaa";
            shirinaa.Size = new Size(91, 27);
            shirinaa.TabIndex = 3;
            shirinaa.TextChanged += textBox2_TextChanged;
            // 
            // visotaa
            // 
            visotaa.Location = new Point(539, 64);
            visotaa.Name = "visotaa";
            visotaa.Size = new Size(102, 27);
            visotaa.TabIndex = 4;
            visotaa.TextChanged += textBox3_TextChanged;
            // 
            // kol__okn
            // 
            kol__okn.Location = new Point(308, 128);
            kol__okn.Name = "kol__okn";
            kol__okn.Size = new Size(96, 27);
            kol__okn.TabIndex = 5;
            kol__okn.TextChanged += textBox4_TextChanged;
            // 
            // visota__okn
            // 
            visota__okn.Location = new Point(422, 128);
            visota__okn.Name = "visota__okn";
            visota__okn.Size = new Size(91, 27);
            visota__okn.TabIndex = 6;
            visota__okn.TextChanged += textBox5_TextChanged;
            // 
            // shirina__okn
            // 
            shirina__okn.Location = new Point(539, 128);
            shirina__okn.Name = "shirina__okn";
            shirina__okn.Size = new Size(102, 27);
            shirina__okn.TabIndex = 7;
            shirina__okn.TextChanged += textBox6_TextChanged;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(323, 41);
            label2.Name = "label2";
            label2.Size = new Size(57, 20);
            label2.TabIndex = 8;
            label2.Text = "Длина ";
            // 
            // shirina
            // 
            shirina.AutoSize = true;
            shirina.Location = new Point(432, 41);
            shirina.Name = "shirina";
            shirina.Size = new Size(67, 20);
            shirina.TabIndex = 9;
            shirina.Text = "Ширина";
            // 
            // visota
            // 
            visota.AutoSize = true;
            visota.Location = new Point(558, 41);
            visota.Name = "visota";
            visota.Size = new Size(59, 20);
            visota.TabIndex = 10;
            visota.Text = "Высота";
            // 
            // kol_okn
            // 
            kol_okn.AutoSize = true;
            kol_okn.Location = new Point(317, 105);
            kol_okn.Name = "kol_okn";
            kol_okn.Size = new Size(87, 20);
            kol_okn.TabIndex = 11;
            kol_okn.Text = "Кол-во окн";
            // 
            // visota_okn
            // 
            visota_okn.AutoSize = true;
            visota_okn.Location = new Point(440, 105);
            visota_okn.Name = "visota_okn";
            visota_okn.Size = new Size(59, 20);
            visota_okn.TabIndex = 12;
            visota_okn.Text = "Высота";
            // 
            // shirina_okn
            // 
            shirina_okn.AutoSize = true;
            shirina_okn.Location = new Point(558, 105);
            shirina_okn.Name = "shirina_okn";
            shirina_okn.Size = new Size(67, 20);
            shirina_okn.TabIndex = 13;
            shirina_okn.Text = "Ширина";
            // 
            // numericUpDown1
            // 
            numericUpDown1.Location = new Point(394, 186);
            numericUpDown1.Name = "numericUpDown1";
            numericUpDown1.Size = new Size(150, 27);
            numericUpDown1.TabIndex = 14;
            numericUpDown1.ValueChanged += numericUpDown1_ValueChanged;
            // 
            // label8
            // 
            label8.AutoSize = true;
            label8.Location = new Point(374, 158);
            label8.Name = "label8";
            label8.Size = new Size(183, 20);
            label8.TabIndex = 15;
            label8.Text = "Размер обоев(10 или 15)";
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(665, 450);
            Controls.Add(label8);
            Controls.Add(numericUpDown1);
            Controls.Add(shirina_okn);
            Controls.Add(visota_okn);
            Controls.Add(kol_okn);
            Controls.Add(visota);
            Controls.Add(shirina);
            Controls.Add(label2);
            Controls.Add(shirina__okn);
            Controls.Add(visota__okn);
            Controls.Add(kol__okn);
            Controls.Add(visotaa);
            Controls.Add(shirinaa);
            Controls.Add(dlina);
            Controls.Add(button1);
            Controls.Add(panel1);
            Name = "Form1";
            Text = "Form1";
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)numericUpDown1).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Panel panel1;
        private Label label1;
        private Button button1;
        private TextBox dlina;
        private TextBox shirinaa;
        private TextBox visotaa;
        private TextBox kol__okn;
        private TextBox visota__okn;
        private TextBox shirina__okn;
        private Label label2;
        private Label shirina;
        private Label visota;
        private Label kol_okn;
        private Label visota_okn;
        private Label shirina_okn;
        private ListBox listBox1;
        private NumericUpDown numericUpDown1;
        private Label label8;
    }
}
