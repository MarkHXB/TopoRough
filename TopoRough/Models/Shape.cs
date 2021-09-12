﻿using System;
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

        public static object Deserialize(string fileName)
        {
            return new object();
        }

        public static void Save(string fileName,Panel workPanel)
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
    }
}
