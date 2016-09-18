using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;

namespace PainterApplication
{
    interface ITool
    {
        void Down(DrawingContext target, Point pos);
        void Up(DrawingContext target, Point pos);
        void Move(DrawingContext target, Point pos);
    }
}
