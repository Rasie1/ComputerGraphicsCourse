using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
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
using System.Runtime.InteropServices;

namespace PainterApplication
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : System.Windows.Window
    {
        List<Ball> objects = new List<Ball>();
        World world = new World(new Size(0, 0));
        System.Diagnostics.Stopwatch timer;
        long previousTime;

        const int ballsAmount = 500;
        double gravityChange = 100;

        byte[] rgbValues;
        System.Drawing.Bitmap bmp;
        System.Drawing.Graphics graphics;


        public MainWindow()
        {
            InitializeComponent();

            LoadWorld();

            DispatcherTimer t = new DispatcherTimer();
            t.Tick += Tick;
            t.Interval = new TimeSpan(0, 0, 0, 0, 1000 / 60);
            t.Start();

            timer = System.Diagnostics.Stopwatch.StartNew();
            previousTime = 0;

            LoadRenderer();
        }

        private void LoadRenderer()
        {
            bmp = new System.Drawing.Bitmap(642, 445);
            graphics = System.Drawing.Graphics.FromImage(bmp);

            System.Drawing.Rectangle rect = new System.Drawing.Rectangle(0, 0, bmp.Width, bmp.Height);
            System.Drawing.Imaging.BitmapData bmpData =
                bmp.LockBits(rect, System.Drawing.Imaging.ImageLockMode.ReadWrite,
                bmp.PixelFormat);

            IntPtr ptr = bmpData.Scan0;

            int bytes = Math.Abs(bmpData.Stride) * bmp.Height;
            rgbValues = new byte[bytes];

            System.Runtime.InteropServices.Marshal.Copy(rgbValues, 0, ptr, bytes);

            bmp.UnlockBits(bmpData);

            Render();
        }

        public void LoadWorld()
        {
            var worldSize = new Size(642, 445);
            world.Size = worldSize;

            var random = new Random();
            for (int i = 0; i < ballsAmount; ++i)
            {
                var color = System.Drawing.Color.FromArgb(255, random.Next(0, 255), random.Next(0, 255), random.Next(0, 255));
                var radius = random.NextDouble() * 65 + 5.0;

                var speed = new Point((int)(random.NextDouble() * 1000.0 - 500.0),
                                      (int)(random.NextDouble() * 1000.0 - 500.0));
                var position =
                    new Point((int)(random.NextDouble() * (world.Size.Width - radius) + radius + speed.X),
                              (int)(random.NextDouble() * (world.Size.Height - radius) + radius + speed.Y));
                objects.Add(new Ball(position, speed, radius, color));
            }
        }


        [DllImport("gdi32")]
        static extern int DeleteObject(IntPtr o);

        public static BitmapSource loadBitmap(System.Drawing.Bitmap source)
        {
            IntPtr ip = source.GetHbitmap();
            BitmapSource bs = null;
            try
            {
                bs = System.Windows.Interop.Imaging.CreateBitmapSourceFromHBitmap(ip,
                   IntPtr.Zero, System.Windows.Int32Rect.Empty,
                   System.Windows.Media.Imaging.BitmapSizeOptions.FromEmptyOptions());
            }
            finally
            {
                DeleteObject(ip);
            }

            return bs;
        }

        void Tick(object sender, EventArgs e)
        {
            var currentTime = timer.ElapsedMilliseconds;
            foreach (var x in objects)
            {
                x.Update((currentTime - previousTime) / (Double)1000.0, world);
            }
            Render();
            previousTime = currentTime;
        }
        
        public void Render()
        {
            world.Render(graphics);
            foreach (var x in objects)
                x.Render(graphics);

            mainImage.Source = loadBitmap(bmp);
        }

        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Left)
            {
                world.Gravity = new Point(-(int)gravityChange, 0);
            }
            if (e.Key == Key.Right)
            {
                world.Gravity = new Point((int)gravityChange, 0);
            }
            if (e.Key == Key.Up)
            {
                world.Gravity = new Point(0, -(int)gravityChange);
            }
            if (e.Key == Key.Down)
            {
                world.Gravity = new Point(0, (int)gravityChange);
            }
        }

        private void Window_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Up || e.Key == Key.Down || e.Key == Key.Left || e.Key == Key.Right)
                world.Gravity = new Point(0, 0);
        }
    }
}
