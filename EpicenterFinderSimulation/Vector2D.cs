using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EpicenterFinderSimulation
{
    class Vector2D
    {
        public double X { get; set; }
        public double Y { get; set; }
        public Vector2D(double x, double y)
        {
            X = x;
            Y = y;
        }
        public static Vector2D FromPolar(double length, double angle) => new Vector2D(Math.Cos(angle), Math.Sin(angle)) * length;
        public double Length => Math.Sqrt(LengthSquared);
        public double LengthSquared => X * X + Y * Y;
        public double Angle => Math.Atan2(Y, X);
        public static Vector2D operator *(Vector2D a, double b) => new Vector2D(a.X * b, a.Y * b);
        public static Vector2D operator *(double a, Vector2D b) => b * a;
        public static Vector2D operator /(Vector2D a, double b) => new Vector2D(a.X / b, a.Y / b);
        public static Vector2D operator +(Vector2D a, Vector2D b) => new Vector2D(a.X + b.X, a.Y + b.X);
        public static Vector2D operator -(Vector2D a, Vector2D b) => new Vector2D(a.X - b.X, a.Y - b.X);
        public static double Dot(Vector2D a, Vector2D b) => a.X * b.X + a.Y * b.Y;
        public static double Cross(Vector2D a, Vector2D b) => a.X * b.Y - a.Y * b.X;
    }
}
