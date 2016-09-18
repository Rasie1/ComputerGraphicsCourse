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
        public Color Color { get; set; }
        public void Down(Graphics target, Point pos)
        {
        }

        public void Up(Graphics target, Point pos)
        {
        }

        public void Move(Graphics target, Point pos)
        {

        }

        private void SetPixel(Graphics target, Point pos)
        {

        }
        
        private void Fill(Graphics target, Point pos)
        {
            if (pos.X < 0 || pos.Y < 0)
                return;
            SetPixel(target, pos);
            Fill(target, new Point(pos.X - 1, pos.Y));
            Fill(target, new Point(pos.X, pos.Y - 1));
            Fill(target, new Point(pos.X + 1, pos.Y));
            Fill(target, new Point(pos.X, pos.Y + 1));
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
