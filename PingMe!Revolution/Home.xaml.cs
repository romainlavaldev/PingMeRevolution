using LiveCharts.Wpf;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Net.NetworkInformation;
using System.Text;
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
using System.Windows.Threading;
using LiveCharts;
using Microsoft.Win32;

namespace PingMe_Revolution
{
    /// <summary>
    /// Logique d'interaction pour Home.xaml
    /// </summary>
    public partial class Home : UserControl
    {

        bool running = false;
        DispatcherTimer timer = new DispatcherTimer();

        string dataPath = System.Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) +
                                        "\\PingMeData\\data.txt";

        public Home()
        {
            InitializeComponent();
            PopulateIpList();

            timer.Interval = TimeSpan.FromMilliseconds(450);
            timer.IsEnabled = false;
            timer.Tick += TimerOnTick ;

            PingChart.Series = pingSeries;
        }

        public void Refresh()
        {
            PopulateIpList();
            PingChart.Series.Clear();
            PingChartYAxis.MaxValue = 400;
            if(running) BtnStartStop_Click(BtnStartStop, null);
            pingSeries = new SeriesCollection
        {
            new LineSeries(){Title = "Ping", Values = new ChartValues<int>()}
        };
            PingChart.Series = pingSeries;
        }

        private void PopulateIpList()
        {
            String[] datas = File.ReadAllLines(dataPath);
            List<String> names = new List<string>();
            List<String> ips = new List<string>();

            foreach (string data in datas)
            {
                names.Add(data.Split(',', 2)[0]);
                ips.Add(data.Split(',', 2)[1]);
            }

            CbNames.ItemsSource = names;
            CbIPs.ItemsSource = ips;
            CbNames.SelectedIndex = 0;

        }

        private void BtnStartStop_Click(object sender, RoutedEventArgs e)
        {
            if (!running)
            {
                timer.IsEnabled = true;
                StartStopIco.ImageSource = new BitmapImage(new Uri(BaseUriHelper.GetBaseUri(this), "images/started.png"));
                running = true;
            }
            else
            {

                timer.IsEnabled = false;
                StartStopIco.ImageSource = new BitmapImage(new Uri(BaseUriHelper.GetBaseUri(this), "images/stopped.png"));
                running = false;
            }
        }

        SeriesCollection pingSeries = new SeriesCollection
        {
            new LineSeries(){Title = "Ping", Values = new ChartValues<int>()}
        };
        
        private void UpdatePingValues(int value)
        {
            
            if (pingSeries[0].Values.Count >= 15) pingSeries[0].Values.RemoveAt(0);
            if (value > PingChartYAxis.MaxValue) PingChartYAxis.MaxValue = value + value*10/100;
            pingSeries[0].Values.Add(value);
        }


        private void TimerOnTick(object sender, EventArgs e)
        {
            CbIPs.SelectedIndex = CbNames.SelectedIndex;
            string adress = CbIPs.SelectedValue.ToString();
            PingReply reply;
            try
            {
                reply = new Ping().Send(adress);
            }
            catch (Exception ex)
            {
                reply = null;
                Console.WriteLine(ex);
            }

            /*ùtry
            {
                TbPing.Text = reply.RoundtripTime.ToString();
                UpdatePingValues((int)reBtnExport_Click);
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message);
                BtnStartStop_Click(BtnStartStop, null);
                throw;
            }*/
            
            if (reply != null)
            {
                switch (reply.Status)
                {
                    case IPStatus.Success:
                        TbPing.Text = reply.RoundtripTime.ToString();
                        UpdatePingValues((int)reply.RoundtripTime);
                        break;

                    case IPStatus.TimedOut:
                        BtnStartStop_Click(BtnStartStop, null);
                        MessageBox.Show("Le serveur choisi est inaccessible");
                        break;

                    default:
                        BtnStartStop_Click(BtnStartStop, null);
                        MessageBox.Show("Erreur");
                        break;
                }
            }
            else
            {
                BtnStartStop_Click(BtnStartStop, null);
                MessageBox.Show("Erreur");
            }
        }

        private void BtnImport_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog fileDialog = new OpenFileDialog();
            if (fileDialog.ShowDialog() == true)
            {
                if (IsValidDataFile(fileDialog.FileName))
                {
                    File.Copy(fileDialog.FileName, dataPath, true);
                    Refresh();
                }
                else
                {
                    MessageBox.Show("Le fichier choisi n'est pas valide");
                }
            }
        }

        private bool IsValidDataFile(string path)
        {
            String[] datas = File.ReadAllLines(path);
            foreach (string line in datas)
            {
                if ( (line.Contains(",")) && (!String.IsNullOrWhiteSpace(line.Split(",", 2)[0])) && (!String.IsNullOrWhiteSpace(line.Split(",", 2)[1])))
                {
                }
                else
                {
                    return false;
                }
            }

            return true;
        }

        private void BtnExport_Click(object sender, RoutedEventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "txt files (*.txt)|*.txt";
            saveFileDialog.OverwritePrompt = true;

            if (saveFileDialog.ShowDialog() == true)
            {
                SaveDataFile(saveFileDialog.FileName);
            }
        }

        private void SaveDataFile(String path)
        {
            string datas = "";
            for (int i = 0; i < CbNames.Items.Count; i++)
            {
                datas += CbNames.Items[i].ToString();
                datas += ",";
                datas += CbIPs.Items[i].ToString();
                datas += '\n';
            }
            File.WriteAllText(path, datas);
        }

        private void BtnModify_Click(object sender, RoutedEventArgs e)
        {
            ModifyWindow modifyWindow = new ModifyWindow(CbNames.SelectedItem.ToString(), CbIPs.Items[CbNames.SelectedIndex].ToString(),
                this, CbNames.SelectedIndex);
            modifyWindow.Show();
        }

        public void ModifyData(string name, string ip, int index)
        {
            String[] datas = File.ReadAllLines(dataPath);

            datas[index] = name + "," + ip;

            File.WriteAllLines(dataPath, datas);

            Refresh();
        }
    }
}
