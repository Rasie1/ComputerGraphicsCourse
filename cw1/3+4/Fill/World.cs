using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;

namespace Engine
{
    class World : IRenderable
    {
        Pen pen;
        Brush brush;

        public Size Size { get; private set; }

        public World(Size size)
        {
            brush = new SolidColorBrush(Colors.Azure);
            pen = new Pen(brush, 1000);

            Size = size;
        }
        
        public void Render(DrawingContext target)
        {
            var rect = new Rect(Size);
            target.DrawRectangle(brush, pen, rect);
        }
    }
}
