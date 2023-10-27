using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GrafikaDLL
{
    public static class ExtensionGraphics
    {
        public class Line
        {
            public float m;
            public float b; 
            public float x;
            public PointF P0;
            public PointF P1;

            public Line(PointF P0, PointF P1)
            {
                this.P0 = P0;
                this.P1 = P1;
                if (P0.X != P1.X)
                {
                    m = (P1.Y - P0.Y) / (P1.X - P0.X);
                    b = P0.Y - m * P0.X;
                    x = -1;
                }
                else
                {
                    m = 0;
                    b = 0;
                    x = P0.X;
                }

            }

            public PointF Instersection(Line line)
            {
                float x, y;
                if (this.x == -1 && line.x == -1)
                {
                    if (this.m == line.m)
                    {
                        return new PointF(-1000, -1000);
                    }
                    x = (this.b - line.b) / (line.m - this.m);
                    y = this.m * x + this.b;
                }
                else
                {
                    if (this.x != -1 && line.x == -1)
                    {
                        x = this.x;
                        y = line.m * x + line.b;
                    }
                    else if (this.x == -1 && line.x != -1)
                    {
                        x = line.x;
                        y = this.m * x + this.b;
                    }
                    else
                    {
                        return new PointF(-1000, -1000);
                    }
                }

                return new PointF(x, y);
            }
            public float ValueIn(PointF P)
            {
                if (this.x == -1)
                {
                    return this.m * P.X + this.b - P.Y;
                }
                else
                {
                    return P.X - this.x;
                }
            }

        }

        #region Pixel
            public static void Pixel(this Graphics g, Pen pen, int x, int y)
        {
            g.DrawRectangle(pen, x, y, 0.5f, 0.5f);
        }
        public static void Pixel(this Graphics g, Color c, int x, int y)
        {
            g.DrawRectangle(new Pen(c), x, y, 0.5f, 0.5f);
        }
        #endregion

        #region DrawLine
        public static void DrawLineDDA(this Graphics g,
            Pen pen, int x1, int y1, int x2, int y2)
        {
            int dx = x2 - x1;
            int dy = y2 - y1;
            int length = Math.Abs(dx);
            if (length < Math.Abs(dy))
                length = Math.Abs(dy);
            double incX = (double)dx / length;
            double incY = (double)dy / length;
            double x = x1, y = y1;
            g.Pixel(pen, (int)x, (int)y);
            for (int i = 0; i < length; i++)
            {
                x += incX;
                y += incY;
                g.Pixel(pen, (int)x, (int)y);
            }
        }
        public static void DrawLineDDA(this Graphics g,
            Color c1, Color c2, int x1, int y1, int x2, int y2)
        {
            int dx = x2 - x1;
            int dy = y2 - y1;
            int dR = c2.R - c1.R;
            int dG = c2.G - c1.G;
            int dB = c2.B - c1.B;
            int length = Math.Abs(dx);
            if (length < Math.Abs(dy))
                length = Math.Abs(dy);
            double incX = (double)dx / length;
            double incY = (double)dy / length;
            double incR = (double)dR / length;
            double incG = (double)dG / length;
            double incB = (double)dB / length;
            double x = x1, y = y1;
            double R = c1.R, G = c1.G, B = c1.B;
            g.Pixel(Color.FromArgb((int)R, (int)G, (int)B), (int)x, (int)y);
            for (int i = 0; i < length; i++)
            {
                x += incX; y += incY;
                R += incR; G += incG; B += incB;
                g.Pixel(Color.FromArgb((int)R, (int)G, (int)B), (int)x, (int)y);
            }
        }
        public static void DrawLineDDA(this Graphics g,
            Color[] colors, double[] ratios, PointF p0, PointF p1)
        {
            throw new ProjectNotImplementedException();
        }
        public static void DrawLineMidPoint2(this Graphics g,
            Pen pen, int x1, int y1, int x2, int y2)
        {
            int dx = x2 - x1;
            int dy = y2 - y1;
            int d = 2 * dy - dx;
            int x = x1, y = y1;
            for (int i = 0; i < dx; i++)
            {
                g.Pixel(pen, x, y);
                if (d > 0)
                {
                    y++;
                    d += dy - dx;
                }
                else
                {
                    d += dy;
                }
                x++;
            }
        }
        public static void DrawLineMidPoint2(this Graphics g,
            Color c1, Color c2, int x1, int y1, int x2, int y2)
        {
            throw new HomeworkNotImplementedException();
        }
        public static void DrawLineMidPoint(this Graphics g,
            Pen pen, int x1, int y1, int x2, int y2)
        {
            throw new HomeworkNotImplementedException();
        }
        public static void DrawLineMidPoint(this Graphics g,
            Color c1, Color c2, int x1, int y1, int x2, int y2)
        {
            throw new HomeworkNotImplementedException();
        }
        public static void DrawLineWu(this Graphics g,
            Color c, PointF p0, PointF p1)
        {
            throw new ProjectNotImplementedException();
        }
        #endregion

        #region DrawCircle
        private static void CirclePoints(this Graphics g, Pen pen, int x, int y)
        {
            g.Pixel(pen, x, y);
            g.Pixel(pen, x, -y);
            g.Pixel(pen, -x, y);
            g.Pixel(pen, -x, -y);
            g.Pixel(pen, y, x);
            g.Pixel(pen, y, -x);
            g.Pixel(pen, -y, x);
            g.Pixel(pen, -y, -x);
        }
        public static void DrawCircle(this Graphics g, Pen pen, int r)
        {
            int x = 0;
            int y = r;
            int h = 1 - r;
            g.CirclePoints(pen, x, y);
            while (x < y)
            {
                if (h < 0)
                {
                    h += 2 * x + 3;
                }
                else
                {
                    h += 2 * (x - y) + 5;
                    y--;
                }
                x++;
                g.CirclePoints(pen, x, y);
            }
        }
        public static void DrawCircle(this Graphics g, Pen pen, Point O, int r)
        {
            throw new HomeworkNotImplementedException();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="g"></param>
        /// <param name="colors">Contains exactly 8 colors!</param>
        /// <param name="O"></param>
        /// <param name="r"></param>
        /// <exception cref="ProjectNotImplementedException"></exception>
        public static void DrawCircle(this Graphics g, Color[] colors,
            Point O, int r)
        {
            throw new ProjectNotImplementedException();
        }
        public static void DrawCircle(this Graphics g, Color c0, Color c1,
            Point O, int r)
        {
            throw new ProjectNotImplementedException();
        }
        #endregion

        #region DrawCurves
        public delegate double RtoR(double x);
        public static void DrawParametricCurve(this Graphics g,
            Pen pen, RtoR x, RtoR y, double a, double b, int n = 500)
        {
            double t = a;
            double h = (b - a) / n;
            Vector2 v0 = new Vector2(x(t), y(t));
            Vector2 v1;
            while (t < b)
            {
                t += h;
                v1 = new Vector2(x(t), y(t));
                g.DrawLine(pen, (float)v0.x, (float)v0.y, (float)v1.x, (float)v1.y);
                v0 = v1;
            }
        }
        //public static void DrawHermiteArc(this Graphics g, Pen pen,
        //    HermiteArc arc)
        //{
        //    g.DrawParametricCurve();
        //}
        public static void DrawHermiteArc(this Graphics g, Pen pen,
            Vector2 p0, Vector2 p1, Vector2 q0, Vector2 q1)
        {
            g.DrawParametricCurve(pen,
                t => Hermite.H0(t) * p0.x + Hermite.H1(t) * p1.x + Hermite.H2(t) * q0.x + Hermite.H3(t) * q1.x,
                t => Hermite.H0(t) * p0.y + Hermite.H1(t) * p1.y + Hermite.H2(t) * q0.y + Hermite.H3(t) * q1.y,
                0.0, 1.0);
        }
        #endregion

        #region
        private const byte INSIDE = 0;  //0000
        private const byte LEFT = 1;    //0001
        private const byte RIGHT = 2;   //0010
        private const byte TOP = 4;     //0100
        private const byte BOTTOM = 8;  //1000

        // 0101
        // 0100
        //-----
        // 0101

        private static byte OutCode(Rectangle window, PointF p)
        {
            byte code = INSIDE;

            if (p.X < window.Left) code |= LEFT;
            else if (window.Right < p.X) code |= RIGHT;

            if (p.Y < window.Top) code |= TOP;
            else if (p.Y > window.Bottom) code |= BOTTOM;

            return code;
        }
        public static void ClipCohenSutherland(this Graphics g, Pen pen,
            Rectangle window, PointF p0, PointF p1)
        {
            byte code0 = OutCode(window, p0);
            byte code1 = OutCode(window, p1);
            bool accept = false;
            while (true)
            {
                if ((code0 | code1) == INSIDE)
                {
                    accept = true;
                    break;
                }
                else if ((code0 & code1) != INSIDE)
                {
                    break;
                }
                else
                {
                    byte code = code0 != INSIDE ? code0 : code1;

                    float x = 0, y = 0;
                    if ((code & LEFT) != 0)
                    {
                        x = window.Left;
                        y = p0.Y + (p1.Y - p0.Y) * (window.Left - p0.X) / (p1.X - p0.X);
                    }
                    else if ((code & RIGHT) != 0)
                    {
                        x = window.Right;
                        y = p0.Y + (p1.Y - p0.Y) * (window.Right - p0.X) / (p1.X - p0.X);
                    }
                    else if ((code & TOP) != 0)
                    {
                        x = p0.X + (p1.X - p0.X) * (window.Top - p0.Y) / (p1.Y - p0.Y);
                        y = window.Top;
                    }
                    else if ((code & BOTTOM) != 0)
                    {
                        x = p0.X + (p1.X - p0.X) * (window.Bottom - p0.Y) / (p1.Y - p0.Y);
                        y = window.Bottom;
                    }
                    else break;

                    if (code == code0)
                    {
                        p0.X = x;
                        p0.Y = y;
                        code0 = OutCode(window, p0);
                    }
                    else
                    {
                        p1.X = x;
                        p1.Y = y;
                        code1 = OutCode(window, p1);
                    }
                }
            }
            if (accept)
                g.DrawLine(pen, p0, p1);
        }


        public static void Clip(this Graphics g, Pen pen, PointF[] polygon, PointF p0, PointF p1)
        {
            Line line = new Line(p0, p1);
            List<int> signs = new List<int>();
            List<PointF> points = new List<PointF>();
            List<PointF> sortedPoints = new List<PointF>();
            int counter = 0;

            foreach (PointF p in polygon)
            {
                float value = line.ValueIn(p);
                if (value > 0)
                {
                    signs.Add(1);
                    continue;
                }
                if (value < 0)
                {
                    signs.Add(-1);
                    continue;
                }
                signs.Add(0);
            }

            for (int i = 0; i < signs.Count - 1; i++)
            {
                if (signs[i] != signs[i + 1])
                {
                    Line newLine = new Line(polygon[i], polygon[i + 1]);
                    PointF p = line.Instersection(newLine);
                    points.Add(p);
                }
            }

            points.Add(p0);
            points.Add(p1);

            sortedPoints.Add(points[0]);

            if (line.x == -1)
            {
                for (int i = 1; i < points.Count; i++)
                {
                    int index = 0;
                    while (index != sortedPoints.Count && points[i].X >= sortedPoints[index].X)
                    {
                        index++;
                    }
                    if (index != sortedPoints.Count)
                    {
                        sortedPoints.Insert(index, points[i]);
                    }
                    else
                    {
                        sortedPoints.Add(points[i]);
                    }
                }
            }
            else
            {
                for (int i = 1; i < points.Count; i++)
                {
                    int index = 0;
                    while (index != sortedPoints.Count && points[i].Y >= sortedPoints[index].Y)
                    {
                        index++;
                    }
                    if (index != sortedPoints.Count)
                    {
                        sortedPoints.Insert(index, points[i]);
                    }
                    else
                    {
                        sortedPoints.Add(points[i]);
                    }
                }
            }

            PointF P = sortedPoints[0];
            while (P != p0 && P != p1)
            {
                sortedPoints.RemoveAt(0);
                P = sortedPoints[0];
                counter++;
            }
            P = sortedPoints[sortedPoints.Count - 1];
            while (P != p0 && P != p1)
            {
                sortedPoints.RemoveAt(sortedPoints.Count - 1);
                P = sortedPoints[sortedPoints.Count - 1];
            }

            if (counter % 2 == 0)
            {
                for (int i = 1; i < sortedPoints.Count - 1; i = i + 2)
                {
                    g.DrawLine(pen /*new Pen(Color.Crimson)*/, sortedPoints[i], sortedPoints[i + 1]);
                }
                
            }
            else
            {
                for (int i = 0; i < sortedPoints.Count - 1; i = i + 2)
                {
                    g.DrawLine(pen /*new Pen(Color.Crimson)*/, sortedPoints[i], sortedPoints[i + 1]);
                }
            }

        }
        public static void Clip(this Graphics g, Pen pen,
            PointF[][] polygon, PointF p0, PointF p1)
        {
            //Tetszőleges, konkáv poligon szigetekkel
            throw new ProjectNotImplementedException();
        }
        #endregion
    }
}
