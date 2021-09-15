using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TopoRough.Data;
using TopoRough.Models;
using TopoRough.Screens.MainScreen;

namespace TopoRough.Events
{
    public class EventsHandler
    {
        private static Point PanelMouseDownLocation = new Point();
        internal static void Shape_MouseUp(object sender, MouseEventArgs e)
        {
            PictureBox root = (PictureBox)sender;

            root.MouseDown += shapeMoving_MouseDown;
            root.MouseMove += shapeMoving_MouseMove;
            root.MouseDown -= Shape_MouseUp;

            foreach (var item in TestShapes.Shapes)
            {
                if (item.Item.Name == root.Name)
                {
                    item.IsAlive = false;
                    
                }
            }

            foreach (var item in Form.ActiveForm.Controls)
            {
                Control ctl = (Control)item;
                if (ctl.GetType().ToString().Contains("Panel"))
                {
                    Panel alPanel = (Panel)ctl;
                    if(alPanel.Name == "sandboxPanel")
                    {
                        alPanel.Controls.Add(root);
                    }  
                }
            }

            
        }

        internal static void shapeMoving_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                PictureBox root = (PictureBox)sender;
                root.Left += e.X - PanelMouseDownLocation.X;

                root.Top += e.Y - PanelMouseDownLocation.Y;

            }
        }


        internal static void shapeMoving_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left) PanelMouseDownLocation = e.Location;
        }

        internal static void Save_Work(Panel sandboxPanel)
        {
            State.Save(sandboxPanel);
        }

        internal static void Load_Work(Panel sandboxPanel)
        {
            if (sandboxPanel.Controls.Count == 0)
            {
                GlobalVariables.LoadingIsValid = true;
            }
            if (GlobalVariables.LoadingIsValid == true)
            {
                try
                {
                    List<PictureBox> loadedShapes = State.Load();
                    sandboxPanel.Controls.AddRange(loadedShapes.ToArray());
                }
                catch (Exception x)
                {
                    MessageBox.Show(x.Message);
                }
                finally
                {
                    GlobalVariables.LoadingIsValid = false;
                }
            }
        }

        internal static void Save_User_Settings(object workItem)
        {
            try
            {
                var panel = Cast(workItem, typeof(Panel));
                Properties.Settings.Default.LastOpenedWorkName = GlobalVariables.RecentlyOpenedWorkName;
                Properties.Settings.Default.ScreenSize = new Point(panel.ClientSize.Width, panel.ClientSize.Height);
                Properties.Settings.Default.ScreenLocation = new Point(panel.Location.X, panel.Location.Y);

                Properties.Settings.Default.Save();
            }
            catch (Exception x)
            {
                MessageBox.Show(x.Message);
            }
            
        }
        public static dynamic Cast(dynamic obj, Type castTo)
        {
            return Convert.ChangeType(obj, castTo);
        }
    }
}
