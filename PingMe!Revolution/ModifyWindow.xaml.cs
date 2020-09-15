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
        private string[] nameList;
        private string[] ipList;
        public ModifyWindow(string[] names, string[] ips, Home home)
        {
            nameList = names;
            ipList = ips;
            populateListView(LVNames, names);
            
            homeRef = home;
            InitializeComponent();
        }

        void populateListView(ListView lv, string[] names)
        {
            lv.Items.Clear();
            foreach (string name in names)
            {
                lv.Items.Add(name);
            }
        }

        private void BtnOK_Click(object sender, RoutedEventArgs e)
        {
            /*homeRef.ModifyData(TBName.Text, TBIp.Text, indexItem);
            this.Close();*/
        }

        private void ListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void LVNames_OnMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            DependencyObject dep = (DependencyObject)e.OriginalSource;
            while ((dep != null) && !(dep is ListViewItem))
            {
                dep = VisualTreeHelper.GetParent(dep);
            }

            if (dep == null)
                return;
            String selectedName = (String)LVNames.ItemContainerGenerator.ItemFromContainer(dep);
            TBName.Text = selectedName;
            int selectedIndex = Array.IndexOf(nameList, selectedName);
            TBIp.Text = ipList[selectedIndex];
        }
    }
}
