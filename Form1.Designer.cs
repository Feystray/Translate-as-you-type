namespace AutoTrans
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            label1 = new Label();
            label2 = new Label();
            label3 = new Label();
            label4 = new Label();
            textBox1 = new TextBox();
            button4 = new Button();
            checkBox1 = new CheckBox();
            comboBox1 = new ComboBox();
            label6 = new Label();
            comboBox2 = new ComboBox();
            label7 = new Label();
            label8 = new Label();
            comboBox3 = new ComboBox();
            label9 = new Label();
            label10 = new Label();
            label11 = new Label();
            label5 = new TextBox();
            checkBox2 = new CheckBox();
            label12 = new Label();
            label13 = new Label();
            SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Comic Sans MS", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label1.Location = new Point(17, 62);
            label1.Name = "label1";
            label1.Size = new Size(113, 21);
            label1.TabIndex = 0;
            label1.Text = "Translate to :";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Comic Sans MS", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label2.Location = new Point(13, 112);
            label2.Name = "label2";
            label2.Size = new Size(117, 21);
            label2.TabIndex = 2;
            label2.Text = "Start Button :";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Font = new Font("Comic Sans MS", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label3.Location = new Point(27, 160);
            label3.Name = "label3";
            label3.Size = new Size(104, 21);
            label3.TabIndex = 4;
            label3.Text = "End Button :";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Font = new Font("Comic Sans MS", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label4.Location = new Point(22, 297);
            label4.Name = "label4";
            label4.Size = new Size(102, 21);
            label4.TabIndex = 6;
            label4.Text = "Application :";
            // 
            // textBox1
            // 
            textBox1.Location = new Point(181, 295);
            textBox1.Name = "textBox1";
            textBox1.Size = new Size(145, 28);
            textBox1.TabIndex = 7;
            // 
            // button4
            // 
            button4.Font = new Font("Comic Sans MS", 9F, FontStyle.Regular, GraphicsUnit.Point, 0);
            button4.Location = new Point(121, 475);
            button4.Name = "button4";
            button4.Size = new Size(50, 43);
            button4.TabIndex = 0;
            button4.Text = "▶";
            button4.UseVisualStyleBackColor = true;
            button4.Click += button4_Click;
            // 
            // checkBox1
            // 
            checkBox1.AutoSize = true;
            checkBox1.Font = new Font("Comic Sans MS", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
            checkBox1.Location = new Point(67, 424);
            checkBox1.Name = "checkBox1";
            checkBox1.Size = new Size(165, 25);
            checkBox1.TabIndex = 11;
            checkBox1.Text = "Type via Clipboard";
            checkBox1.UseVisualStyleBackColor = true;
            // 
            // comboBox1
            // 
            comboBox1.FlatStyle = FlatStyle.System;
            comboBox1.Font = new Font("Comic Sans MS", 9F, FontStyle.Regular, GraphicsUnit.Point, 0);
            comboBox1.FormattingEnabled = true;
            comboBox1.Items.AddRange(new object[] { "Hindi", "English", "Thai", "Japanese", "Spanish" });
            comboBox1.Location = new Point(203, 62);
            comboBox1.Name = "comboBox1";
            comboBox1.Size = new Size(124, 28);
            comboBox1.TabIndex = 12;
            comboBox1.SelectedValueChanged += comboBox1_SelectedValueChanged;
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Font = new Font("Comic Sans MS", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label6.Location = new Point(26, 342);
            label6.Name = "label6";
            label6.Size = new Size(98, 21);
            label6.TabIndex = 13;
            label6.Text = "Translator: ";
            // 
            // comboBox2
            // 
            comboBox2.FlatStyle = FlatStyle.System;
            comboBox2.Font = new Font("Comic Sans MS", 9F, FontStyle.Regular, GraphicsUnit.Point, 0);
            comboBox2.FormattingEnabled = true;
            comboBox2.Items.AddRange(new object[] { "LLM", "Gemini", "Chatgpt" });
            comboBox2.Location = new Point(194, 335);
            comboBox2.Name = "comboBox2";
            comboBox2.Size = new Size(124, 28);
            comboBox2.TabIndex = 14;
            comboBox2.Text = "LLM";
            comboBox2.SelectedIndexChanged += comboBox2_SelectedIndexChanged;
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.Font = new Font("Comic Sans MS", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label7.Location = new Point(9, 213);
            label7.Name = "label7";
            label7.Size = new Size(114, 21);
            label7.TabIndex = 15;
            label7.Text = "Clear Button :";
            // 
            // label8
            // 
            label8.AutoSize = true;
            label8.Font = new Font("Comic Sans MS", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label8.Location = new Point(10, 24);
            label8.Name = "label8";
            label8.Size = new Size(134, 21);
            label8.TabIndex = 17;
            label8.Text = "Input Language :";
            // 
            // comboBox3
            // 
            comboBox3.FlatStyle = FlatStyle.System;
            comboBox3.Font = new Font("Comic Sans MS", 9F, FontStyle.Regular, GraphicsUnit.Point, 0);
            comboBox3.FormattingEnabled = true;
            comboBox3.Items.AddRange(new object[] { "Auto-Detect", "Hindi", "English", "Thai", "Japanese", "Spanish" });
            comboBox3.Location = new Point(203, 24);
            comboBox3.Name = "comboBox3";
            comboBox3.Size = new Size(124, 28);
            comboBox3.TabIndex = 18;
            comboBox3.SelectedValueChanged += comboBox3_SelectedValueChanged;
            // 
            // label9
            // 
            label9.BackColor = SystemColors.ActiveBorder;
            label9.FlatStyle = FlatStyle.System;
            label9.Location = new Point(174, 113);
            label9.Name = "label9";
            label9.Size = new Size(157, 25);
            label9.TabIndex = 19;
            label9.Text = "Enter";
            label9.TextAlign = ContentAlignment.MiddleCenter;
            label9.Click += button2_Click;
            // 
            // label10
            // 
            label10.BackColor = SystemColors.ActiveBorder;
            label10.FlatStyle = FlatStyle.System;
            label10.Location = new Point(174, 159);
            label10.Name = "label10";
            label10.Size = new Size(157, 25);
            label10.TabIndex = 20;
            label10.Text = "`";
            label10.TextAlign = ContentAlignment.MiddleCenter;
            label10.Click += button2_Click;
            // 
            // label11
            // 
            label11.BackColor = SystemColors.ActiveBorder;
            label11.FlatStyle = FlatStyle.System;
            label11.Location = new Point(174, 213);
            label11.Name = "label11";
            label11.Size = new Size(157, 25);
            label11.TabIndex = 21;
            label11.Text = "None";
            label11.TextAlign = ContentAlignment.MiddleCenter;
            label11.Click += button2_Click;
            // 
            // label5
            // 
            label5.Enabled = false;
            label5.ForeColor = SystemColors.MenuText;
            label5.Location = new Point(355, 12);
            label5.Multiline = true;
            label5.Name = "label5";
            label5.ScrollBars = ScrollBars.Vertical;
            label5.Size = new Size(284, 506);
            label5.TabIndex = 22;
            // 
            // checkBox2
            // 
            checkBox2.AutoSize = true;
            checkBox2.Font = new Font("Comic Sans MS", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
            checkBox2.Location = new Point(67, 393);
            checkBox2.Name = "checkBox2";
            checkBox2.Size = new Size(156, 25);
            checkBox2.TabIndex = 23;
            checkBox2.Text = "Delete Start Key";
            checkBox2.UseVisualStyleBackColor = true;
            checkBox2.CheckedChanged += checkBox2_CheckedChanged;
            // 
            // label12
            // 
            label12.AutoSize = true;
            label12.Font = new Font("Comic Sans MS", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label12.Location = new Point(9, 259);
            label12.Name = "label12";
            label12.Size = new Size(117, 21);
            label12.TabIndex = 24;
            label12.Text = "Pause Button :";
            // 
            // label13
            // 
            label13.BackColor = SystemColors.ActiveBorder;
            label13.FlatStyle = FlatStyle.System;
            label13.Location = new Point(174, 258);
            label13.Name = "label13";
            label13.Size = new Size(157, 25);
            label13.TabIndex = 25;
            label13.Text = "None";
            label13.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(9F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(651, 530);
            Controls.Add(label13);
            Controls.Add(label12);
            Controls.Add(checkBox2);
            Controls.Add(label5);
            Controls.Add(label11);
            Controls.Add(label10);
            Controls.Add(label9);
            Controls.Add(comboBox3);
            Controls.Add(label8);
            Controls.Add(label7);
            Controls.Add(comboBox2);
            Controls.Add(label6);
            Controls.Add(comboBox1);
            Controls.Add(checkBox1);
            Controls.Add(button4);
            Controls.Add(textBox1);
            Controls.Add(label4);
            Controls.Add(label3);
            Controls.Add(label2);
            Controls.Add(label1);
            Font = new Font("Comic Sans MS", 9F, FontStyle.Regular, GraphicsUnit.Point, 0);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            Icon = (Icon)resources.GetObject("$this.Icon");
            MaximizeBox = false;
            Name = "Form1";
            Text = "Translate As You Type";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label label1;
        private Label label2;
        private Label label3;
        private Label label4;
        private TextBox textBox1;
        private Button button4;
        private CheckBox checkBox1;
        private ComboBox comboBox1;
        private Label label6;
        private ComboBox comboBox2;
        private Label label7;
        private Label label8;
        private ComboBox comboBox3;
        private Label label9;
        private Label label10;
        private Label label11;
        private TextBox label5;
        private CheckBox checkBox2;
        private Label label12;
        private Label label13;
    }
}
