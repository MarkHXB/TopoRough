using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;

using TopoRough.Data;
using TopoRough.Events;

namespace TopoRough.Screens.MainScreen
{
    public class LoadShapeList
    {
        private int gapBetween = 80;
        private static int margin = 10;
        private static int shapePanelHeight = 110;
        private static int padding = 5;
        private static int zoomO =30;
        public static void InitList(Panel panel)
        {
            TestShapes.RenderShapeList();

            int panelWidth = panel.Width;
            int panelHeight = panel.Height;

            int lastItemLocY = 0;

            int counter = 0;

            foreach (var shape in TestShapes.Shapes)
            {
                if(shape.IsAlive == false)
                {
                    counter = GlobalVariables.GeneratedShapes;

                    shape.Item.Size = new Size((panelWidth - margin) - zoomO, shapePanelHeight);
                    shape.Item.Name = shape.Title.ToLower() + "Shape" + counter.ToString();
                    shape.Item.Location = new Point(panelWidth / 2 - shape.Item.Size.Width / 2, lastItemLocY);

                    shape.Item.MouseDown += EventsHandler.shapeMoving_MouseDown;
                    shape.Item.MouseMove += EventsHandler.shapeMoving_MouseMove;

                    panel.Controls.Add(shape.Item);

                    lastItemLocY += shapePanelHeight + padding;
                    GlobalVariables.GeneratedShapes++;

                    shape.IsAlive = true;
                }
                else
                {
                    lastItemLocY += shapePanelHeight + padding;
                }          
            }
        }
    }
}
