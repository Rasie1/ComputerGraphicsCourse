using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace Engine
{
    class World : IRenderable
    {
        Pen pen;
        Brush brush;

        public Size Size { get; set; }
        public Point Gravity { get; set; }
        public Double MaxSpeed { get; set; }
        public Double Friction { get; set; }

        public World(Size size)
        {
            brush = new SolidBrush(Color.Azure);
            pen = new Pen(brush, 1000);

            Size = size;
            Gravity = new Point(0, 0);
            MaxSpeed = 3000;
            Friction = 0.975;
        }
        
        public void Render(Graphics target)
        {
            var rect = new Rectangle(new Point(0, 0), Size);
            target.DrawRectangle(pen, rect);
        }
    }
}
