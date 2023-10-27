using GrafikaDLL;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GrafikaAlap
{
    public partial class Form1 : Form
    {
        Graphics g;
        PointF[] polygon;
        PointF[] polygon2;

        public Form1()
        {
            InitializeComponent();
            polygon = new PointF[9];
            polygon2 = new PointF[4];
        }

        private void canvas_Paint(object sender, PaintEventArgs e)
        {
            g = e.Graphics;
            Random rnd = new Random();
            polygon[0] = new PointF(100, 100);
            polygon[1] = new PointF(200, 200);
            polygon[2] = new PointF(300, 100);
            polygon[3] = new PointF(400, 300);
            polygon[4] = new PointF(250, 200);
            polygon[5] = new PointF(500, 400);
            polygon[6] = new PointF(200, 350);
            polygon[7] = new PointF(100, 400);
            polygon[8] = new PointF(100, 100);

            
            polygon2[0] = new PointF(700, 80);
            polygon2[1] = new PointF(200, 50);
            polygon2[2] = new PointF(90, 90);
            polygon2[3] = new PointF(700, 80);
            
            /*
            polygon2[0] = new PointF(22, 50);
            polygon2[1] = new PointF(500, 0);
            polygon2[2] = new PointF(260, 80);
            polygon2[3] = new PointF(300, 100);
            polygon2[4] = new PointF(400, 150);
            polygon2[5] = new PointF(100, 200);
            polygon2[6] = new PointF(22, 50);
            */

            for (int i = 0; i < polygon.Length - 1; i++)
            {
                g.DrawLine(Pens.Blue, polygon[i], polygon[i + 1]);
            }
            for (int i = 0; i < polygon2.Length - 1; i++)
            {
                g.DrawLine(Pens.Blue, polygon2[i], polygon2[i + 1]);
            }
            for (int i = 0; i < 20; i++)
            {
                PointF P0 = new PointF(rnd.Next(0, canvas.Width), rnd.Next(0, canvas.Height));
                PointF P1 = new PointF(rnd.Next(0, canvas.Width), rnd.Next(0, canvas.Height));
                g.DrawLine(Pens.Black, P0, P1);
                g.Clip(new Pen(Color.Red), polygon, P0, P1);
                g.Clip(new Pen(Color.Red), polygon2, P0, P1);
            }
        }

        private void canvas_MouseDown(object sender, MouseEventArgs e)
        {

        }
        private void canvas_MouseMove(object sender, MouseEventArgs e)
        {

        }
        private void canvas_MouseUp(object sender, MouseEventArgs e)
        {

        }
    }
}
