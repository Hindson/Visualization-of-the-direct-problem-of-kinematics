using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static glWinFormGW.OpenGL;

namespace glWinFormGW.Classes
{
    internal class Grid
    {
        public void Draw(uint idGrid, double len)
        {
            glPushAttrib(GL_CURRENT_BIT | GL_LINE_BIT);
            glNewList(idGrid, GL_COMPILE);
            glColor(Color.LightGray);
            glLineStipple(0, 0xFF00);
            glEnable(GL_LINE_STIPPLE);
            glBegin(GL_LINES);
            for (double pointX1 = 0; pointX1 <= +len; pointX1 += 0.2)
            {
                glVertex3d(pointX1, 0, 0);
                glVertex3d(pointX1, 0, +len);
            }

            for (double pointZ = +len; pointZ >= 0; pointZ -= 0.2)
            {
                glVertex3d(0, 0, pointZ);
                glVertex3d(+len, 0, pointZ);
            }
            glEnd();

            glDisable(GL_LINE_STIPPLE);
            glEndList();
            glPopAttrib();
        }
    }
}
