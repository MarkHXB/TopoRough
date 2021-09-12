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


namespace TopoRough
{
    public partial class MainScreen : Form
    {
        //mentés
        State stateHandler = new State();

        //Shape created
        private float zoom = 1f;

        private bool isEditing = true;
        private bool lastOpenShapes = false;



        //COMPONENTS & INTERFECES
        private Drawing draw = new Drawing();
        private FormHandler formHandler = new FormHandler();

        public MainScreen()
        {
            InitializeComponent();
            EditStatus();
            LoadShapeList.InitList(shapeItemsPanel);
            sandboxPanel.AllowDrop = true; 
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

            }
        }

        private void editLink_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (isEditing)
            {

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
            Application.Exit();
        }

        private void exitLink_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
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
            //Shape shape = new Shape
            //{
            //    Id = 0,
            //    Name = "test",
            //    Type = "panel",
            //    Location = new Point(100, 100),
            //    Size = new Size(100, 100),
            //    Image = Application.StartupPath+@"\Assets\Shapes\Téglalap.png"
            //};
            Shape.Save("Save.xml",sandboxPanel);
            MessageBox.Show("kész");
            //Panel test = (Panel)Shape.Deserialize("kecske.xml");
            //sandboxPanel.Controls.Add(test);
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
            
        }

        private void saveButton_Click(object sender, EventArgs e)
        {
            stateHandler.Save(sandboxPanel);
        }

        private void saveLink_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            stateHandler.Save(sandboxPanel);
        }

        private void sandboxPanel_MouseDown(object sender, MouseEventArgs e)
        {
            //stateHandler.Save(sandboxPanel);
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
    }
}
