using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GrafikaDLL
{
    public class Vector2
    {
        public double x, y;

        public Vector2()
        {

        }
        public Vector2(double x, double y)
        {
            this.x = x;
            this.y = y;
        }

        public static Vector2 operator +(Vector2 v1, Vector2 v2)
        { return new Vector2(v1.x + v2.x, v1.y + v2.y); }
        public static Vector2 operator -(Vector2 v1, Vector2 v2)
        { return new Vector2(v1.x - v2.x, v1.y - v2.y); }
    }
}
