using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;

namespace Engine
{
    class Ball : IRenderable, IActor, IBody
    {
        public Point Position { get; set; }
        public Point Speed { get; set; }
        public Double Radius { get; set; }

        Pen pen;
        Brush brush;

        public Ball(Point position, Point speed, Double radius, Color color)
        {
            brush = new SolidColorBrush(color);
            pen = new Pen(brush, 2);

            Position = position;
            Speed = speed;
            Radius = radius;
        }
        

        public void Update(Double dt, World world)
        {
            UpdatePhysics(dt, world);
        }

        public void UpdatePhysics(Double dt, World world)
        {
            Speed = new Point(
                Math.Min(world.MaxSpeed, (Speed.X + world.Gravity.X) * world.Friction),
                Math.Min(world.MaxSpeed, (Speed.Y + world.Gravity.Y) * world.Friction));
            var nextPosition = new Point(
                Position.X + (Speed.X) * dt,
                Position.Y + (Speed.Y) * dt);

            if (nextPosition.X < 0 + Radius)
            {
                nextPosition.X = Radius;
                Speed = new Point(-Speed.X, Speed.Y);
            }
            if (nextPosition.X > world.Size.Width - Radius)
            {
                nextPosition.X = world.Size.Width - Radius;
                Speed = new Point(-Speed.X, Speed.Y);
            }
            if (nextPosition.Y < 0 + Radius)
            {
                nextPosition.Y = Radius;
                Speed = new Point(Speed.X, -Speed.Y);
            }
            if (nextPosition.Y > world.Size.Height - Radius)
            {
                nextPosition.Y = world.Size.Height - Radius;
                Speed = new Point(Speed.X, -Speed.Y);
            }
            

            Position = nextPosition;
        }

        public void Render(DrawingContext target)
        {
            target.DrawEllipse(brush, pen, Position, Radius, Radius);
        }
    }
}
