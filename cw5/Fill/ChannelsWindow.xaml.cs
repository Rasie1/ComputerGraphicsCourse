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
using System.Windows.Shapes;

namespace PainterApplication
{
    /// <summary>
    /// Interaction logic for ChannelsWindow.xaml
    /// </summary>
    public partial class ChannelsWindow : Window
    {
        public ChannelsTool ChannelsTool { get; set; }
        public ChannelsWindow()
        {
            InitializeComponent();
        }

        //green, todo: decent name
        private void redCheckBox_Copy_Checked(object sender, RoutedEventArgs e)
        {

        }

        private void redCheckBox_Checked(object sender, RoutedEventArgs e)
        {

        }

        // blue
        private void redCheckBox_Copy1_Checked(object sender, RoutedEventArgs e)
        {

        }

        private void applyButton_Click(object sender, RoutedEventArgs e)
        {
            ChannelsTool.ApplyFilter();
        }
    }
}
