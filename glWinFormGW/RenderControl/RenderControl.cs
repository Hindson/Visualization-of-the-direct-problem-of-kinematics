using glWinFormGW.Classes;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Design;
using System.IO;
using System.Linq;
using System.Reflection.Metadata;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using static System.Net.WebRequestMethods;

namespace glWinFormGW
{
    public partial class RenderControl : OpenGL
    {
        public RenderControl()
        {
            InitializeComponent();
            MouseWheel += OnWheel;
        }

        double a = 0.3, b1 = 0.36, S = 0.65, r = 0.02;
        double cosC => (a * a + b1 * b1 - S * S) / (2 * a * b1);
        double angleC => Math.Acos(cosC) * (180 / Math.PI); 
        double cosO => (a * a + S * S - b1 * b1) / (2 * a * S);
        double angleO => Math.Acos(cosO) * (180 / Math.PI);
        double AspectRatio => (double)Width / Height;
        double b => b1 + 2 * b1;
        double rx = +20, ry = -30, m = 1;
        double my = 45, mx = -70;

        float[] matAmbient = { 0.5f, 0.5f, 0.5f, 1.0f };
        float[] matDiffuse = { 0.5f, 0.5f, 0.5f, 0.5f };
        float[] matSpecular = { 1.0f, 1.0f, 1.0f, 1.0f };
        float[] matShininess = { 50.0f };

        private void OnRender(object sender, EventArgs e)
        {
            glClear(GL_COLOR_BUFFER_BIT | GL_DEPTH_BUFFER_BIT | GL_STENCIL_BUFFER_BIT);
            glViewport(0, 0, Width, Height);
            glMatrixMode(GL_PROJECTION);
            glLoadIdentity();
            gluPerspective(50, AspectRatio, 0.1, 10);
            glTranslated(0, -0.5, -3);

            glMatrixMode(GL_MODELVIEW);
            glLoadIdentity();
            glRotated(rx, 1, 0, 0);
            glRotated(ry, 0, 1, 0);
            glColor(Color.Yellow);
            glCallList(idAxis);
            glCallList(idGrid);
            DrawText("+X", 1.5 + 0.15, 0, 0);
            DrawText("+Y", 0, 1.5 + 0.15, 0);
            DrawText("+Z", 0, 0, 1.5 + 0.15);

            glEnable(GL_NORMALIZE);
            light.Enable();
            glEnable(GL_LIGHTING);
            glEnable(GL_LIGHT0);
            glEnable(GL_COLOR_MATERIAL);

            glEnable(GL_BLEND);
            glBlendFunc(GL_ONE, GL_DST_COLOR);

            glColorMaterial(GL_FRONT_AND_BACK, GL_AMBIENT_AND_DIFFUSE);
            glMaterialfv(GL_FRONT_AND_BACK, GL_AMBIENT, matAmbient);
            glMaterialfv(GL_FRONT_AND_BACK, GL_DIFFUSE, matDiffuse);
            glMaterialfv(GL_FRONT_AND_BACK, GL_SPECULAR, matSpecular);
            glMaterialfv(GL_FRONT_AND_BACK, GL_SHININESS, matShininess);

            glLightModelf(GL_LIGHT_MODEL_TWO_SIDE, GL_TRUE);

            glScaled(m, m, m);
            Segment();

            glDisable(GL_BLEND);
            glDisable(GL_COLOR_MATERIAL);
            glDisable(GL_LIGHT0);
            glDisable(GL_LIGHTING);
            glDisable(GL_NORMALIZE);
        }

        private void Segment()
        {
            glRotated(my, 0, 1, 0);
            glRotated(mx, 1, 0, 0);
            stick.Draw(q, r, r, a, Color.DarkBlue);

            glRotated(angleO, 1, 0, 0);
            stick.Draw(q, r, r, S, Color.Red);

            glRotated(-angleO, 1, 0, 0);
            glTranslatef(0, 0, (float)a);
            glRotated(-angleC + 180, 1, 0, 0);
            stick.Draw(q, r, r, b, Color.Green);
        }

        bool fDown = false;
        double x0, y0;
        private void OnDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                fDown = true;
                x0 = e.X;
                y0 = e.Y;
            }
        }

        private void OnUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left && fDown)
                fDown = false;
        }

        private void OnMove(object sender, MouseEventArgs e)
        {
            if (fDown)
            {
                ry -= (x0 - e.X) / 2.0;
                rx -= (y0 - e.Y) / 2.0;
                x0 = e.X;
                y0 = e.Y;
                Invalidate();
            }
        }

        private void OnWheel(object sender, MouseEventArgs e)
        {
            m += e.Delta / 2000.0;
            Invalidate();
        }

        uint idAxis, idGrid, idWall;
        IntPtr q;
        Stick stick;
        Light light;
        private void OnContextCreated(object sender, EventArgs e)
        {
            stick = new Stick();
            light = new Light();

            idAxis = glGenLists(1);
            Axis axis = new Axis();
            axis.Draw(idAxis, 1.5);

            idGrid = glGenLists(1);
            Grid grid = new Grid();
            grid.Draw(idGrid, 1.5);

            idWall = glGenLists(1);
            Wall wall = new Wall();
            wall.Draw(idWall, 1.5);

            q = gluNewQuadric();
        }

        private void OnContextDeleting(object sender, EventArgs e)
        {
            glDeleteLists(idAxis, 1);
            glDeleteLists(idGrid, 1);
            glDeleteLists(idWall, 1);

            gluDeleteQuadric(q);
        }

        private void Key_Down(object sender, PreviewKeyDownEventArgs e)
        {
            if (e.KeyCode == Keys.W)
                if (mx - 1 > -180)
                    mx -= 1;
            if (e.KeyCode == Keys.S)
                if (mx + 1 < -20)
                    mx += 1;

            if (e.KeyCode == Keys.A)
                if (S <= a + b1 - 0.01)
                    S += 0.01;

            if (e.KeyCode == Keys.D)
                if (S >= Math.Abs(b1 - a) + 0.01)
                    S -= 0.01;

            if (e.KeyCode == Keys.E)
                if(my > 0.1)
                    my -= 1;

            if (e.KeyCode == Keys.Q)
                if(my < 90)
                    my += 1;
            Invalidate();
        }
    }
}

