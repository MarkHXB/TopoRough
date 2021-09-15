using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Reflection;
using System.Threading;

using TopoRough.Screens;
using TopoRough.Screens.MainScreen;
using TopoRough.Constants;
using TopoRough.Models;

using FormSerialisation;
using TopoRough.Events;

namespace TopoRough
{
    public partial class MainScreen : Form
    {
        //mentés
        State stateHandler = new State();

        private bool isLinking = false;

        //Shape created
        private float zoom = 1f;

        private bool isEditing = true;
        private bool lastOpenShapes = false;

        private bool AskedExitSave = false;


        //COMPONENTS & INTERFECES
        private Drawing draw = new Drawing();
        private FormHandler formHandler = new FormHandler();

        public MainScreen()
        {
            InitializeComponent();
            EditStatus();
            LoadShapeList.InitList(shapeItemsPanel);
            sandboxPanel.AllowDrop = true;
            EventsHandler.Load_Work(sandboxPanel);
        }

        private void Show_On_MainMenu()
        {
            mainMenuPanel.Visible = true;
            savePanel.Visible = true;
            uploadPanel.Visible = true;
            settingsPanel.Visible = true;
            exitPanel.Visible = true;
            shapePanel.Visible = true;
            if (lastOpenShapes)
                shapeItemsPanel.Visible = true;
        }
        private void Show_Off_MainMenu()
        {
            mainMenuPanel.Visible = false;
            savePanel.Visible = false;
            uploadPanel.Visible = false;
            settingsPanel.Visible = false;
            exitPanel.Visible = false;
            shapePanel.Visible = false;
            shapeItemsPanel.Visible = false;
        }

        private void mainMenuBarStatusBtn_Click(object sender, EventArgs e)
        {
            if (mainMenuPanel.Visible == false)
            {
                Show_On_MainMenu();
            }
            else
                Show_Off_MainMenu();
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (mainMenuPanel.Visible == false)
            {
                Show_On_MainMenu();
            }
            else
                Show_Off_MainMenu();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            if (mainMenuPanel.Visible == false)
            {
                Show_On_MainMenu();
            }
            else
                Show_Off_MainMenu();
        }

        private void EditStatus()
        {
            if (isEditing)
            {
                editableTitle.ForeColor = Color.Green;
                editableTitle.Text = "Munka betöltve";
            }
            else
            {
                editableTitle.ForeColor = Color.BlueViolet;
                editableTitle.Text = "Hozz létre egy új munkát";
            }
        }

        private void editButton_Click(object sender, EventArgs e)
        {
            if (isEditing)
            {
                if(editToolPanel.Visible == true)
                {
                    editToolPanel.Visible = false;
                }
                else
                {
                    editToolPanel.Visible = true;
                }
            }
        }

        private void editLink_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (isEditing)
            {
                if (editToolPanel.Visible == true)
                {
                    editToolPanel.Visible = false;
                }
                else
                {
                    editToolPanel.Visible = true;
                }
            }
        }

        private void shapesOpenCloseButton_Click(object sender, EventArgs e)
        {
            if (isEditing)
            {
                if (shapeItemsPanel.Visible == false)
                {
                    shapeItemsPanel.Visible = true;
                    lastOpenShapes = true;
                }
                else
                {
                    shapeItemsPanel.Visible = false;
                    lastOpenShapes = false;
                }

            }
        }



       



        private void pictureBox4_Click(object sender, EventArgs e)
        {
            AskedExitSave = true;
            formHandler.Exit_On_Save(sandboxPanel);
            Application.Exit();
        }

        private void exitLink_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            AskedExitSave = true;
            formHandler.Exit_On_Save(sandboxPanel);
            Application.Exit();
        }

        private void sandboxPanel_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            g.ScaleTransform(zoom, zoom);
        }


        private void settingsLink_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            formHandler.SwitchForm(this, new Settings(),"settingsPanel");
        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            formHandler.SwitchForm(this, new Settings(), "settingsPanel");
        }

        private void uploadLink_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            EventsHandler.Load_Work(sandboxPanel);
        }

        private void homePanel_Paint(object sender, PaintEventArgs e)
        {
            if (GlobalVariables.ActiveMenuSection == "homePanel")
            {
                draw.DrawBorder(sender, e);
            }
        }


        private void MainScreen_Load(object sender, EventArgs e)
        {
            this.Width = Properties.Settings.Default.ScreenSize.X;
            this.Height = Properties.Settings.Default.ScreenSize.Y;
        }

        private void saveButton_Click(object sender, EventArgs e)
        {
            EventsHandler.Save_Work(sandboxPanel);
        }

        private void saveLink_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            EventsHandler.Save_Work(sandboxPanel);
        }

        private void sandboxPanel_MouseDown(object sender, MouseEventArgs e)
        {
            //stateHandler.Save(sandboxPanel);
        }

        private void shapeItemsPanel_MouseLeave(object sender, EventArgs e)
        {
            LoadShapeList.InitList(shapeItemsPanel);
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            EventsHandler.Load_Work(sandboxPanel);
        }

        private void MainScreen_FormClosing(object sender, FormClosingEventArgs e)
        {
            if(AskedExitSave == false)
                formHandler.Exit_On_Save(sandboxPanel);  
        }

        private void createChainButton_Click(object sender, EventArgs e)
        {
            if(isLinking == false)
            {
                createChainButton.Image = Properties.Resources.times_solid;
                this.Cursor = new Cursor(@"C:\Users\bakon\source\repos\TopoRough_2\TopoRough\Assets\Icons\link-solid.ico");
                isLinking = true;
            }
            else
            {
                createChainButton.Image = Properties.Resources.link_solid;
                this.Cursor = Cursors.Default;
                isLinking = false;
            }
        }

        private void testShape1_Click(object sender, EventArgs e)
        {
            if (isLinking)
            {
               
            }
        }
    }
    public static class ControlExtensions
    {
        public static T Clone<T>(this T controlToClone)
            where T : Control
        {
            PropertyInfo[] controlProperties = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance);

            T instance = Activator.CreateInstance<T>();

            foreach (PropertyInfo propInfo in controlProperties)
            {
                if (propInfo.CanWrite)
                {
                    if (propInfo.Name != "WindowTarget")
                        propInfo.SetValue(instance, propInfo.GetValue(controlToClone, null), null);
                }
            }

            instance.Parent = null;

            return instance;
        }

        public static T Cast<T>(object o)
        {
            return (T)o;
        }
    }
}
