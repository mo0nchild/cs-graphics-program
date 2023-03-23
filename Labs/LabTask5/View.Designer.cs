namespace CSGraphicsProgram.Labs.LabTask5
{
    partial class View
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
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
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            pictureBox = new PictureBox();
            comboBox = new ComboBox();
            trackBar = new TrackBar();
            update_button = new Button();
            label1 = new Label();
            textBox = new TextBox();
            label2 = new Label();
            ((System.ComponentModel.ISupportInitialize)pictureBox).BeginInit();
            ((System.ComponentModel.ISupportInitialize)trackBar).BeginInit();
            SuspendLayout();
            // 
            // pictureBox
            // 
            pictureBox.BackgroundImageLayout = ImageLayout.Stretch;
            pictureBox.BorderStyle = BorderStyle.FixedSingle;
            pictureBox.Location = new Point(12, 57);
            pictureBox.Name = "pictureBox";
            pictureBox.Size = new Size(760, 392);
            pictureBox.TabIndex = 0;
            pictureBox.TabStop = false;
            // 
            // comboBox
            // 
            comboBox.BackColor = Color.White;
            comboBox.DropDownStyle = ComboBoxStyle.DropDownList;
            comboBox.Font = new Font("Segoe UI", 10F, FontStyle.Regular, GraphicsUnit.Point);
            comboBox.FormattingEnabled = true;
            comboBox.Location = new Point(240, 26);
            comboBox.Name = "comboBox";
            comboBox.Size = new Size(190, 25);
            comboBox.TabIndex = 1;
            // 
            // trackBar
            // 
            trackBar.Location = new Point(646, 12);
            trackBar.Name = "trackBar";
            trackBar.Size = new Size(126, 45);
            trackBar.TabIndex = 2;
            trackBar.TickStyle = TickStyle.Both;
            trackBar.Value = 1;
            // 
            // update_button
            // 
            update_button.BackColor = Color.White;
            update_button.Font = new Font("Segoe UI", 10F, FontStyle.Regular, GraphicsUnit.Point);
            update_button.Location = new Point(12, 22);
            update_button.Name = "update_button";
            update_button.Size = new Size(80, 30);
            update_button.TabIndex = 3;
            update_button.Text = "Загрузить";
            update_button.UseVisualStyleBackColor = false;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(240, 8);
            label1.Name = "label1";
            label1.Size = new Size(130, 15);
            label1.TabIndex = 5;
            label1.Text = "Доступные анимации:";
            // 
            // textBox
            // 
            textBox.BackColor = Color.White;
            textBox.BorderStyle = BorderStyle.FixedSingle;
            textBox.Font = new Font("Segoe UI", 10F, FontStyle.Regular, GraphicsUnit.Point);
            textBox.Location = new Point(540, 26);
            textBox.Name = "textBox";
            textBox.ReadOnly = true;
            textBox.Size = new Size(87, 25);
            textBox.TabIndex = 6;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(540, 8);
            label2.Name = "label2";
            label2.Size = new Size(63, 15);
            label2.TabIndex = 7;
            label2.Text = "Интервал:";
            // 
            // View
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(784, 461);
            Controls.Add(label2);
            Controls.Add(textBox);
            Controls.Add(label1);
            Controls.Add(update_button);
            Controls.Add(trackBar);
            Controls.Add(comboBox);
            Controls.Add(pictureBox);
            MaximumSize = new Size(800, 500);
            MinimumSize = new Size(800, 500);
            Name = "View";
            StartPosition = FormStartPosition.CenterParent;
            Text = "Лабораторная работа 5";
            ((System.ComponentModel.ISupportInitialize)pictureBox).EndInit();
            ((System.ComponentModel.ISupportInitialize)trackBar).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private PictureBox pictureBox;
        private ComboBox comboBox;
        private TrackBar trackBar;
        private Button update_button;
        private Label label1;
        private TextBox textBox;
        private Label label2;
    }
}