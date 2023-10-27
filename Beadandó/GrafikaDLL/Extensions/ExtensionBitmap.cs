using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GrafikaDLL
{
    public enum SupersamplingType { Standard }

    public static class ExtensionBitmap
    {
        public static void SetLine(this Bitmap bmp, int x1, int y1,
            int x2, int y2, Color color)
        {
            int dx = x2 - x1;
            int dy = y2 - y1;
            int length = Math.Abs(dx);
            if (length < Math.Abs(dy))
                length = Math.Abs(dy);
            double incX = (double)dx / length;
            double incY = (double)dy / length;
            double x = x1, y = y1;
            bmp.SetPixel((int)x, (int)y, color);
            for (int i = 0; i < length; i++)
            {
                x += incX;
                y += incY;
                bmp.SetPixel((int)x, (int)y, color);
            }
        }

        public static Bitmap Supersampling(this Bitmap bmp, bool copyTopLeft = true)
        {
            Bitmap res = new Bitmap(bmp.Width, bmp.Height);

            if (copyTopLeft)
            {
                for (int y = 0; y < bmp.Height - 1; y++)
                {
                    for (int x = 0; x < bmp.Width - 1; x++)
                    {
                        Color c0 = bmp.GetPixel(x, y);
                        Color c1 = bmp.GetPixel(x + 1, y);
                        Color c2 = bmp.GetPixel(x, y + 1);
                        Color c3 = bmp.GetPixel(x + 1, y + 1);
                        int r = (int)((c0.R + c1.R + c2.R + c3.R) / 4.0);
                        int g = (int)((c0.G + c1.G + c2.G + c3.G) / 4.0);
                        int b = (int)((c0.B + c1.B + c2.B + c3.B) / 4.0);
                        Color newColor = Color.FromArgb(r, g, b);
                        res.SetPixel(x + 1, y + 1, newColor);
                    }
                }

                for (int x = 0; x < bmp.Width; x++)
                    res.SetPixel(x, 0, bmp.GetPixel(x, 0));
                for (int y = 0; y < bmp.Height; y++)
                    res.SetPixel(0, y, bmp.GetPixel(0, y));
            }
            else
            {
                for (int y = 0; y < bmp.Height - 1; y++)
                {
                    for (int x = 0; x < bmp.Width - 1; x++)
                    {
                        Color c0 = bmp.GetPixel(x, y);
                        Color c1 = bmp.GetPixel(x + 1, y);
                        Color c2 = bmp.GetPixel(x, y + 1);
                        Color c3 = bmp.GetPixel(x + 1, y + 1);
                        int r = (int)((c0.R + c1.R + c2.R + c3.R) / 4.0);
                        int g = (int)((c0.G + c1.G + c2.G + c3.G) / 4.0);
                        int b = (int)((c0.B + c1.B + c2.B + c3.B) / 4.0);
                        Color newColor = Color.FromArgb(r, g, b);
                        res.SetPixel(x, y, newColor);
                    }
                }

                for (int x = 0; x < bmp.Width; x++)
                    res.SetPixel(x, bmp.Height - 1, bmp.GetPixel(x, bmp.Height - 1));
                for (int y = 0; y < bmp.Height; y++)
                    res.SetPixel(bmp.Width - 1, y, bmp.GetPixel(bmp.Width - 1, y));
            }

            return res;
        }
        public static Bitmap Supersampling(this Bitmap bmp, int squareSise, bool copyTopLeft = true)
        {
            throw new ProjectNotImplementedException();
        }
        public static Bitmap SupersamplingWithCircleRadius(this Bitmap bmp, int r)
        {
            throw new ProjectNotImplementedException();
        }

        public static void FillEdgeFlag(this Bitmap bmp, Color background, Color fillColor)
        {
            for (int y = 0; y < bmp.Height; y++)
            {
                bool inside = false;
                for (int x = 0; x < bmp.Width; x++)
                {
                    Color color = bmp.GetPixel(x, y);
                    if (color.R == background.R &&
                        color.G == background.G &&
                        color.B == background.B)
                    {
                        inside = !inside;
                        continue;
                    }

                    //Páros mennyiségű szomszédos határpontok
                    throw new HomeworkNotImplementedException();

                    if (inside)
                        bmp.SetPixel(x, y, fillColor);
                }
            }
        }
        public static void FillRec4(this Bitmap bmp, int x, int y, Color background, Color fillColor)
        {
            Color color = bmp.GetPixel(x, y);
            if (color.R == background.R &&
                color.G == background.G &&
                color.B == background.B)
            {
                bmp.SetPixel(x, y, fillColor);

                bmp.FillRec4(x + 1, y, background, fillColor);
                bmp.FillRec4(x - 1, y, background, fillColor);
                bmp.FillRec4(x, y + 1, background, fillColor);
                bmp.FillRec4(x, y - 1, background, fillColor);
            }
        }
        public static void FillRec8(this Bitmap bmp, int x, int y, Color background, Color fillColor)
        {
            Color color = bmp.GetPixel(x, y);
            if (color.R == background.R &&
                color.G == background.G &&
                color.B == background.B)
            {
                bmp.SetPixel(x, y, fillColor);

                bmp.FillRec4(x + 1, y, background, fillColor);
                bmp.FillRec4(x - 1, y, background, fillColor);
                bmp.FillRec4(x, y + 1, background, fillColor);
                bmp.FillRec4(x, y - 1, background, fillColor);
                //Kezelni kell a határokat
                throw new HomeworkNotImplementedException();
                bmp.FillRec4(x + 1, y + 1, background, fillColor);
                bmp.FillRec4(x - 1, y + 1, background, fillColor);
                bmp.FillRec4(x - 1, y + 1, background, fillColor);
                bmp.FillRec4(x + 1, y - 1, background, fillColor);
            }
        }
        public static void FillStack4(this Bitmap bmp, int x, int y, Color background, Color fillColor)
        {
            int[] dx = { 0, 1, 0, -1 };
            int[] dy = { 1, 0, -1, 0 };
            Stack<Point> stack = new Stack<Point>();
            stack.Push(new Point(x, y));
            while(stack.Count != 0)
            {
                Point p = stack.Pop();
                bmp.SetPixel(p.X, p.Y, fillColor);

                for (int i = 0; i < dx.Length; i++)
                {
                    Point n = new Point(p.X + dx[i], p.Y + dy[i]);
                    Color color = bmp.GetPixel(n.X, n.Y);
                    if (color.R == background.R &&
                        color.G == background.G &&
                        color.B == background.B)
                    {
                        stack.Push(n);
                    }
                }
            }
        }
        public static void FillStack8(this Bitmap bmp, int x, int y, Color background, Color fillColor)
        {
            throw new HomeworkNotImplementedException();
        }
        public static void FillStack8(this Bitmap bmp, int x, int y, Color background, Color fillColor1, Color fillColor2)
        {
            throw new ProjectNotImplementedException();
        }
        public static void FillStack8(this Bitmap bmp, int x, int y, Color background, 
            Color fillColorCenter, Color fillColorTopLeft, Color fillColorTopRight, 
            Color fillColorBottomLeft, Color fillColorBottomRight)
        {
            throw new ProjectNotImplementedException();
        }
    }
}
