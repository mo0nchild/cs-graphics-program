using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CSGraphicsProgram.Labs.LabTask4
{
    public partial class View : Form
    {
        public record VertexData(ModelData.ModelRecord Record, System.Drawing.Point Vertex);
        private View.VertexData? SelectedVertex { get; set; } = default!;

        protected ObservableCollection<ModelData.ModelRecord> VertexList { get; private set; } = new(ModelData.BirdModel);
        protected System.Drawing.Point CursorPosition { get; private set; } = new();

        public const System.Int32 VertexSize = 10;
        public View() : base()
        {
            this.InitializeComponent();
            this.pictureBox.MouseMove += new MouseEventHandler(this.PictureBox_MouseMove);
            this.pictureBox.MouseDown += new MouseEventHandler(this.PictureBox_MouseDown);

            this.pictureBox.Paint += new PaintEventHandler(this.PictureBox_Paint);
            this.button_color.Click += new EventHandler(this.Button_color_Click);

            this.VertexList.CollectionChanged += (sender, args) => this.pictureBox.Invalidate();

            this.checkBox.CheckedChanged += delegate (object? sender, EventArgs args)
            {
                this.pictureBox.Invalidate();
            };
            this.checkBox.Checked = true;
            this.button_color.BackColor = Color.Crimson;
        }

        /// <summary>Вычисление выбора вершины изменения кривой</summary>
        /// <param name="sender">Объект, создавший событие</param>
        /// <param name="args">Экземпляр, описывающий событие</param>
        private void PictureBox_MouseDown(object? sender, MouseEventArgs args)
        {
            if(this.SelectedVertex == null)
            {
                this.VertexList.ToList().ForEach(delegate (ModelData.ModelRecord record)
                {
                    if (!CheckVertexHit(record.control1) && !CheckVertexHit(record.control2)) return;

                    var selected_vertex = CheckVertexHit(record.control1) ? record.control1 : record.control2;
                    this.SelectedVertex = new VertexData(record, selected_vertex);
                    this.pictureBox.Invalidate();
                });
            }
            else
            {
                var change_state = default(bool);
                for(var index = 0; index < this.VertexList.Count; index++)
                {
                    if (this.VertexList[index] != this.SelectedVertex.Record) continue;

                    this.VertexList[index] = (this.VertexList[index].control1 == this.SelectedVertex.Vertex) 
                        ? this.SelectedVertex.Record with { control1 = args.Location } 
                        : this.SelectedVertex.Record with { control2 = args.Location };
                    change_state = true;
                }
                if (!change_state) return;
                this.SelectedVertex = default!;
            }
            System.Boolean CheckVertexHit(System.Drawing.Point vertex)
            { 
                return Math.Sqrt(Math.Pow(args.X - vertex.X, 2) + Math.Pow(args.Y - vertex.Y, 2)) <= View.VertexSize; 
            }
        }

        private void Button_color_Click(object? sender, EventArgs args)
        {
            using (var color_dialog = new ColorDialog() { SolidColorOnly = true })
            {
                if (color_dialog.ShowDialog() != DialogResult.OK) return;
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
            foreach (var item in this.VertexList)
            {
                args.Graphics.DrawCustomBezier(item, this.button_color.BackColor, this.checkBox.Checked);
            }
            using (var border_pen = new Pen(this.button_color.BackColor, TaskLogic.BorderWidth))
            {
                args.Graphics.DrawCurve(border_pen, new Point[] { new(422, 547), new(410, 590), new(370, 580) }, 0.5f);
                args.Graphics.DrawCurve(border_pen, new Point[] { new(487, 548), new(500, 590), new(460, 585) }, 0.5f);

                var vertex_size = new Size(View.VertexSize, View.VertexSize);
                args.Graphics.DrawEllipse(border_pen, new Rectangle(new Point(315, 307), vertex_size));
                if (this.SelectedVertex != null)
                {
                    using var vertex_brush = new SolidBrush(Color.Lavender);
                    var point = this.SelectedVertex.Vertex;
                    args.Graphics.FillEllipse(vertex_brush, new Rectangle(new Point(point.X - View.VertexSize / 2,
                        point.Y - View.VertexSize / 2), vertex_size));
                }
            }
            using var cursor_brush = new SolidBrush(Color.LawnGreen);
            var cursor_geometry = new Rectangle(this.CursorPosition.X - 5, this.CursorPosition.Y - 5, 10, 10);

            args.Graphics.FillEllipse(cursor_brush, cursor_geometry);
        }
    }
}
