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
        

        public static void Serialize(string fileName, object shapeObject)
        {
          
            try
            {
                using(FileStream stream = new FileStream(fileName, FileMode.Create))
                {
                    XmlSerializer xml = new XmlSerializer(typeof(ConfigShape));
                    xml.Serialize(stream, shapeObject);
                    
                }
            }
            catch (Exception x)
            {
                MessageBox.Show(x.Message);
            }
        }

        public static ConfigShape Deserialize(string fileName = "Save.xml")
        {
            ConfigShape Output = new ConfigShape();
            try
            {
                using (FileStream stream = new FileStream(fileName, FileMode.Open))
                {
                    XmlSerializer xml = new XmlSerializer(typeof(ConfigShape));
                    Output = (ConfigShape)xml.Deserialize(stream);
                }
            }
            catch (Exception x)
            {
                MessageBox.Show(x.Message);
            }
            return Output;
        }

        public static void Save(Panel workPanel,string fileName = "Save.xml")
        {
            ConfigShape shape = new ConfigShape();
             
            if(fileName == null || workPanel == null)
            {
                return;
            }

            try
            {
                
                foreach (var item in workPanel.Controls)
                {
                    if(item.GetType().ToString().Contains("PictureBox"))
                        shape.Elements.Add(Shape.ToObject(item));
                }

                Serialize(fileName, shape);
            }
            catch (Exception x)
            {
                MessageBox.Show(x.Message);
                MessageBox.Show("Save probléma");
            }
        }
        public static List<PictureBox> Load(string fileName = "Save.xml")
        {
            if (fileName == null)
            {
                return new List<PictureBox>();
            }

            List<PictureBox> Output = new List<PictureBox>();

            try
            {
                ConfigShape loadedShapes = Deserialize();
                foreach (var shape in loadedShapes.Elements)
                {
                    PictureBox generatedPictureBox = (PictureBox)Shape.ToPictureBox(shape);

                    AppendEvents(generatedPictureBox);

                    Output.Add(generatedPictureBox);
                }
            }
            catch (Exception x)
            {
                MessageBox.Show(x.Message);
            }

            return Output;
        }
        private static Shape ToObject(object item)
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
        private static PictureBox ToPictureBox(Shape shape)
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
        private static void AppendEvents(PictureBox pictureBox)
        {
            pictureBox.MouseDown += EventsHandler.Shape_MouseDown;
            pictureBox.MouseUp += EventsHandler.Shape_MouseUp;
        }
    }
}
