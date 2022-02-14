using Communication.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace Communication
{
    public interface IClient
    {
        [OperationContract]
        void GetMessage(string message, string spacecraftname);

        [OperationContract]
        void GetDeployPayloadCommand(LaunchVehicle launchVehicle);

        [OperationContract]
        string GetVehicleTelemetryCommand(LaunchVehicle launchVehicle);

        [OperationContract]
        string GetPayloadTelemetryCommand(PayloadVehicle payloadVehicle);

        [OperationContract]
        void GetDeOrbitCommand(LaunchVehicle launchVehicle);
    }
}
