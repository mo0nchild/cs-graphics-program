using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Text;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CSGraphicsProgram.Labs.LabTask3
{
    public partial class View : Form
    {
        protected TaskLogic LogicInstance { get; private set; } = default!;
        protected System.Drawing.Point CameraPosition { get; private set; } = new();
        private System.Drawing.Bitmap FractalVisualState { get; set; } = default!;

        protected virtual System.Int32 FractalLength { get; private set; }
        protected virtual System.Double CameraMovingSpeed { get; } = 10;

        public View() : base()
        {
            this.InitializeComponent(); this.DoubleBuffered = true;
            this.buttonColor.BackColor = Color.Crimson;
            this.LogicInstance = new TaskLogic(new TaskLogic.DrawBorderHandler(this.DrawFractal));

            this.buttonColor.Click += new EventHandler(this.ButtonColorClickHandler);
            this.numericUpDown1.ValueChanged += new EventHandler(this.NumericUpDownValueChanged);

            this.trackBar.ValueChanged += new EventHandler(this.TrackBarValueChanged);
            this.MouseWheel += new MouseEventHandler(this.ViewMouseWheelHandler);

            this.pictureBox.MouseMove += new MouseEventHandler(PictureBoxMouseMove);
            this.pictureBox.MouseDown += (sender, args) => this.camera_dragmode = true;
            this.pictureBox.MouseUp += (sender, args) => this.camera_dragmode = false;

            this.NumericUpDownValueChanged(this, EventArgs.Empty);
            this.TrackBarValueChanged(this, EventArgs.Empty);

            this.pictureBox.Paint += delegate(object? sender, PaintEventArgs args)
            {
                args.Graphics.Clear(Color.White);
                args.Graphics.DrawImage(this.FractalVisualState, this.CameraPosition);
            };
        }

        private void NumericUpDownValueChanged(object? sender, EventArgs args)
        {
            this.FractalLength = (int)this.numericUpDown1.Value;
            this.CameraPosition = new Point((this.pictureBox.Width - this.FractalLength) / 2,
                (int)(this.pictureBox.Height / 2 - this.FractalLength * 3.0 / 4));
        }

        private System.Drawing.Point movingposition_buffer = new Point();
        private System.Boolean camera_dragmode = default;

        /// <summary>Обрабатывает событие перемещения курсора мыши в области элемента pictureBox</summary>
        /// <param name="sender">Элемент, который создал событие</param>
        /// <param name="args">Экземпляр события класса MouseEventArgs</param>
        private void PictureBoxMouseMove(object? sender, MouseEventArgs args)
        {
            if (this.camera_dragmode == false) return;
            int moving_direction_x = Math.Sign(args.X - this.movingposition_buffer.X),
                moving_direction_y = Math.Sign(args.Y - this.movingposition_buffer.Y);

            var x_offset = this.CameraPosition.X + moving_direction_x * this.CameraMovingSpeed;
            var y_offset = this.CameraPosition.Y - moving_direction_y * this.CameraMovingSpeed;

            this.CameraPosition = new Point((int)x_offset, (int)y_offset);
            this.movingposition_buffer = args.Location;

            this.pictureBox.Invalidate();
            this.labelPosition.Text = $"{this.CameraPosition}";
        }

        /// <summary>Производит обновление буферного массива изображения новым фрактальным рисунком</summary>
        /// <param name="y_offset">Значение, отвечающее за смещение отрисовки рисунка по оси У</param>
        protected virtual void UpdatePictureBox(int y_offset)
        {
            this.FractalVisualState = new Bitmap(this.FractalLength, this.FractalLength);
            int width = this.pictureBox.Width, height = this.pictureBox.Height;
            try { 
                this.LogicInstance.KochCurveAlgorithm(new Point(0, y_offset), new Point(FractalLength, y_offset), 
                    this.trackBar.Value); 
            }
            catch (Exception error) { MessageBox.Show(error.Message); }
        }

        /// <summary>Обрабатывает событие изменения значения ползунка trackBar</summary>
        /// <param name="sender">Элемент, который создал событие</param>
        /// <param name="args">Экземпляр события класса EventArgs</param>
        private void TrackBarValueChanged(object? sender, EventArgs args)
        {
            this.labelScale.Text = $"Value: {(double)this.trackBar.Value / this.trackBar.Maximum * 100}%";
            this.UpdatePictureBox(this.FractalLength);
            this.pictureBox.Invalidate();
        }

        /// <summary>Обрабатывает событие прокрутки колесика мыши в области элемента pictureBox</summary>
        /// <param name="sender">Элемент, который создал событие</param>
        /// <param name="args">Экземпляр события класса MouseEventArgs</param>
        private void ViewMouseWheelHandler(object? sender, MouseEventArgs args)
        { try { this.trackBar.Value += (Math.Sign(args.Delta)); } catch (ArgumentException) {  } }

        /// <summary>Обработчик отрисовки контура фрактального рисунка</summary>
        /// <param name="begin">Начальная вершина отрисовки</param>
        /// <param name="end">Конечная вершина отрисовки</param>
        protected virtual void DrawFractal(Point begin, Point end)
        {
            using (var graphic = Graphics.FromImage(this.FractalVisualState))
            { graphic.DrawLine(new Pen(new SolidBrush(this.buttonColor.BackColor), 5), begin, end); }
        }

        protected virtual void ButtonColorClickHandler(object? sender, EventArgs args)
        {
            using (var color_dialog = new ColorDialog())
            { if(color_dialog.ShowDialog() == DialogResult.OK) this.buttonColor.BackColor = color_dialog.Color; }
        }
    }
}
