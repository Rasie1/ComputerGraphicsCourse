using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;

namespace Engine
{
    class Ball : IRenderable, IActor
    {

        public Point Gravity { get; set; }
        public Point Position { get; set; }
        public Point Speed { get; set; }
        public Double Radius { get; set; }

        Pen pen;
        Brush brush;

        public Ball(Point position, Point speed, Double radius, Color color)
        {
            brush = new SolidColorBrush(color);
            pen = new Pen(brush, 2);

            Gravity = new Point(0, 0);
            Position = position;
            Speed = speed;
            Radius = radius;
        }

        private Point CalculateNextPosition(float dt)
        {
            return new Point(
                Position.X + (Speed.X + Gravity.X) * dt,
                Position.Y + (Speed.Y + Gravity.Y) * dt);
        }

        public void Update(float dt, World world)
        {
            var nextPosition = CalculateNextPosition(dt);

            var outOfBoundsX = (nextPosition.X < 0 + Radius) ||
                (nextPosition.X > world.Size.Width - Radius);
            var outOfBoundsY = (nextPosition.Y < 0 + Radius) ||
                (nextPosition.Y > world.Size.Height - Radius);

            if (outOfBoundsX || outOfBoundsY)
            {
                Speed = new Point(outOfBoundsX ? -Speed.X : Speed.Y,
                                  outOfBoundsY ? -Speed.Y : Speed.Y);
                nextPosition = CalculateNextPosition(dt);
            }

            Position = nextPosition;
        }

        public void Render(DrawingContext target)
        {
            target.DrawEllipse(brush, pen, Position, Radius, Radius);
        }
    }
}
