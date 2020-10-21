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
        private List<string> nameList;
        private List<string> ipList;
        private int selectedIndex = -1;
        public ModifyWindow(List<string> names, List<string> ips, Home home)
        {
            nameList = names;
            ipList = ips;
            
            InitializeComponent();
            populateListView(LVNames, names);
            
            homeRef = home;
        }

        void populateListView(ListView lv, List<string> names)
        {
            foreach (string name in names)
            {
                lv.Items.Add(name);
            }
        }

        private void BtnOK_Click(object sender, RoutedEventArgs e)
        {
            if (LVNames.SelectedIndex == -1)
            {
                this.Close();
            }
            else
            {
                if (selectedIndex != -1)
                {
                    nameList[selectedIndex] = TBName.Text;
                    ipList[selectedIndex] = TBIp.Text;
                }
                homeRef.ModifyData(nameList, ipList);
                this.Close();   
            }
        }

        

        private void ListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (selectedIndex != -1)
            {
                nameList[selectedIndex] = TBName.Text;
                ipList[selectedIndex] = TBIp.Text;
                LVNames.Items[selectedIndex] = TBName.Text;
            }
            
            selectedIndex = LVNames.SelectedIndex;
            TBName.Text = nameList[selectedIndex];
            TBIp.Text = ipList[selectedIndex];
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
