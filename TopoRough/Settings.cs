using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TopoRough.Models;

namespace TopoRough
{
    public partial class Settings : Form
    {
        private Drawing draw = new Drawing();
        private FormHandler formHandler = new FormHandler();

        public Settings()
        {
            InitializeComponent();
        }

        private void homeLink_Click(object sender, LinkLabelLinkClickedEventArgs e)
        {
            formHandler.SwitchForm(ActiveForm, new MainScreen(),"homePanel");
        }
        private void homeButton_Click(object sender, EventArgs e)
        {
            formHandler.SwitchForm(ActiveForm, new MainScreen(), "homePanel");
        }

        private void settingsPanel_Paint(object sender, PaintEventArgs e)
        {
            if (GlobalVariables.ActiveMenuSection == "settingsPanel")
            {
                draw.DrawBorder(sender, e);
            }
        }
    }
}
