using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GrafikaDLL
{
    public class HermiteArc
    {        
        public Vector2 p0, p1, q0, q1;

        public HermiteArc(Vector2 p0, Vector2 p1, Vector2 q0, Vector2 q1)
        {
            this.p0 = p0; this.p1 = p1;
            this.q0 = q0; this.q1 = q1;
        }
    }
}
