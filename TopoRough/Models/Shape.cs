using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Serialization;

namespace TopoRough.Models
{

    public class Shape 
    {
        public int Id { get; set; }
        public string Type { get; set; }
        public string Name { get; set; }
        public string Text { get; set; }
        public Point Location { get; set; }
        public string Image { get; set; }
        public Size Size { get; set; }
        public List<Shape> childElements { get; set; }
        public static List<Panel> ChildElements { get; set; }
        public static Shape RootElement { get; set; }

        public static void Serialize(string fileName, object shapeObject)
        {
            try
            {
                using(FileStream stream = new FileStream(fileName, FileMode.Create))
                {
                    XmlSerializer xml = new XmlSerializer(typeof(Shape));
                    xml.Serialize(stream, shapeObject);
                }
            }
            catch (Exception x)
            {
                MessageBox.Show(x.Message);
            }
        }

        public static object Deserialize(string fileName)
        {
            Control result = null;

            if(fileName == null)
            {
                return null;
            }

            try
            {
                Shape shape = null;

                using (FileStream fs = File.OpenRead($"{fileName}"))
                {
                    XmlSerializer xml = new XmlSerializer(typeof(Shape));
                    shape = (Shape)xml.Deserialize(fs);
                }

                if(shape.Type == "panel")
                {
                    result = new Panel();
                    result.Name = $"{shape.Name}{shape.Id}";
                    result.Location = (Point)shape.Location;
                    result.Size = shape.Size;
                    
                }
                else if (shape.Type == "picturebox")
                {
                    result = new PictureBox();
                    result.Name = $"{shape.Name}{shape.Id}";
                    result.Location = (Point)shape.Location;
                    result.Size = shape.Size;
                    RootElements.Controls.Add(result);
                }
                else if (shape.Type == "label")
                {
                    result = new Label();
                    result.Name = $"{shape.Name}{shape.Id}";
                    result.Location = (Point)shape.Location;
                    result.Size = shape.Size;
                    result.Text = shape.Text;
                    RootElements.Controls.Add(result);
                }

                return RootElements;
            }
            catch(FileLoadException fileLoadError)
            {
                MessageBox.Show(fileLoadError.Message);
            }
            catch (Exception x)
            {
                MessageBox.Show(x.Message);
            }

            return result = new Panel();
        }

        public static void Save(string fileName,Panel workPanel)
        {
            if(fileName == null || workPanel == null)
            {
                return;
            }

            try
            {
                RootElement = Shape.ToObject(workPanel);

                foreach (var item in workPanel.Controls)
                {
                    ChildElements.Add((Panel)item);
                }

                foreach (var panel in ChildElements)
                {
                    //téglalap panelja
                    foreach (var item in panel.Controls)
                    {
                        //téglalap picturebox ; button ; stb...
                        Shape shape = Shape.ToObject(item);
                    }
                    
                }


                Serialize(fileName, RootElement);

            }
            catch (Exception x)
            {
                MessageBox.Show(x.Message);
            }
        }
        private static Shape ToObject(object item)
        {
            Shape shape = new Shape();

            shape.Type = item.GetType().ToString();
            if (shape.Type == "panel")
            {
                Panel panel = (Panel)item;
                shape.Name = $"{panel.Name}";
                shape.Location = (Point)panel.Location;
                shape.Size = panel.Size;              
            }


            return shape;
        }
    }
}
