using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Windows.Forms;

using TopoRough.Constants;
using TopoRough.Models;

namespace TopoRough.Screens
{
    public class SideMenu
    {
        public SideMenu()
        {

        }

        public static string AppName = TopoRough.Constants.Texts.AppName;

        public static List<MenuItem> MenuItems = new List<MenuItem>()
        {
            new MenuItem(PictureBoxes.SaveButton, "Mentés"),
            new MenuItem(PictureBoxes.SaveButton, "Betöltés"),
            new MenuItem(PictureBoxes.SaveButton, "Beállítások"),
            new MenuItem(PictureBoxes.SaveButton, "Kilépés"),
        };
    }
}


/*
 * 
 * Mit kell még fejlesztenem?
 *
 * Az ikon és a szöveg is kattintható, illetve navigálható képességű legyen
 * 
 */