using Communication;
using System;
using System.Collections.Generic;
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

namespace Payload
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public static ICommunicationService server;
        private static DuplexChannelFactory<ICommunicationService> _channelFactory;

        public MainWindow()
        {
            try
            {
                InitializeComponent();
                _channelFactory = new DuplexChannelFactory<ICommunicationService>(new SpaceCraftCallBack(), "CommunicationServiceEndpoint");
                server = _channelFactory.CreateChannel();
            }
            catch (Exception exp)
            {
                MessageBox.Show(exp.Message + exp.InnerException.Message);
            }
        }
        public void SetArguments(string payloadvehicleName, string payloadvehicleType)
        {
            try
            {
                Title = Title +"-"+ payloadvehicleName;
                payloadVehicleName.Content = payloadvehicleName;
                payloadVehicleType.Content = payloadvehicleType;

                server.ConnectPayloadVehicle(new Communication.Models.PayloadVehicle
                {
                    Name = payloadvehicleName,
                    Type = payloadvehicleType
                });

            }
            catch (Exception exp)
            {
                MessageBox.Show(exp.Message);
            }
        }
        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            server.DisconnectPayloadVehicle(new Communication.Models.PayloadVehicle
            {
                Name = payloadVehicleName.Content.ToString()
            });
        }
    }
}
