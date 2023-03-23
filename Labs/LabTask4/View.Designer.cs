namespace CSGraphicsProgram.Labs.LabTask4
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
            checkBox = new CheckBox();
            label = new Label();
            button_color = new Button();
            ((System.ComponentModel.ISupportInitialize)pictureBox).BeginInit();
            SuspendLayout();
            // 
            // pictureBox
            // 
            pictureBox.BorderStyle = BorderStyle.FixedSingle;
            pictureBox.Location = new Point(12, 32);
            pictureBox.Name = "pictureBox";
            pictureBox.Size = new Size(860, 817);
            pictureBox.TabIndex = 0;
            pictureBox.TabStop = false;
            // 
            // checkBox
            // 
            checkBox.AutoSize = true;
            checkBox.Font = new Font("Segoe UI", 10F, FontStyle.Regular, GraphicsUnit.Point);
            checkBox.Location = new Point(629, 7);
            checkBox.Name = "checkBox";
            checkBox.Size = new Size(206, 23);
            checkBox.TabIndex = 1;
            checkBox.Text = "Отрисовать направляющие:";
            checkBox.UseVisualStyleBackColor = true;
            // 
            // label
            // 
            label.AutoSize = true;
            label.Font = new Font("Segoe UI", 10F, FontStyle.Regular, GraphicsUnit.Point);
            label.Location = new Point(12, 9);
            label.Name = "label";
            label.Size = new Size(45, 19);
            label.TabIndex = 2;
            label.Text = "label1";
            // 
            // button_color
            // 
            button_color.Location = new Point(842, 5);
            button_color.Name = "button_color";
            button_color.Size = new Size(30, 23);
            button_color.TabIndex = 3;
            button_color.UseVisualStyleBackColor = true;
            // 
            // View
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(884, 861);
            Controls.Add(button_color);
            Controls.Add(label);
            Controls.Add(checkBox);
            Controls.Add(pictureBox);
            MaximumSize = new Size(900, 900);
            MinimumSize = new Size(900, 900);
            Name = "View";
            Text = "Лабораторная работа 4";
            ((System.ComponentModel.ISupportInitialize)pictureBox).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private PictureBox pictureBox;
        private CheckBox checkBox;
        private Label label;
        private Button button_color;
    }
}