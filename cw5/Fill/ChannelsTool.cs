using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace PainterApplication
{
    public class ChannelsTool : ITool, IFilter
    {
        public Bitmap ImageData { get; set; }

        public bool Red { get; set; }
        public bool Green { get; set; }
        public bool Blue { get; set; }
        public MainWindow MainWindow { get; set; }

    public ChannelsWindow ChannelsWindow { get; set; }

        public void ApplyFilter()
        {
            for (var i = 0; i < ImageData.Size.Height; ++i)
                for (var j = 0; j < ImageData.Size.Width; ++j)
                {
                    Red = ChannelsWindow.redCheckBox.IsChecked.Value;
                    Green = ChannelsWindow.redCheckBox_Copy.IsChecked.Value;
                    Blue = ChannelsWindow.redCheckBox_Copy1.IsChecked.Value;
                    var pixel = ImageData.GetPixel(j, i);
                    var newPixel = Color.FromArgb(
                        pixel.A, Red ? pixel.R : 0,
                                 Green ? pixel.G : 0,
                                 Blue ? pixel.B : 0);
                    ImageData.SetPixel(j, i, newPixel);
                }
            MainWindow.Render();
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
