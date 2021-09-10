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
        //for saving the sandbox panel content
        /// <summary>
        /// Save all the content of panel to a bitmap. It can override already existed file and save as a new one.
        /// </summary>
        /// <param name="panel"></param>
        void Save(Panel panel);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        bool PanelBmpExist(string path = "./");

        Bitmap ConvertPanelToBitmap(Panel panel);
    }

    public class State:IState
    {
        public Bitmap ConvertPanelToBitmap(Panel panel)
        {
            Graphics g = panel.CreateGraphics();
            Bitmap bmp = new Bitmap(panel.Width, panel.Height);
            panel.DrawToBitmap(bmp, new Rectangle(0, 0, panel.Width, panel.Height));

            return bmp;
        }

        public bool PanelBmpExist(string path = "./")
        {
            bool state = false;

            path += "test";

            try
            {
                StreamReader sr = new StreamReader(path);
                state = true;
                sr.Close();
            }
            catch (Exception x)
            {
                MessageBox.Show(x.Message);
                state = false;
            }

            return state;
        }

        public void Save(Panel panel)
        {
            Bitmap bmp;
            string state = "";
            bool fileIsAlive = PanelBmpExist();

            if(panel == null)
            {
                return;
            }



            if(fileIsAlive == true)
            {
                //MessageBox.Show("Már létezik");
                bmp = ConvertPanelToBitmap(panel);
                bmp.Save("test");
                state = "mentve";
                MessageBox.Show(state == "" ? "nem lett mentve" : state);
                return;
            }

            try
            {
                bmp = ConvertPanelToBitmap(panel);
                bmp.Save("test");
                state = "mentve";
            }
            catch (Exception x)
            {
                MessageBox.Show(x.Message);
            }
            finally
            {
                //MessageBox.Show(state == "" ? "nem lett mentve" : state);
            }
        }


    }

 
}
