using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static CSGraphicsProgram.Labs.LabTask4.ModelData;

namespace CSGraphicsProgram.Labs.LabTask4
{
    public static class TaskLogic : System.Object
    {
        public static System.Int32 BorderWidth { get; set; } = 5;
        public static void DrawCustomBezier(this Graphics graphic, ModelRecord record, Color color, 
            bool draw_info = false)
        {
            using (var border = new Pen(new SolidBrush(color), TaskLogic.BorderWidth))
            {
                graphic.DrawBezier(border, record.start, record.control1, record.control2, record.finish);
            }
            if (draw_info == false) return;
            using (var info_brush = new SolidBrush(Color.Green))
            {
                graphic.FillEllipse(info_brush, record.start.X - 5, record.start.Y - 5, 10, 10);
                graphic.FillEllipse(info_brush, record.control1.X - 5, record.control1.Y - 5, 10, 10);

                graphic.FillEllipse(info_brush, record.control2.X - 5, record.control2.Y - 5, 10, 10);
                graphic.FillEllipse(info_brush, record.finish.X - 5, record.finish.Y - 5, 10, 10);

                using var info_pen = new Pen(info_brush, 2);
                graphic.DrawLine(info_pen, record.start, record.control1);
                graphic.DrawLine(info_pen, record.finish, record.control2);
            }
        }
    }

    public sealed class ModelData : System.Object
    {
        public record ModelRecord(Point start, Point control1, Point control2, Point finish);

        public static List<ModelData.ModelRecord> BirdModel = new()
        {
            new ModelRecord(new Point(210, 380), new Point(250, 370), new Point(260, 350), new Point(270, 310)),
            new ModelRecord(new Point(270, 310), new Point(310, 260), new Point(340, 250), new Point(390, 270)),
            new ModelRecord(new Point(390, 270), new Point(440, 350), new Point(440, 360), new Point(570, 300)),
            new ModelRecord(new Point(570, 300), new Point(620, 260), new Point(640, 260), new Point(680, 300)),

            new ModelRecord(new Point(680, 300), new Point(640, 325), new Point(630, 330), new Point(630, 380)),
            new ModelRecord(new Point(630, 380), new Point(580, 600), new Point(400, 570), new Point(320, 490)),
            new ModelRecord(new Point(320, 490), new Point(290, 400), new Point(260, 380), new Point(210, 380)),

            new ModelRecord(new Point(410, 400), new Point(470, 430), new Point(520, 420), new Point(560, 370)),
            new ModelRecord(new Point(560, 370), new Point(550, 470), new Point(470, 550), new Point(360, 410)),
        };

        public ModelData() : base() { }
    }
}
