using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EpicenterFinderSimulation
{

    class Line
    {
        public double X0 { get; set; }
        public double Y0 { get; set; }
        public double M { get; set; }
        public double Y(double x) => M * (x - X0);
        public static Vector2D FindIntersection(Line a, Line b)
        {
            Vector2D pos = new Vector2D((a.M - b.M) / (a.M * a.X0 - b.M * b.X0), 0);
            pos.Y = a.Y(pos.X);
            return pos;
        }
    }
}
