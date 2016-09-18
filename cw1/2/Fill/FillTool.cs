using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;

namespace PainterApplication
{
    public class FillTool : ITool
    {
        public void Down(DrawingContext target, Point pos)
        {
        }

        public void Up(DrawingContext target, Point pos)
        {
        }

        public void Move(DrawingContext target, Point pos)
        {

        }

        //private void FillImage(Image img, Point position, Color color)
        //{
        //    var brush = new SolidBrush(color);
        //    var pen = new Pen(brush);
            
        //    using (Graphics g = Graphics.FromImage(img))
        //    {
        //        g.DrawLine(pen, new Point(0, 0), position);
        //    }
        //}
    }
}
