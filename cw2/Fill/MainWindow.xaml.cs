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
using System.Drawing;
using System.Drawing.Imaging;
using System.Runtime.InteropServices;
using System.IO;
using System.Windows;

namespace PainterApplication
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : System.Windows.Window
    {
        bool isMouseDown = false;

        ITool currentTool;

        ITool noTool = new NoTool();
        ITool brushTool = new BrushTool();
        ITool fillTool = new FillTool();


        byte[] rgbValues;
        System.Drawing.Bitmap bmp;
        System.Drawing.Graphics graphics;

        System.Drawing.Color color;


        public MainWindow()
        {
            InitializeComponent();

            SelectBrushTool();
            

            LoadRenderer();

            var fill = fillTool as FillTool;
            fill.ImageData = bmp;
            fill.Color = System.Drawing.Color.Black;
        }

        private void SetColor(System.Drawing.Color newColor)
        {
            var fill = fillTool as FillTool;
            color = newColor;
            fill.Color = color;
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

        public void Render()
        {
            mainImage.Source = loadBitmap(bmp);
        }
        
        private void OpenImage(BitmapImage bmp)
        {
            //DrawingVisual drawingVisual = new DrawingVisual();
            //var rect = new System.Windows.Rect(0, 0, 
            //    mainImage.DesiredSize.Width, mainImage.DesiredSize.Height);
            //using (DrawingContext drawingContext = drawingVisual.RenderOpen())
            //    drawingContext.DrawImage(bmp, rect);
            //renderTargetBitmap.Render(drawingVisual);
        }

        private void openButton_Click(object sender, RoutedEventArgs e)
        {
            var dialog = new Microsoft.Win32.OpenFileDialog();
            var result = dialog.ShowDialog();

            if (result == true)
            {
                var filename = dialog.FileName;
                
                OpenImage(new BitmapImage(new Uri(filename)));
            }

        }

        private void ShowMessageInToolbox(string s)
        {
            label.Content = s;
        }
        
        private void mainImage_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            var pos = e.GetPosition(this.mainImage);
            this.label.Content = pos.ToString();
            isMouseDown = true;

            currentTool.Down(graphics, new System.Drawing.Point((int)pos.X, (int)pos.Y));
            Render();
        }

        private void mainImage_MouseMove(object sender, MouseEventArgs e)
        {
            if (isMouseDown == true)
            {
                var pos = e.GetPosition(this.mainImage);
                this.label.Content = pos.ToString();

                currentTool.Move(graphics, new System.Drawing.Point((int)pos.X, (int)pos.Y));
                Render();
            }
        }

        private void mainImage_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            isMouseDown = false;

            var pos = e.GetPosition(this.mainImage);
            this.label.Content = pos.ToString();

            currentTool.Up(graphics, new System.Drawing.Point((int)pos.X, (int)pos.Y));

            Render();
        }

        private void UnblockToolButtons()
        {
            fillButton.IsEnabled = true;
            brushButton.IsEnabled = true;
        }

        private void fillButton_Click(object sender, RoutedEventArgs e)
        {
            ShowMessageInToolbox("Fill selected");
            UnblockToolButtons();
            SelectFillTool();
        }

        public void SelectBrushTool()
        {
            brushButton.IsEnabled = false;
            currentTool = brushTool;
        }

        public void SelectFillTool()
        {
            fillButton.IsEnabled = false;
            currentTool = fillTool;
        }

        private void brushButton_Click(object sender, RoutedEventArgs e)
        {
            ShowMessageInToolbox("Brush selected");
            UnblockToolButtons();
            SelectBrushTool();
        }

        private void mainImage_MouseLeave(object sender, MouseEventArgs e)
        {
            isMouseDown = false;
        }

        private void mainImage_MouseEnter(object sender, MouseEventArgs e)
        {
            // todo: set isMouseDown if button is pressed
        }

        private void saveButton_Click(object sender, RoutedEventArgs e)
        {
            var dialog = new Microsoft.Win32.SaveFileDialog();
            dialog.DefaultExt = "png";
            dialog.FileName = "image";
            var result = dialog.ShowDialog();

            if (result == true)
            {
                var filename = dialog.FileName;
                SaveClipboardImageToFile(loadBitmap(bmp), filename);
            }
        }

        public static void SaveClipboardImageToFile(BitmapSource image, string filePath)
        {
            using (var fileStream = new FileStream(filePath, FileMode.Create))
            {
                BitmapEncoder encoder = new PngBitmapEncoder();
                encoder.Frames.Add(BitmapFrame.Create(image));
                encoder.Save(fileStream);
            }
        }

        private void colorImage_MouseDown(object sender, MouseButtonEventArgs e)
        {
            SetColor(System.Drawing.Color.FromArgb(255, 14, 14, 14));

        }
    }
}
