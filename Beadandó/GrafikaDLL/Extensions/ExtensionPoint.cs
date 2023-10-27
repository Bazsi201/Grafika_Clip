using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GrafikaDLL
{
    public static class ExtensionPoint
    {
        public static int GRAB_DISTANCE = 5;

        public static bool GrabbedBy(this Point p, int mouseX, int mouseY)
        {
            return Math.Abs(p.X - mouseX) <= GRAB_DISTANCE &&
                   Math.Abs(p.Y - mouseY) <= GRAB_DISTANCE;
        }
        public static double Distance(this Point p, Point p0)
        {
            return Math.Sqrt((p.X - p0.X) * (p.X - p0.X) +
                             (p.Y - p0.Y) * (p.Y - p0.Y));
        }
    }
}
