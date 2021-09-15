using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Xml.Serialization;
using System.Xml;

namespace TopoRough.Models
{
    /*
     Az alap logika az úgy nevezett temporary mentésben =>
    Lekonvertálom a Panel-t ( tartalmát ) egy bitmap-be és azt újra felülírva is akár lehet menteni, majd betölteni.
     */

    interface IState
    {
        
    }

    public class State:IState
    {
        public static void Serialize(string fileName, object shapeObject)
        {

            try
            {
                using (FileStream stream = new FileStream(fileName, FileMode.Create))
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

        public static void Save(Panel workPanel, string fileName = "Save.xml")
        {
            ConfigShape shape = new ConfigShape();

            if (fileName == null || workPanel == null)
            {
                return;
            }

            try
            {

                foreach (var item in workPanel.Controls)
                {
                    if (item.GetType().ToString().Contains("PictureBox"))
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

                    Shape.AppendEvents(generatedPictureBox);

                    Output.Add(generatedPictureBox);
                }
            }
            catch (Exception x)
            {
                MessageBox.Show(x.Message);
            }

            return Output;
        }

    }

 
}
