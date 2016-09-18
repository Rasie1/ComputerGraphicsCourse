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
using System.Drawing;
using System.Drawing.Imaging;
using System.Runtime.InteropServices;
using System.IO;

namespace PainterApplication
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        bool isMouseDown = false;
        RenderTargetBitmap renderTargetBitmap;

        ITool currentTool;

        ITool noTool = new NoTool();
        ITool brushTool = new BrushTool();
        ITool fillTool = new FillTool();

        public MainWindow()
        {
            InitializeComponent();

            SelectBrushTool();

            renderTargetBitmap = new RenderTargetBitmap(
                829, 553, 
                96, 96, 
                PixelFormats.Default);

            mainImage.Source = CreateBitmap(
                829, 553, 96,
                drawingContext =>
                {
                });
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

        private void OpenImage(BitmapImage bmp)
        {
            DrawingVisual drawingVisual = new DrawingVisual();
            var rect = new System.Windows.Rect(0, 0, 256, 256);
            using (DrawingContext drawingContext = drawingVisual.RenderOpen())
                drawingContext.DrawImage(bmp, rect);
            renderTargetBitmap.Render(drawingVisual);
        }


        private void openButton_Click(object sender, RoutedEventArgs e)
        {
            ShowMessageInToolbox("Open - coming soon in CW2");

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

            DrawingVisual drawingVisual = new DrawingVisual();
            using (DrawingContext drawingContext = drawingVisual.RenderOpen())
                currentTool.Down(drawingContext, pos);
            renderTargetBitmap.Render(drawingVisual);
        }

        private void mainImage_MouseMove(object sender, MouseEventArgs e)
        {
            if (isMouseDown == true)
            {
                var pos = e.GetPosition(this.mainImage);
                this.label.Content = pos.ToString();

                DrawingVisual drawingVisual = new DrawingVisual();
                using (DrawingContext drawingContext = drawingVisual.RenderOpen())
                    currentTool.Move(drawingContext, pos);
                renderTargetBitmap.Render(drawingVisual);
            }
        }

        private void mainImage_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            isMouseDown = false;

            var pos = e.GetPosition(this.mainImage);
            this.label.Content = pos.ToString();

            DrawingVisual drawingVisual = new DrawingVisual();
            using (DrawingContext drawingContext = drawingVisual.RenderOpen())
                currentTool.Up(drawingContext, pos);
            renderTargetBitmap.Render(drawingVisual);
        }

        private void UnblockToolButtons()
        {
            fillButton.IsEnabled = true;
            brushButton.IsEnabled = true;
        }

        private void fillButton_Click(object sender, RoutedEventArgs e)
        {
            ShowMessageInToolbox("Fill selected - coming soon in CW2");
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
                SaveClipboardImageToFile(renderTargetBitmap, filename);
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
    }
}
