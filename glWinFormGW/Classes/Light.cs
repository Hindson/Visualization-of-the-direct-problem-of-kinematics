using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static glWinFormGW.OpenGL;

namespace glWinFormGW.Classes
{
    internal class Light
    {
        public void Enable()
        {
            float[] pos = new float[4] { 0.1f, 0.5f, 0.5f, 1 };
            glLightfv(GL_LIGHT0, GL_POSITION, pos);
            ShowLight(pos);
        }

        private void ShowLight(float[] pos)
        {
            glColor(Color.White);
            glPointSize(7);
            glBegin(GL_POINTS);
            glVertex3fv(pos);
            glEnd();
            glPointSize(1);

            glLineStipple(1, 0xCCCC);
            glEnable(GL_LINE_STIPPLE);
            glLineWidth(3);
            glBegin(GL_LINES);
            glVertex3fv(pos);
            glVertex3f(0, 0, 0);
            glEnd();
            glDisable(GL_LINE_STIPPLE);
            glLineWidth(1);
        }
    }
}
