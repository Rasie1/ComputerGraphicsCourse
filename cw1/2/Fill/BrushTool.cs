using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;

namespace PainterApplication
{
    public class BrushTool : ITool
    {
        Pen pen;
        Brush brush;
        
        Point? previousPoint;

        public BrushTool()
        {
            brush = new SolidColorBrush(Colors.Black);
            pen = new Pen(brush, 2);
        }
        
        
        public void Down(DrawingContext target, Point pos)
        {
            previousPoint = pos;
            target.DrawLine(pen, pos, previousPoint.Value);
        }

        public void Up(DrawingContext target, Point pos)
        {
            if (previousPoint == null)
                return;
            target.DrawLine(pen, pos, previousPoint.Value);
            previousPoint = null;
        }

        public void Move(DrawingContext target, Point pos)
        {
            target.DrawLine(pen, pos, previousPoint.Value);
            previousPoint = pos;
        }
    }
}
