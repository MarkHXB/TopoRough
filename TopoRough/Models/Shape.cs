using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Serialization;
using TopoRough.Events;

namespace TopoRough.Models
{
    public class ConfigShape
    {
        public  List<Shape> Elements { get; set; } = new List<Shape>();
    }
    public class Shape 
    {
        public int Id { get; set; }
        public string Type { get; set; }
        public string Name { get; set; }
        public string Text { get; set; }
        public Point Location { get; set; }
        public string Image { get; set; }
        public Size Size { get; set; }
        public Color Color { get; set; }
        /*
         * 1. Frame = SandBoxPanel
         * 2. RootElements = Panelok = ábrák
         * 3. ChildElements = Picturebox ...
         */
        
  
        public static Shape ToObject(object item)
        {
            Shape shape = new Shape();

            try
            {
                shape.Type = item.GetType().ToString();
                if (shape.Type.Contains("Panel"))
                {
                    Panel element = (Panel)item;
                    shape.Name = $"{element.Name}";
                    shape.Location = (Point)element.Location;
                    shape.Size = element.Size;
                }
                else if (shape.Type.Contains("Label"))
                {
                    Label element = (Label)item;
                    shape.Name = $"{element.Name}";
                    shape.Location = (Point)element.Location;
                    shape.Size = element.Size;
                    shape.Text = element.Text;
                }
                else if (shape.Type.Contains("PictureBox"))
                {
                    PictureBox element = (PictureBox)item;
                    shape.Name = $"{element.Name}";
                    shape.Location = (Point)element.Location;
                    shape.Size = element.Size;
                    shape.Image = element.ImageLocation;
                }
            }
            catch (Exception x)
            {
                MessageBox.Show(x.Message);
            }
            

            return shape;
        }
        public static PictureBox ToPictureBox(Shape shape)
        {
            PictureBox pictureBox = new PictureBox();

            try
            {
                if(shape.Type.Contains("PictureBox"))
                {
                    pictureBox.Name = shape.Name;
                    pictureBox.Size = shape.Size;
                    pictureBox.Location = shape.Location;
                    pictureBox.Load(shape.Image);
                    pictureBox.SizeMode = PictureBoxSizeMode.Zoom;
                }
            }
            catch (Exception x)
            {
                MessageBox.Show(x.Message);
            }

            return pictureBox;
        }
        public static void AppendEvents(PictureBox pictureBox)
        {
            pictureBox.MouseMove += EventsHandler.shapeMoving_MouseMove;
            pictureBox.MouseDown += EventsHandler.shapeMoving_MouseDown;
        }
    }
}
