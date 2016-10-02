using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace PainterApplication
{
    public class FillTool : ITool
    {
        public Bitmap ImageData { get; set; }
        public Color Color { get; set; }

        public void Down(Graphics target, Point pos)
        {
            Fill(target, pos);
        }

        public void Up(Graphics target, Point pos)
        {
        }

        public void Move(Graphics target, Point pos)
        {

        }

        private void SetPixel(Graphics target, Point pos)
        {
            ImageData.SetPixel(pos.X, pos.Y, Color);
        }

        private bool CheckPixel(Point pos)
        {
            if (pos.X < 0 || pos.Y < 0)
                return false;
            if (pos.X >= ImageData.Size.Width || pos.Y >= ImageData.Size.Height)
                return false;
            var pixel = ImageData.GetPixel(pos.X, pos.Y);
            if (pixel.ToArgb() == Color.ToArgb())
                return false;
            return true;
        }
        
        private void Fill(Graphics target, Point pos)
        {
            var pixels = new Stack<Point>();
            pixels.Push(pos);
            while (pixels.Count > 0)
            {
                var x = pixels.Pop();
                if (!CheckPixel(x))
                    continue;
                SetPixel(target, x);
                pixels.Push(new Point(x.X - 1, x.Y));
                pixels.Push(new Point(x.X + 1, x.Y));
                pixels.Push(new Point(x.X, x.Y - 1));
                pixels.Push(new Point(x.X, x.Y + 1));
            }
        }
    }
}
