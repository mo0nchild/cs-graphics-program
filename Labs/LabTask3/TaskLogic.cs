using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSGraphicsProgram.Labs.LabTask3
{
    public sealed class TaskLogic : System.Object
    {
        public delegate void DrawBorderHandler(Point begin_point, Point end_point);
        public DrawBorderHandler DrawHandler { get; private set; } = delegate { return; };

        public TaskLogic(DrawBorderHandler draw_handler) : base()  { this.DrawHandler = draw_handler; }

        /// <summary>Алгоритм формирования фрактального рисунка "Кривая Коха"</summary>
        /// 
        /// <param name="point_a">Начальная вершина формирования фрактала</param>
        /// <param name="point_e">Конечная вершина формирования фрактала</param>
        /// 
        /// <param name="iter">Количество рекурсивных вызовов</param>
        public void KochCurveAlgorithm(Point point_a, Point point_e, int iter)
        {
            if(iter == 0) { this.DrawHandler(point_a, point_e); return; }
            Point point_b = calculateVertex(1.0 / 3), point_d = calculateVertex(2.0 / 3);
            
            var x_c = point_b.X + (point_d.X - point_b.X) * Math.Cos(60 * Math.PI / 180) + Math.Sin(
                60 * Math.PI / 180) * (point_d.Y - point_b.Y);
            var y_c = point_b.Y - (point_d.X - point_b.X) * Math.Sin(60 * Math.PI / 180) + Math.Cos(
                60 * Math.PI / 180) * (point_d.Y - point_b.Y);

            this.KochCurveAlgorithm(point_a, point_b, iter - 1);
            this.KochCurveAlgorithm(point_b, new Point((int)x_c, (int)y_c), iter - 1);

            this.KochCurveAlgorithm(new Point((int)x_c, (int)y_c), point_d, iter - 1);
            this.KochCurveAlgorithm(point_d, point_e, iter - 1);

            Point calculateVertex(double segment) => new Point((int)(point_a.X + (point_e.X - point_a.X) 
                * segment), (int)(point_a.Y + (point_e.Y - point_a.Y) * segment));
        }
    }
}
