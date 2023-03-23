using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CSGraphicsProgram.Labs.LabTask4
{
    public partial class View : Form
    {
        protected System.Drawing.Point CursorPosition { get; private set; } = new();
        public View() : base()
        {
            this.InitializeComponent();
            this.pictureBox.Paint += new PaintEventHandler(PictureBox_Paint);
            this.pictureBox.MouseMove += new MouseEventHandler(PictureBox_MouseMove);

            this.button_color.Click += new EventHandler(Button_color_Click);
            this.checkBox.CheckedChanged += delegate (object? sender, EventArgs args)
            {
                this.pictureBox.Invalidate();
            };
            this.checkBox.Checked = true;
            this.button_color.BackColor = Color.Crimson;
        }

        private void Button_color_Click(object? sender, EventArgs args)
        {
            using (var color_dialog = new ColorDialog() { SolidColorOnly = true })
            {
                if(color_dialog.ShowDialog() != DialogResult.OK) return;
                this.button_color.BackColor = color_dialog.Color;
            }
        }

        private void PictureBox_MouseMove(object? sender, MouseEventArgs args)
        {
            this.label.Text = string.Format("X: {0}; Y: {1}", args.Location.X, args.Location.Y);
            this.CursorPosition = new Point(args.Location.X, args.Location.Y);

            this.pictureBox.Invalidate();
        }

        private void PictureBox_Paint(object? sender, PaintEventArgs args)
        {
            args.Graphics.Clear(Color.White);
            foreach (var item in ModelData.BirdModel)
            {
                args.Graphics.DrawCustomBezier(item, this.button_color.BackColor, this.checkBox.Checked);
            }
            using (var border_pen = new Pen(this.button_color.BackColor, TaskLogic.BorderWidth))
            {
                args.Graphics.DrawCurve(border_pen, new Point[] { new(422, 547), new(410, 590), new(370, 580) }, 0.5f);
                args.Graphics.DrawCurve(border_pen, new Point[] { new(487, 548), new(500, 590), new(460, 585) }, 0.5f);

                args.Graphics.DrawEllipse(border_pen, new Rectangle(315, 307, 30, 30));
            }
            using var cursor_brush = new SolidBrush(Color.LawnGreen);
            var cursor_geometry = new Rectangle(this.CursorPosition.X - 5, this.CursorPosition.Y - 5, 10, 10);

            args.Graphics.FillEllipse(cursor_brush, cursor_geometry);
        }
    }
}
