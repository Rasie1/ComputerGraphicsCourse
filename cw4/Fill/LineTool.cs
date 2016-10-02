using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace PainterApplication
{
    public class LineTool : ITool
    {
        public Bitmap ImageData { get; set; }
        public Color Color { get; set; }

        Point? begin;
        

        public void Down(Graphics target, Point pos)
        {
            begin = pos;
        }

        public void Up(Graphics target, Point pos)
        {
            if (begin == null)
                return;
            DrawLine(target, pos, begin.Value);
            begin = null;
        }

        public void Move(Graphics target, Point pos)
        {
            
        }

        private static void Swap<T>(ref T lhs, ref T rhs) {
            T temp;
            temp = lhs;
            lhs = rhs;
            rhs = temp;
        }

        private void DrawLine(Graphics target, Point to, Point from)
        {
            int x0 = to.X;
            int y0 = to.Y;
            int x1 = from.X;
            int y1 = from.Y;
            bool steep = Math.Abs(y1 - y0) > Math.Abs(x1 - x0);
            if (steep)
            {
                Swap<int>(ref x0, ref y0);
                Swap<int>(ref x1, ref y1);
            }
            if (x0 > x1)
            {
                Swap<int>(ref x0, ref x1);
                Swap<int>(ref y0, ref y1);
            }
            int dX = (x1 - x0), 
                dY = Math.Abs(y1 - y0), 
                err = (dX / 2), 
                ystep = (y0 < y1 ? 1 : -1), 
                y = y0;

            for (int x = x0; x <= x1; ++x)
            {
                if (!(steep ? SetPixel(target, new Point(y, x))
                            : SetPixel(target, new Point(x, y))))
                    return;
                err = err - dY;
                if (err < 0)
                {
                    y += ystep;
                    err += dX;
                }
            }
        }

        private bool SetPixel(Graphics target, Point pos)
        {
            //if (ImageData.Size.Width <= pos.X || ImageData.Size.Height <= pos.Y)
            //    return false;
            ImageData.SetPixel(pos.X, pos.Y, Color);
            return true;
        }

    }
}
