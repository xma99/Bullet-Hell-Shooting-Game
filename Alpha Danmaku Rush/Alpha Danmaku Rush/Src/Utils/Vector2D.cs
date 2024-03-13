using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Alpha_Danmaku_Rush.Src.Utils
{
    public class Vector2D
    {
        public float X { get; set; }
        public float Y { get; set; }

        public Vector2D(float x = 0.0f, float y = 0.0f)
        {
            X = x;
            Y = y;
        }

        public void Add(Vector2D v)
        {
            X += v.X;
            Y += v.Y;
        }

        public void Subtract(Vector2D v)
        {
            X -= v.X;
            Y -= v.Y;
        }

        public void Multiply(float scalar)
        {
            X *= scalar;
            Y *= scalar;
        }

        public float Magnitude()
        {
            return (float)Math.Sqrt(X * X + Y * Y);
        }

        public void Normalize()
        {
            float magnitude = Magnitude();
            if (magnitude > 0)
            {
                X /= magnitude;
                Y /= magnitude;
            }
        }

        public static Vector2D operator +(Vector2D v1, Vector2D v2)
        {
            return new Vector2D(v1.X + v2.X, v1.Y + v2.Y);
        }

        public static Vector2D operator -(Vector2D v1, Vector2D v2)
        {
            return new Vector2D(v1.X - v2.X, v1.Y - v2.Y);
        }

        public static Vector2D operator *(Vector2D v, float scalar)
        {
            return new Vector2D(v.X * scalar, v.Y * scalar);
        }

        public static Vector2D operator *(float scalar, Vector2D v)
        {
            return new Vector2D(v.X * scalar, v.Y * scalar);
        }

        // Additional utility methods like distance, dot product etc. can be added here.

        public override string ToString()
        {
            return $"({X}, {Y})";
        }
    }

}
