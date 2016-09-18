using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;

namespace PainterApplication
{
    public class NoTool : ITool
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
    }
}
