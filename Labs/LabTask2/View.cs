using System.Drawing;
using System.Drawing.Drawing2D;
using System.Net.Http.Headers;

namespace CSGraphicsProgram.Labs.LabTask2
{
    public partial class View : Form
    {
        protected ModelData CatModel { get; private set; } = new ModelData();

        public View() : base()
        {
            this.InitializeComponent();
            this.pictureBox.Click += new EventHandler(PictureBox_Click);
        }

        private void PictureBox_Click(object? sender, EventArgs arg)
        {
            using (var graphic = this.pictureBox.CreateGraphics())
            {
                graphic.Clear(Color.White);

                graphic.FillPolygon(new SolidBrush(Color.FromArgb(229, 229, 229)), this.CatModel.FaceBackground);
                graphic.FillPolygon(new SolidBrush(Color.FromArgb(229, 229, 229)), this.CatModel.LegsBackground);

                graphic.FillPolygon(Brushes.White, this.CatModel.SecondFaceBackground);
                graphic
                    .DrawCatHair()
                    .DrawCatNouse()
                    .DrawCatMustache()
                    .DrawCatEyes()
                    .DrawCatEars();

                graphic.DrawCurve(new Pen(Brushes.Black, 5), this.CatModel.FaceBackground);
                graphic.DrawCurve(new Pen(Brushes.Black, 4), this.CatModel.LegsBackground);
            }
        }
    }
}