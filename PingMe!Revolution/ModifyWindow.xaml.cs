using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace PingMe_Revolution
{
    /// <summary>
    /// Logique d'interaction pour ModifyWindow.xaml
    /// </summary>
    public partial class ModifyWindow : Window
    {
        Home homeRef;
        int indexItem;
        public ModifyWindow(string name, string ip, Home home, int index)
        {
            indexItem = index;
            homeRef = home;
            InitializeComponent();
            TBName.Text = name;
            TBIp.Text = ip;
        }

        private void BtnOK_Click(object sender, RoutedEventArgs e)
        {
            homeRef.ModifyData(TBName.Text, TBIp.Text, indexItem);
            this.Close();
        }
    }
}
