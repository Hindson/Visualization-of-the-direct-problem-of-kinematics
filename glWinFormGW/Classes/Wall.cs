using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static glWinFormGW.OpenGL;

namespace glWinFormGW.Classes
{
    internal class Wall
    {
        public void Draw(uint idWall, double sz)
        {
            glNewList(idWall, GL_COMPILE);
            double a = sz / 10 +0.01;
            glBegin(GL_QUADS);
            glVertex3d(-a, -a, -a);
            glVertex3d(sz, -a, -a);
            glVertex3d(sz, sz, -a);
            glVertex3d(-a, sz, -a);
            glEnd();
            glEndList();
        }
        public void Draw2(uint idWall, double sz)
        {
            glNewList(idWall, GL_COMPILE);
            double a = sz / 10 + 0.01;
            glBegin(GL_POLYGON);
            glVertex3d(-a, -a, -a); 
            glVertex3d(-a, -a, sz);glVertex3d(-a, sz, sz);glVertex3d(-a, sz, -a); 
            glEnd();
            glEndList();
        }
    }
}
