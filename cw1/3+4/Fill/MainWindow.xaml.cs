using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.IO;
using System.Windows.Threading;
using Engine;

namespace PainterApplication
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        RenderTargetBitmap renderTargetBitmap;

        List<Ball> objects = new List<Ball>();
        World world = new World(new Size(829, 553));
        System.Diagnostics.Stopwatch timer;
        long previousTime;

        const int ballsAmount = 50;


        public MainWindow()
        {
            InitializeComponent();

            renderTargetBitmap = new RenderTargetBitmap(
                829, 553, 
                96, 96, 
                PixelFormats.Default);

            mainImage.Source = CreateBitmap(
                829, 553, 96,
                drawingContext =>
                {
                });

            DispatcherTimer t = new DispatcherTimer();
            t.Tick += Tick;
            t.Interval = new TimeSpan(0, 0, 0, 0, 1000 / 60);
            t.Start();

            timer = System.Diagnostics.Stopwatch.StartNew();
            previousTime = 0;

            var random = new Random();
            for (int i = 0; i < ballsAmount; ++i)
            {
                var color = new Color();
                color.R = (byte)random.Next(0, 255);
                color.G = (byte)random.Next(0, 255);
                color.B = (byte)random.Next(0, 255);
                color.A = 255;
                var radius = random.NextDouble() * 40 + 5.0;
                objects.Add(
                    new Ball(
                        new Point(random.NextDouble() * (world.Size.Width - radius) + radius,
                                  random.NextDouble() * (world.Size.Height - radius) + radius),
                        new Point(random.NextDouble() * 1000.0 - 500.0, random.NextDouble() * 1000.0 - 500.0),
                        radius,
                        color
                        ));
            }
        }


        void Tick(object sender, EventArgs e)
        {
            var currentTime = timer.ElapsedMilliseconds;
            foreach (var x in objects)
            {
                x.Update((currentTime - previousTime) / (float)1000.0, world);
            }
            Render();
            previousTime = currentTime;
        }

        public BitmapSource CreateBitmap(
            int width, 
            int height, 
            double dpi, 
            Action<DrawingContext> initialRenderAction)
        {
            DrawingVisual drawingVisual = new DrawingVisual();
            using (DrawingContext drawingContext = drawingVisual.RenderOpen())
            {
                initialRenderAction(drawingContext);
            }
            renderTargetBitmap.Render(drawingVisual);

            return renderTargetBitmap;
        }
        
        public void Render()
        {
            DrawingVisual drawingVisual = new DrawingVisual();
            using (DrawingContext drawingContext = drawingVisual.RenderOpen())
            {
                world.Render(drawingContext);
                foreach (var x in objects)
                    x.Render(drawingContext);
            }
            renderTargetBitmap.Render(drawingVisual);
        }
        
    }
}
