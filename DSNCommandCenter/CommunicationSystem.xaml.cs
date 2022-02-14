using Communication;
using Communication.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.ServiceModel;
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
using System.Windows.Threading;

namespace DSNCommandCenter
{
    /// <summary>
    /// Interaction logic for CommunicationSystem.xaml
    /// </summary>
    public partial class CommunicationSystem : UserControl
    {
        public static ICommunicationService server;
        private static DuplexChannelFactory<ICommunicationService> _channelFactory;
        public DispatcherTimer SpaceCraftVehicleDt = new DispatcherTimer();
        public DispatcherTimer PayloadVehicleDt = new DispatcherTimer();
        public DispatcherTimer ScientificDataDt = new DispatcherTimer();
        public DispatcherTimer CommunicationDataDt = new DispatcherTimer();
        public DispatcherTimer SpyDataDt = new DispatcherTimer();

        public int timeToReach = 0;

        public CommunicationSystem()
        {
            InitializeComponent();
            _channelFactory = new DuplexChannelFactory<ICommunicationService>(new SpaceCraftCallBack(), "CommunicationServiceEndpoint");
            server = _channelFactory.CreateChannel();
            activeVehiclesDropDown.Items.Add("---Select---");
            activePayloadVehiclesDropDown.Items.Add("---Select---");
            foreach (var item in MainWindow.LaunchedSpaceCrafts)
            {
                activeVehiclesDropDown.Items.Add(item);
            }
        }

        private void requestTelemetry_ButtonClick(object sender, RoutedEventArgs e)
        {
            try
            {
                telemetryInfoBox.Text = "";
                
                if (activeVehiclesDropDown.SelectedItem == null || string.IsNullOrEmpty(activeVehiclesDropDown.SelectedItem.ToString()) || activeVehiclesDropDown.SelectedItem.ToString() == "---Select---")
                    throw new Exception("Please select a valid Launch Vehicle");

                var selectedSpaceCraft = activeVehiclesDropDown.SelectedItem.ToString();

                var selectedLaunchVehicle = MainWindow.SpaceCraftCollection.ContainsKey(selectedSpaceCraft) ?
                                             MainWindow.SpaceCraftCollection[selectedSpaceCraft] :
                                             null;
                timeToReach = (selectedLaunchVehicle.OrbitRadius / 3600) + 10;
                SpaceCraftVehicleDt.Interval = TimeSpan.FromSeconds(1);
                SpaceCraftVehicleDt.Tick += launchVehicleDtTicker;
                SpaceCraftVehicleDt.Start();


                telemetryInfoBox.Text = "Telemetry Start : " + selectedSpaceCraft;
                stopTelemetry.IsEnabled = true;
            }
            catch(Exception exp)
            {
                MessageBox.Show(exp.Message);
            }    
        }

        private void stopTelemetry_ButtonClick(object sender, RoutedEventArgs e)
        {
            SpaceCraftVehicleDt.Stop();
            telemetryInfoBox.Text = telemetryInfoBox.Text + "\n" + "Telemetry End!!!";
            MessageBox.Show($"Telemetry End");
        }
        private void stopPayloadTelemetry_ButtonClick(object sender, RoutedEventArgs e)
        {
            PayloadVehicleDt.Stop();
            payloadTelemetryInfoBox.Text = payloadTelemetryInfoBox.Text + "\n" + "Telemetry End!!!";
            MessageBox.Show($"Telemetry End");
        }
        private void deployPayload_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var selectedSpaceCraft = activeVehiclesDropDown.SelectedItem.ToString();
                var selectedLaunchVehicle = MainWindow.SpaceCraftCollection.ContainsKey(selectedSpaceCraft) ?
                                            MainWindow.SpaceCraftCollection[selectedSpaceCraft] :
                                            null;

                var payloadVehicleInfo = new PayloadVehicle { };

                using (var r = new StreamReader(selectedLaunchVehicle.PayloadPath))
                {
                    var json = r.ReadToEnd();
                    payloadVehicleInfo  = JsonConvert.DeserializeObject<PayloadVehicle>(json);
                }
                
                if (!MainWindow.ActivePayloadProcess.ContainsKey(payloadVehicleInfo.Name))
                {
                    var p = new Process();
                    p.StartInfo = new ProcessStartInfo("C:\\Users\\nalla\\source\\repos\\SpaceZProject\\Payload\\bin\\Debug\\Payload.exe");
                    p.StartInfo.Arguments = payloadVehicleInfo.Name + " " + payloadVehicleInfo.Type;
                    p.Start();
                    MainWindow.ActivePayloadProcess.Add(payloadVehicleInfo.Name, p);
                    activePayloadVehiclesDropDown.Items.Add(payloadVehicleInfo.Name);
                    MainWindow.PayloadCollection.Add(payloadVehicleInfo.Name, payloadVehicleInfo);
                }
                else
                {
                    MessageBox.Show($"Payload Vehicle {payloadVehicleInfo.Name} is already Launched");
                }

            }
            catch(Exception exp)
            {
                MessageBox.Show(exp.Message);
            }
        }

        private void requestPayloadTelemetry_ButtonClick(object sender, RoutedEventArgs e)
        {
            try
            {
                payloadTelemetryInfoBox.Text = "";
               
                if (activePayloadVehiclesDropDown.SelectedItem == null || string.IsNullOrEmpty(activePayloadVehiclesDropDown.SelectedItem.ToString()) || activePayloadVehiclesDropDown.SelectedItem.ToString() == "---Select---")
                    throw new Exception("Please select a valid payload vehicle");

                var selectedPayload = activePayloadVehiclesDropDown.SelectedItem.ToString();
                

                var selectedPayloadVehicle = MainWindow.PayloadCollection.ContainsKey(selectedPayload) ?
                                            MainWindow.PayloadCollection[selectedPayload] :
                                            null;

                PayloadVehicleDt.Interval = TimeSpan.FromSeconds(3);
                PayloadVehicleDt.Tick += payloadVehicleDtTicker;
                PayloadVehicleDt.Start();


                payloadTelemetryInfoBox.Text = "Telemetry Start : " + selectedPayload;
                stopPayloadTelemetry.IsEnabled = true;
            }
            catch (Exception exp)
            {
                MessageBox.Show(exp.Message);
            }
          
        }

        private void requestPayloadData_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (activePayloadVehiclesDropDown.SelectedItem == null || string.IsNullOrEmpty(activePayloadVehiclesDropDown.SelectedItem.ToString()) || activePayloadVehiclesDropDown.SelectedItem.ToString() == "---Select---")
                    throw new Exception("Please select a valid payload vehicle");

                var selectedPayload = activePayloadVehiclesDropDown.SelectedItem.ToString();        

                var selectedPayloadVehicle = MainWindow.PayloadCollection.ContainsKey(selectedPayload) ?
                                            MainWindow.PayloadCollection[selectedPayload] :
                                            null;
                payloadDataInfoBox.Text = "";
                stopPayloadData.IsEnabled = true;

                switch (selectedPayloadVehicle.Type)
                {
                    case "Scientific":
                        ScientificDataDt.Interval = TimeSpan.FromSeconds(60);
                        ScientificDataDt.Tick += ScientificDataDtTicker;
                        ScientificDataDt.Start();
                        payloadDataInfoBox.Text = "Weather Data : " + selectedPayload;
                        break;

                    case "Communication":
                        CommunicationDataDt.Interval = TimeSpan.FromSeconds(5);
                        CommunicationDataDt.Tick += CommunicationDataDtTicker;
                        CommunicationDataDt.Start();
                        payloadDataInfoBox.Text = "Communication Data : " + selectedPayload;
                        break;

                    case "Spy":
                        SpyDataDt.Interval = TimeSpan.FromSeconds(10);
                        SpyDataDt.Tick += SpyDataDtTicker;
                        SpyDataDt.Start();
                        payloadDataInfoBox.Text = "Spy Data : " + selectedPayload;
                        break;

                    default:
                        payloadDataInfoBox.Text = "";
                        break;
                }
            }
            catch (Exception exp)
            {
                MessageBox.Show(exp.Message);
            }                 
        }

        private void stopPayloadData_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var selectedPayload = activePayloadVehiclesDropDown.SelectedItem.ToString();
                var selectedPayloadVehicle = MainWindow.PayloadCollection.ContainsKey(selectedPayload) ?
                                            MainWindow.PayloadCollection[selectedPayload] :
                                            null;

                switch (selectedPayloadVehicle.Type)
                {
                    case "Scientific":
                        ScientificDataDt.Stop();
                        break;

                    case "Communication":
                        CommunicationDataDt.Stop();
                        break;

                    case "Spy":
                        SpyDataDt.Stop();
                        break;
                    default:
                        payloadDataInfoBox.Text = "";
                        break;
                }
                MessageBox.Show($"Payload - {selectedPayload} - {selectedPayloadVehicle.Type} data has stopped");
            }
            catch (Exception exp)
            {
                MessageBox.Show(exp.Message);
            }
        }

        private void decommisionPayload_Click(object sender, RoutedEventArgs e)
        {
            if (activePayloadVehiclesDropDown.SelectedItem == null || string.IsNullOrEmpty(activePayloadVehiclesDropDown.SelectedItem.ToString()) || activePayloadVehiclesDropDown.SelectedItem.ToString() == "---Select---")
                throw new Exception("Please select a valid payload vehicle");

            var selectedPayloadVehicle = activePayloadVehiclesDropDown.SelectedItem.ToString();
            
            var process = MainWindow.ActivePayloadProcess.ContainsKey(selectedPayloadVehicle) ?
                          MainWindow.ActivePayloadProcess[selectedPayloadVehicle] :
                          null;
            if (process != null)
            {

                activePayloadVehiclesDropDown.Items.Remove(selectedPayloadVehicle);
                process.Kill();
                MessageBox.Show($"Payload Vehicle - {selectedPayloadVehicle} has ended it's mission");
            }
        }
      
        private void launchVehicleDtTicker(object sender, EventArgs e)
        {
            try
            {
                var selectedSpaceCraft = activeVehiclesDropDown.SelectedItem.ToString();
                var data = server.RequestLaunchVehicleTelemetry(selectedSpaceCraft);
                data = data + string.Format(",\"timeToOrbit\": {0}\n", timeToReach--) + "}";

                telemetryInfoBox.Text += selectedSpaceCraft + " flying.... :\n" + data;
                telemetryInfoBox.Text += "\n_______________\n";

                if (timeToReach <= 0)
                {
                    telemetryInfoBox.Text += "\n=[ORBIT REACHED FOR : " + selectedSpaceCraft + "]=";
                    SpaceCraftVehicleDt.Stop();
                    deployPayload.IsEnabled = true;
                    MessageBox.Show($"SpaceCraft - {selectedSpaceCraft} has reached it's Orbit");
                }

                telemetryInfoBox.ScrollToEnd();
            }
            catch (Exception exp)
            {
                MessageBox.Show(exp.Message);
            }
        }

        private void payloadVehicleDtTicker(object sender, EventArgs e)
        {
            var selectedPayloadVehicle = activePayloadVehiclesDropDown.SelectedItem.ToString();
            var data = server.RequestPayloadVehicleTelemetry(selectedPayloadVehicle);

            payloadTelemetryInfoBox.Text += selectedPayloadVehicle + "....\n" + data +"}";
            payloadTelemetryInfoBox.Text += "\n_______________\n";
            payloadTelemetryInfoBox.ScrollToEnd();
        }

        private void ScientificDataDtTicker(object sender, EventArgs e)
        {
            var selectedPayloadVehicle = activePayloadVehiclesDropDown.SelectedItem.ToString();
            var r = new Random();
            var rain = r.Next(0, 100);
            var humidity = r.Next(0, 100);
            var snow = r.Next(0, 100);

            var randomData = new Dictionary<string, string>();

            randomData.Add("rain", rain.ToString() +"mm");
            randomData.Add("humidity", humidity.ToString() + "%");
            randomData.Add("snow", snow.ToString() + "in");

            var entries = randomData.Select(d => string.Format("\"{0}\": {1}\n", d.Key, string.Join(",", d.Value)));

            var data = "{\n" + string.Join(",", entries);

            payloadDataInfoBox.Text += selectedPayloadVehicle + "....\n" + data +"}";
            payloadDataInfoBox.Text += "\n_______________\n";
            payloadDataInfoBox.ScrollToEnd();
        }

        private void CommunicationDataDtTicker(object sender, EventArgs e)
        {
            var selectedPayloadVehicle = activePayloadVehiclesDropDown.SelectedItem.ToString();
            var r = new Random();

            var uplink = r.Next(0, 100);
            var downlink = r.Next(0, 100);
            var activeTransponders = r.Next(20, 120);
            var randomData = new Dictionary<string, string>();

            randomData.Add("UpLink", uplink.ToString() +"MBps");
            randomData.Add("DownLink", downlink.ToString() +"MBps");
            randomData.Add("ActiveTransponders", activeTransponders.ToString());

            var entries = randomData.Select(d => string.Format("\"{0}\": {1}\n", d.Key, string.Join(",", d.Value)));

            var data = "{\n" + string.Join(",", entries);

            payloadDataInfoBox.Text += selectedPayloadVehicle + "....\n" + data +"}";
            payloadDataInfoBox.Text += "\n_______________\n";
            payloadDataInfoBox.ScrollToEnd();
        }

        private void SpyDataDtTicker(object sender, EventArgs e)
        {
            var selectedPayloadVehicle = activePayloadVehiclesDropDown.SelectedItem.ToString();
            var r = new Random();
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            var incomingMessage = new string(Enumerable.Repeat(chars, 18)
                                        .Select(s => s[r.Next(s.Length)]).ToArray());
            var outgoingMessage = new string(Enumerable.Repeat(chars, 18)
                                        .Select(s => s[r.Next(s.Length)]).ToArray());

            var randomData = new Dictionary<string, string>();

            randomData.Add("IncomingMessage", incomingMessage);
            randomData.Add("OutgoingMessage", outgoingMessage);

            var entries = randomData.Select(d => string.Format("\"{0}\": {1}\n", d.Key, string.Join(",", d.Value)));

            var data = "{\n" + string.Join(",", entries);

            payloadDataInfoBox.Text += selectedPayloadVehicle + "....\n" + data + "}";
            payloadDataInfoBox.Text += "\n_______________\n";
            payloadDataInfoBox.ScrollToEnd();
        }
    }
}
