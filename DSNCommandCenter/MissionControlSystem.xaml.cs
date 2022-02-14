using Communication.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
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

namespace DSNCommandCenter
{
    /// <summary>
    /// Interaction logic for MissionControlSystem.xaml
    /// </summary>
    public partial class MissionControlSystem : UserControl
    {
        public MissionControlSystem()
        {
            InitializeComponent();
            payloadTypeDropDown.Items.Add("---Select---");
            unlaunchedVehiclesDropDown.Items.Add("---Select---");
            payloadTypeDropDown.Items.Add("Scientific");
            payloadTypeDropDown.Items.Add("Communication");
            payloadTypeDropDown.Items.Add("Spy");
        }

        private void launchSpacecraft_ButtonClick(object sender, RoutedEventArgs e)
        {
            try
            {
                if (unlaunchedVehiclesDropDown.SelectedItem == null || string.IsNullOrEmpty(unlaunchedVehiclesDropDown.SelectedItem.ToString()) || unlaunchedVehiclesDropDown.SelectedItem.ToString() == "---Select---")
                    throw new Exception("A vehicle needs to be selected to launch.");

                var spacecraftToBeLaunched = unlaunchedVehiclesDropDown.SelectedItem.ToString();

                if (!MainWindow.ActiveSpacecraftProcess.ContainsKey(spacecraftToBeLaunched))
                {
                    var spaceCraftRadius = MainWindow.SpaceCraftCollection[spacecraftToBeLaunched].OrbitRadius.ToString();
                    var p = new Process();
                    p.StartInfo = new ProcessStartInfo("C:\\Users\\nalla\\source\\repos\\SpaceZProject\\LaunchedVehicle\\bin\\Debug\\LaunchedVehicle.exe");
                    //C:\Users\nalla\source\repos\SpaceZProject\LaunchedVehicle\bin\Debug
                    p.StartInfo.Arguments = spacecraftToBeLaunched + " " + spaceCraftRadius;
                    p.Start();

                    MainWindow.ActiveSpacecraftProcess.Add(spacecraftToBeLaunched, p);
                    MainWindow.LaunchedSpaceCrafts.Add(spacecraftToBeLaunched);
                    if (MainWindow.UnlaunchedSpaceCrafts.Contains(spacecraftToBeLaunched))
                        MainWindow.UnlaunchedSpaceCrafts.Remove(spacecraftToBeLaunched);

                    launchedSpaceCrafts_ListBox.Items.Add(spacecraftToBeLaunched);
                    unlaunchedSpaceCrafts_ListBox.Items.Remove(spacecraftToBeLaunched);
                    unlaunchedVehiclesDropDown.SelectedItem = null;
                }
                else
                {
                    MessageBox.Show($"SpaceCraft {spacecraftToBeLaunched} is already Launched");
                }
       
            }
            catch (Exception exp)
            {
                MessageBox.Show(exp.Message);
                return;
            }
        }

        private void configureSpaceCraft_ButtonClick(object sender, RoutedEventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(launchVehicleName.Text))
                    throw new Exception("Launch Vehicle Name is Required");
                if (string.IsNullOrEmpty(launchVehicleOrbitRadius.Text))
                    throw new Exception("Launch Vehicle Orbit Radius is Required");
                if (!int.TryParse(launchVehicleOrbitRadius.Text, out int orbitRadius))
                    throw new Exception("Launch Vehicle Orbit Radius must be numeric value");
                if (string.IsNullOrEmpty(launchVehiclePayloadFilePath.Text))
                    throw new Exception("Launch Vehicle should have Payload file path selected");

                var launchVehicle = new LaunchVehicle
                {
                    Name = launchVehicleName.Text,
                    OrbitRadius = orbitRadius,
                    PayloadPath = launchVehiclePayloadFilePath.Text
                };

                var dir = @"C:\Users\nalla\source\repos\SpaceZProject\DSNCommandCenter\ConfigFiles\LaunchVehicleConfigs";  // folder location

                if (!Directory.Exists(dir))
                    Directory.CreateDirectory(dir);

                File.WriteAllText(Path.Combine(dir, $"{launchVehicle.Name + "_" + launchVehicle.OrbitRadius}.json"), JsonConvert.SerializeObject(launchVehicle));

                unlaunchedSpaceCrafts_ListBox.Items.Add(launchVehicleName.Text);
                unlaunchedVehiclesDropDown.Items.Add(launchVehicleName.Text);
                MainWindow.SpaceCraftCollection.Add(launchVehicleName.Text, launchVehicle);
                launchVehicleName.Text = "";
                launchVehicleOrbitRadius.Text = "";
                launchVehiclePayloadFilePath.Text = "";
                MessageBox.Show($"Launch Vehicle {launchVehicle.Name} is ready to be launched !!");
            }
            catch (Exception exp)
            {
                MessageBox.Show(exp.Message);
                return;
            }
        }

        private void ShowActiveSpaceCraftInfo_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var selectedItem = launchedSpaceCrafts_ListBox.SelectedItem.ToString();

                if (string.IsNullOrEmpty(selectedItem))
                    throw new Exception($"Please select a vehicle to see the information");

                if (unlaunchedSpaceCrafts_ListBox.Items.Contains(selectedItem))
                    throw new Exception($"SpaceCraft {selectedItem} is not launched yet. Please select Launched/Active SpaceCraft to view info");

                var spaceCraft = MainWindow.SpaceCraftCollection[selectedItem];
               
                var payloadVehicleInfo = new PayloadVehicle { };

                using (var r = new StreamReader(spaceCraft.PayloadPath))
                {
                    var json = r.ReadToEnd();
                    payloadVehicleInfo = JsonConvert.DeserializeObject<PayloadVehicle>(json);
                }

                var spaceCraftInfo = "Name : " + spaceCraft.Name + "\n" +
                                     "Orbit : " + spaceCraft.OrbitRadius + "\n" +
                                     "Payload Name : " + payloadVehicleInfo.Name + "\n" +
                                     "Payload Type : " + payloadVehicleInfo.Type + "\n" +
                                     "Payload Path : " + spaceCraft.PayloadPath;

                spaceCraftInfoTxt.Text = spaceCraftInfo;
            }
            catch (Exception exp)
            {
                MessageBox.Show(exp.Message);
            }
        }

        private void configurePayload_ButtonClick(object sender, RoutedEventArgs e)
        {
            try
            {
                if (payloadName.Text == null || string.IsNullOrEmpty(payloadName.Text))
                    throw new Exception("Payload Name is Required");
                if (payloadTypeDropDown.SelectedItem == null || string.IsNullOrEmpty(payloadTypeDropDown.SelectedItem.ToString()) || payloadTypeDropDown.SelectedItem.ToString() == "---Select---")
                    throw new Exception("Payload Type is Required");

                var payLoad = new PayloadVehicle
                {
                    Name = payloadName.Text,
                    Type = payloadTypeDropDown.SelectedItem.ToString()
                };

                var dir = @"C:\Users\nalla\source\repos\SpaceZProject\DSNCommandCenter\ConfigFiles\PayloadConfigs";  // folder location

                if (!Directory.Exists(dir))
                    Directory.CreateDirectory(dir);

                File.WriteAllText(Path.Combine(dir, $"{payLoad.Name + "_" + payLoad.Type}.json"), JsonConvert.SerializeObject(payLoad));

                payloadName.Text = "";
                payloadTypeDropDown.SelectedItem = null;

                MessageBox.Show($"Payload - {payLoad.Name} of type - {payLoad.Type} configured successfully!");
            }
            catch (Exception exp)
            {
                MessageBox.Show(exp.Message);
            }
        }

        private void choosePayloadFilePath_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Microsoft.Win32.OpenFileDialog dlg = new Microsoft.Win32.OpenFileDialog();

                dlg.DefaultExt = ".json";
                dlg.Filter = "Json files (*.json)|*.json";
                Nullable<bool> result = dlg.ShowDialog();

                // Get the selected file name and display in a TextBox 
                if (result == true)
                {
                    // Open document 
                    string filename = dlg.FileName;
                    launchVehiclePayloadFilePath.Text = filename;
                }

            }
            catch (Exception exp)
            {
                MessageBox.Show(exp.Message);
            }
        }
    }
}
