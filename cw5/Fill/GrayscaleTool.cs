using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace PainterApplication
{
    public class GrayscaleTool : ITool, IFilter
    {
        public Bitmap ImageData { get; set; }

        public void ApplyFilter()
        {
            for (var i = 0; i < ImageData.Size.Height; ++i)
                for (var j = 0; j < ImageData.Size.Width; ++j)
                {
                    var pixel = ImageData.GetPixel(j, i);
                    var lum = (int)(pixel.R * 0.2125 + pixel.G * 0.7154 + pixel.B * 0.0721);
                    var newPixel = Color.FromArgb(pixel.A, lum, lum, lum);
                    ImageData.SetPixel(j, i, newPixel);
                }
        }


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
    }
}
