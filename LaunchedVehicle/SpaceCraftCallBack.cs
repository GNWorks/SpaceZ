using Communication;
using Communication.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace LaunchedVehicle
{
    [CallbackBehavior(ConcurrencyMode = ConcurrencyMode.Multiple, IncludeExceptionDetailInFaults =true)]
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

        public string GetPayloadTelemetryCommand(PayloadVehicle payloadVehicle)
        {
            throw new NotImplementedException();
        }

        public string GetVehicleTelemetryCommand(LaunchVehicle launchVehicle)
        {
            try
            {
                var orbit = launchVehicle.OrbitRadius;
                var r = new Random();
                var longitude = r.Next(-90, 90);
                var latitude = r.Next(-180, 180);
                var altitude = r.Next(1, launchVehicle.OrbitRadius);
                var temperatureKelvin = r.Next(-1200, -200);

                Dictionary<string, string> randomData = new Dictionary<string, string>();

                randomData.Add("altitude", altitude.ToString());
                randomData.Add("longitude", r.Next(-90, 90).ToString());
                randomData.Add("latitude", r.Next(-180, 180).ToString());
                randomData.Add("temperature", (temperatureKelvin).ToString());

                var entries = randomData.Select(d => string.Format("\"{0}\": {1}\n", d.Key, string.Join(",", d.Value)));

                return "{\n" + string.Join(",", entries);
            }
            catch(Exception exp)
            {
                Console.WriteLine(exp.Message);
                return string.Empty;
            }
        }
    }
}
