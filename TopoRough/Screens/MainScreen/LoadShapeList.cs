using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;

using TopoRough.Data;

namespace TopoRough.Screens.MainScreen
{
    public class LoadShapeList
    {
        private int gapBetween = 80;
        private static int margin = 10;
        private static int shapePanelHeight = 110;
        private static int padding = 5;

        public static void InitList(Panel panel)
        {
            int panelWidth = panel.Width;
            int panelHeight = panel.Height;

            int lastItemLocY = 0;

            int counter = 0;

            foreach (var shape in TestShapes.Shapes)
            {
                Panel shapePanel = new Panel()
                {
                    Size = new Size(panelWidth - margin, shapePanelHeight),
                    Location = new Point(0 + padding, lastItemLocY + padding),
                    Name=$"shapePanelForm{counter}"
                };
                shape.Item.Location = new Point(shapePanel.Width / 2 - shape.Item.Width / 2, 0);
                shapePanel.Controls.Add(shape.Item);


                panel.Controls.Add(shapePanel);

                lastItemLocY += shapePanelHeight + padding;
                counter++;
            }
        }
    }
}
