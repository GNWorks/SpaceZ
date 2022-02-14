using Communication;
using Communication.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
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

namespace DSNCommandCenter
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public static ICommunicationService server;
        public static Dictionary<string, LaunchVehicle> SpaceCraftCollection = new Dictionary<string, LaunchVehicle>();
        public static Dictionary<string, PayloadVehicle> PayloadCollection = new Dictionary<string, PayloadVehicle>();
        public static List<string> LaunchedSpaceCrafts = new List<string>();
        public static List<string> UnlaunchedSpaceCrafts = new List<string>();
        public static Dictionary<string, Process> ActiveSpacecraftProcess = new Dictionary<string, Process>();
        public static Dictionary<string, Process> ActivePayloadProcess = new Dictionary<string, Process>();

        public MainWindow()
        {
            InitializeComponent();
        }

        //Change content usercontrol from sidebar menu
        private void ListViewMenu_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            UserControl usc = null;
            ContentMain.Children.Clear();

            //Change usercontrol 
            switch (((ListViewItem)((ListView)sender).SelectedItem).Name)
            {
                case "Home":
                    usc = new HomeControlClient();
                    ContentMain.Children.Add(usc);
                    break;
                case "MissionControlSystem":
                    usc = new MissionControlSystem();
                    ContentMain.Children.Add(usc);
                    break;
                case "CommunicationSystem":
                    usc = new CommunicationSystem();
                    ContentMain.Children.Add(usc);
                    break;
                default:
                    break;
            }
        }
    }
}
