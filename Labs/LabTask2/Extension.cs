using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace CSGraphicsProgram.Labs.LabTask2
{
    internal static class Extension : System.Object
    {
        public static ModelData CatModel { get; private set; } = new ModelData();

        public static Graphics DrawCatEyes(this Graphics graphic)
        {
            graphic.FillEllipse(Brushes.Black, new Rectangle(365 - 50 / 2, 200 - 50 / 2, 50, 50));
            graphic.FillEllipse(Brushes.Black, new Rectangle(485 - 50 / 2, 220 - 50 / 2, 50, 50));

            graphic.FillEllipse(Brushes.White, new Rectangle(492 - 7, 234 - 7, 14, 14));
            graphic.FillEllipse(Brushes.White, new Rectangle(372 - 7, 212 - 7, 14, 14));

            graphic.FillEllipse(Brushes.White, new Rectangle(382 - 4, 194 - 4, 8, 8));
            graphic.FillEllipse(Brushes.White, new Rectangle(500 - 4, 210 - 4, 8, 8));

            graphic.FillEllipse(Brushes.White, new Rectangle(356 - 3, 190 - 3, 6, 6));
            graphic.FillEllipse(Brushes.White, new Rectangle(472 - 3, 208 - 3, 6, 6));

            return graphic;
        }

        public static Graphics DrawCatNouse(this Graphics graphic)
        {
            graphic.DrawCurve(new Pen(Brushes.Black, 4), CatModel.MouthBorder);
            graphic.FillPolygon(Brushes.Pink, CatModel.NouseBorder);
            return graphic;
        }

        public static Graphics DrawCatEars(this Graphics graphic)
        {
            graphic.FillPolygon(new SolidBrush(Color.FromArgb(206, 206, 206)), CatModel.LeftEarsBackground);
            graphic.DrawCurve(new Pen(Brushes.Black, 4), CatModel.LeftEarsBorder);

            graphic.FillPolygon(new SolidBrush(Color.FromArgb(206, 206, 206)), CatModel.RightEarsBackground);
            graphic.DrawCurve(new Pen(Brushes.Black, 4), CatModel.RightEarsBorder);
            return graphic;
        }

        public static Graphics DrawCatHair(this Graphics graphic)
        {
            using (var hair_brush = new SolidBrush(Color.FromArgb(153, 153, 153)))
            { foreach (var item in CatModel.BodyHairBackground) graphic.FillPolygon(hair_brush, item); }
            return graphic;
        }

        public static Graphics DrawCatMustache(this Graphics graphic)
        {
            using (var mustacher_brush = new SolidBrush(Color.FromArgb(160, 249, 189, 186)))
            {
                graphic.FillEllipse(mustacher_brush, new Rectangle(352 - 20, 240 - 20, 40, 40));
                graphic.FillEllipse(mustacher_brush, new Rectangle(496 - 20, 258 - 20, 40, 40));
            }
            using (var mustache_pen = new Pen(Brushes.Black, 4))
            {
                graphic.DrawLine(mustache_pen, new Point(512, 246), new Point(534, 238));
                graphic.DrawLine(mustache_pen, new Point(516, 260), new Point(536, 258));
                graphic.DrawLine(mustache_pen, new Point(515, 274), new Point(515, 274));

                graphic.DrawLine(mustache_pen, new Point(328, 220), new Point(306, 210));
                graphic.DrawLine(mustache_pen, new Point(320, 234), new Point(300, 228));
                graphic.DrawLine(mustache_pen, new Point(320, 248), new Point(298, 245));
                return graphic;
            }
        }

        
    }
}
