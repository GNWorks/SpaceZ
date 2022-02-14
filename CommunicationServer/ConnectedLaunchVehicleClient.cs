using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Communication;
using Communication.Models;

namespace CommunicationServer
{
    public class ConnectedLaunchVehicleClient
    {
        public  IClient Connection;
        public LaunchVehicle LaunchVehicle { get; set;}
    }
}
