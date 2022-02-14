using Communication.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace Communication
{
    [ServiceContract(CallbackContract = typeof(IClient))]
    public interface ICommunicationService
    {
        [OperationContract]
        void ConnectLaunchVehicle(LaunchVehicle launchVehicle);

        [OperationContract]
        void ConnectPayloadVehicle(PayloadVehicle launchVehicle);
        
        [OperationContract]
        void DisconnectLaunchVehicle(LaunchVehicle launchVehicle);

        [OperationContract]
        void DisconnectPayloadVehicle(PayloadVehicle launchVehicle);

        [OperationContract]
        void SendMessage(string message, string spacecraftName);

        [OperationContract]
        string RequestLaunchVehicleTelemetry(string launchVehicleSpacecraft);

        [OperationContract]
        string RequestPayloadVehicleTelemetry(string payloadVehicle);

        [OperationContract]
        void StopTelemetry(string spacecraft);

        [OperationContract]
        void DeOrbitVehicle(string spacecraft);

        [OperationContract]
        void DeployPayload(LaunchVehicle launchVehicle);


    }
}
