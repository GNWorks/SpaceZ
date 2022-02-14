using Communication;
using Communication.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace DSNCommandCenter
{
    [CallbackBehavior(ConcurrencyMode = ConcurrencyMode.Multiple)]
    public class SpaceCraftCallBack : IClient
    {
        public void GetDeOrbitCommand(LaunchVehicle launchVehicle)
        {
            throw new NotImplementedException();
        }

        public void GetDeployPayloadCommand(LaunchVehicle launchVehicle)
        {
            throw new NotImplementedException();
        }

        public void GetMessage(string message, string spacecraftname)
        {
            throw new NotImplementedException();
        }

        public void GetPayloadTelemetryCommand(LaunchVehicle launchVehicle)
        {
            throw new NotImplementedException();
        }

        public string GetPayloadTelemetryCommand(PayloadVehicle payloadVehicle)
        {
            throw new NotImplementedException();
        }

        public string GetVehicleTelemetryCommand(LaunchVehicle launchVehicle)
        {
            throw new NotImplementedException();
        }
    }
}
