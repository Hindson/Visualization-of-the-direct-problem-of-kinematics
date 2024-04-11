using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static glWinFormGW.OpenGL;

namespace glWinFormGW
{
    internal class Stick
    {
        public void Draw(IntPtr q, double br, double tr, double h, Color color)
        {
            glColor(color);
            gluCylinder(q, br, tr, h, 30, 30);
        }
    }
}
