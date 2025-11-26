namespace KuznetsovAA_0102_02
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
            label1 = new Label();
            label2 = new Label();
            listBox1 = new ListBox();
            listBox2 = new ListBox();
            radioButton1 = new RadioButton();
            radioButton2 = new RadioButton();
            textBox1 = new TextBox();
            textBox2 = new TextBox();
            textBox3 = new TextBox();
            hdd_textBox = new TextBox();
            button1 = new Button();
            label3 = new Label();
            label4 = new Label();
            label5 = new Label();
            hdd = new Label();
            SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(103, 28);
            label1.Name = "label1";
            label1.Size = new Size(163, 20);
            label1.TabIndex = 0;
            label1.Text = "Базовые компьютеры";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(471, 28);
            label2.Name = "label2";
            label2.Size = new Size(187, 20);
            label2.TabIndex = 1;
            label2.Text = "Наследники компьютеры";
            // 
            // listBox1
            // 
            listBox1.FormattingEnabled = true;
            listBox1.Location = new Point(22, 79);
            listBox1.Name = "listBox1";
            listBox1.Size = new Size(339, 124);
            listBox1.TabIndex = 2;
            // 
            // listBox2
            // 
            listBox2.FormattingEnabled = true;
            listBox2.Location = new Point(385, 79);
            listBox2.Name = "listBox2";
            listBox2.Size = new Size(339, 124);
            listBox2.TabIndex = 3;
            // 
            // radioButton1
            // 
            radioButton1.AutoSize = true;
            radioButton1.Location = new Point(32, 285);
            radioButton1.Name = "radioButton1";
            radioButton1.Size = new Size(132, 24);
            radioButton1.TabIndex = 4;
            radioButton1.TabStop = true;
            radioButton1.Text = "Базовый класс";
            radioButton1.UseVisualStyleBackColor = true;
            radioButton1.CheckedChanged += radioButton1_CheckedChanged_1;
            // 
            // radioButton2
            // 
            radioButton2.AutoSize = true;
            radioButton2.Location = new Point(32, 315);
            radioButton2.Name = "radioButton2";
            radioButton2.Size = new Size(148, 24);
            radioButton2.TabIndex = 5;
            radioButton2.TabStop = true;
            radioButton2.Text = "Класс-наследник";
            radioButton2.UseVisualStyleBackColor = true;
            radioButton2.CheckedChanged += radioButton2_CheckedChanged;
            // 
            // textBox1
            // 
            textBox1.Location = new Point(22, 239);
            textBox1.Name = "textBox1";
            textBox1.Size = new Size(125, 27);
            textBox1.TabIndex = 6;
            // 
            // textBox2
            // 
            textBox2.Location = new Point(171, 239);
            textBox2.Name = "textBox2";
            textBox2.Size = new Size(125, 27);
            textBox2.TabIndex = 7;
            // 
            // textBox3
            // 
            textBox3.Location = new Point(320, 239);
            textBox3.Name = "textBox3";
            textBox3.Size = new Size(125, 27);
            textBox3.TabIndex = 8;
            // 
            // hdd_textBox
            // 
            hdd_textBox.Location = new Point(471, 239);
            hdd_textBox.Name = "hdd_textBox";
            hdd_textBox.Size = new Size(125, 27);
            hdd_textBox.TabIndex = 9;
            // 
            // button1
            // 
            button1.Location = new Point(471, 343);
            button1.Name = "button1";
            button1.Size = new Size(187, 61);
            button1.TabIndex = 10;
            button1.Text = "Добавить";
            button1.UseVisualStyleBackColor = true;
            button1.Click += button1_Click;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(27, 214);
            label3.Name = "label3";
            label3.Size = new Size(108, 20);
            label3.TabIndex = 11;
            label3.Text = "Название CPU";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(171, 214);
            label4.Name = "label4";
            label4.Size = new Size(38, 20);
            label4.TabIndex = 12;
            label4.Text = "МГц";
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new Point(320, 214);
            label5.Name = "label5";
            label5.Size = new Size(89, 20);
            label5.TabIndex = 13;
            label5.Text = "Объем ОЗУ";
            // 
            // hdd
            // 
            hdd.AutoSize = true;
            hdd.Location = new Point(471, 214);
            hdd.Name = "hdd";
            hdd.Size = new Size(35, 20);
            hdd.TabIndex = 14;
            hdd.Text = "hdd";
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(754, 450);
            Controls.Add(hdd);
            Controls.Add(label5);
            Controls.Add(label4);
            Controls.Add(label3);
            Controls.Add(button1);
            Controls.Add(hdd_textBox);
            Controls.Add(textBox3);
            Controls.Add(textBox2);
            Controls.Add(textBox1);
            Controls.Add(radioButton2);
            Controls.Add(radioButton1);
            Controls.Add(listBox2);
            Controls.Add(listBox1);
            Controls.Add(label2);
            Controls.Add(label1);
            Name = "Form1";
            Text = "Form1";
            Load += Form1_Load;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label label1;
        private Label label2;
        private ListBox listBox1;
        private ListBox listBox2;
        private RadioButton radioButton1;
        private RadioButton radioButton2;
        private TextBox textBox1;
        private TextBox textBox2;
        private TextBox textBox3;
        private TextBox hdd_textBox;
        private Button button1;
        private Label label3;
        private Label label4;
        private Label label5;
        private Label hdd;
    }
}
