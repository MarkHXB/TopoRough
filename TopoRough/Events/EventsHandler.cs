using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TopoRough.Events
{
    public class EventsHandler
    {
        internal static void Shape_MouseUp(object sender, MouseEventArgs e)
        {
            PictureBox root = (PictureBox)sender;

            root.MouseDown += shapeMoving_MouseDown;
            root.MouseUp += shapeMoving_MouseUp;
            root.MouseDown -= Shape_MouseUp;

            foreach (var item in Form.ActiveForm.Controls)
            {
                Control ctl = (Control)item;
                if (ctl.GetType().ToString().Contains("Panel"))
                {
                    Panel alPanel = (Panel)ctl;
                    if(alPanel.Name == "sandboxPanel")
                        alPanel.Controls.Add(root);
                }
            }
            
        }

        private static void shapeMoving_MouseUp(object sender, MouseEventArgs e)
        {
            PictureBox root = (PictureBox)sender;
            root.Location = new Point(e.X, e.Y);
        }

        private static void shapeMoving_MouseDown(object sender, MouseEventArgs e)
        {
            PictureBox root = (PictureBox)sender;
            root.Location = new Point(e.X, e.Y);
        }

        internal static void Shape_MouseDown(object sender, MouseEventArgs e)
        {
            PictureBox root = (PictureBox)sender;
            root.Location = new Point(e.X, e.Y);
        }
    }
}
