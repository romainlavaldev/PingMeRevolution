using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace PingMe_Revolution
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        

        public MainWindow()
        {
            InitializeComponent();
            if (!File.Exists(System.Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\PingMeData\\data.txt"))
            {
                InitFiles();
            }
            BtnHome_Click(BtnHome, null);


        }

        

        private void TitleBar_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            this.DragMove();
        }

        private void BtnClose_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void BtnMinimize_Click(object sender, RoutedEventArgs e)
        {
            this.WindowState = WindowState.Minimized;
        }

        private void InitFiles()
        {
            Directory.CreateDirectory(System.Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\PingMeData");
            File.Create(System.Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\PingMeData\\data.txt");
        }

        private void BtnDonate_Click(object sender, RoutedEventArgs e)
        {
            HideRectAndBack();
            RectBtnDonate.Visibility = Visibility.Visible;
            BtnDonate.Background = (Brush)new BrushConverter().ConvertFrom("#41454b");
        }

        private void BtnHelp_Click(object sender, RoutedEventArgs e)
        {
            HideRectAndBack();
            RectBtnHelp.Visibility = Visibility.Visible;
            BtnHelp.Background = (Brush)new BrushConverter().ConvertFrom("#41454b");
        }

        private void BtnAddresses_Click(object sender, RoutedEventArgs e)
        {
            HideRectAndBack();
            RectBtnAddresses.Visibility = Visibility.Visible;
            BtnAddresses.Background = (Brush)new BrushConverter().ConvertFrom("#41454b");
        }

        private void BtnHome_Click(object sender, RoutedEventArgs e)
        {
            HideRectAndBack();
            RectBtnHome.Visibility = Visibility.Visible;
            BtnHome.Background = (Brush)new BrushConverter().ConvertFrom("#41454b");
        }

        private void HideRectAndBack()
        {
            RectBtnHelp.Visibility = Visibility.Hidden;
            RectBtnAddresses.Visibility = Visibility.Hidden;
            RectBtnDonate.Visibility = Visibility.Hidden;
            RectBtnHome.Visibility = Visibility.Hidden;

            BtnHelp.Background = Brushes.Transparent;
            BtnAddresses.Background = Brushes.Transparent;
            BtnDonate.Background = Brushes.Transparent;
            BtnHome.Background = Brushes.Transparent;
        }
    }
}
