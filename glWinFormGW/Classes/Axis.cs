using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static glWinFormGW.OpenGL;

namespace glWinFormGW.Classes
{
    internal class Axis
    {
        public void Draw(uint id, double sz)
        {
            glNewList(id, GL_COMPILE);
            glLineWidth(2);
            double a = sz / 10;
            glBegin(GL_LINES);
            glVertex3d(-a, 0, 0); glVertex3d(sz, 0, 0);
            glVertex3d(0, -a, 0); glVertex3d(0, sz, 0);
            glVertex3d(0, 0, -a); glVertex3d(0, 0, sz);
            glEnd();
            glLineWidth(1);
            glEndList();
        }
    }
}
