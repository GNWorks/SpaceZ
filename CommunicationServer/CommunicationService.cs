using Communication;
using Communication.Models;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace CommunicationServer
{
    [ServiceBehavior(ConcurrencyMode = ConcurrencyMode.Multiple, InstanceContextMode = InstanceContextMode.Single)]
    public class CommunicationService : ICommunicationService
    {
        public static ConcurrentDictionary<string, ConnectedLaunchVehicleClient> _connectedSpacecrafts =
                                        new ConcurrentDictionary<string, ConnectedLaunchVehicleClient>();
        public static ConcurrentDictionary<string, ConnectedPayloadClient> _connectedPayloads =
                                        new ConcurrentDictionary<string, ConnectedPayloadClient>();
        public void ConnectLaunchVehicle(LaunchVehicle launchVehicle)
        {
            var establishedSpacecraftConnection = OperationContext.Current.GetCallbackChannel<IClient>();
            var newSpacecraft = new ConnectedLaunchVehicleClient()
            {
                Connection = establishedSpacecraftConnection,
                LaunchVehicle = launchVehicle
            };
            _connectedSpacecrafts.TryAdd(newSpacecraft.LaunchVehicle.Name, newSpacecraft);
        }

        public void ConnectPayloadVehicle(PayloadVehicle payloadVehicle)
        {
            var establishedSpacecraftConnection = OperationContext.Current.GetCallbackChannel<IClient>();
            var newPayload = new ConnectedPayloadClient()
            {
                Connection = establishedSpacecraftConnection,
                PayloadVehicle = payloadVehicle
            };
            _connectedPayloads.TryAdd(newPayload.PayloadVehicle.Name, newPayload);
        }

        public void DeOrbitVehicle(string spacecraft)
        {
            throw new NotImplementedException();
        }

        public void DeployPayload(LaunchVehicle launchVehicle)
        {
            throw new NotImplementedException();
        }

        public void DisconnectLaunchVehicle(LaunchVehicle launchVehicle)
        {
            ConnectedLaunchVehicleClient removedSpaceCraft;
            var establishedSpacecraftConnection = OperationContext.Current.GetCallbackChannel<IClient>();

            var connectedClient = _connectedSpacecrafts.Values.FirstOrDefault(x => x.Connection == establishedSpacecraftConnection).LaunchVehicle;

            _connectedSpacecrafts.TryRemove(connectedClient.Name, out removedSpaceCraft);

        }

        public void DisconnectPayloadVehicle(PayloadVehicle launchVehicle)
        {
            ConnectedPayloadClient removedSpaceCraft;
            var establishedSpacecraftConnection = OperationContext.Current.GetCallbackChannel<IClient>();

            var connectedClient = _connectedPayloads.Values.FirstOrDefault(x => x.Connection == establishedSpacecraftConnection).PayloadVehicle;

            _connectedPayloads.TryRemove(connectedClient.Name, out removedSpaceCraft);
        }

        public string RequestLaunchVehicleTelemetry(string spacecraft)
        {
            var connectedLaunchVehicleClient = _connectedSpacecrafts.ContainsKey(spacecraft) ? _connectedSpacecrafts[spacecraft] : null;
            if (connectedLaunchVehicleClient == null)
                return string.Empty;

            var telemetryInfo = connectedLaunchVehicleClient.Connection.GetVehicleTelemetryCommand(connectedLaunchVehicleClient.LaunchVehicle);

            return telemetryInfo;
        }

        public string RequestPayloadVehicleTelemetry(string payloadVehicle)
        {
            var connectedPayloadClient = _connectedPayloads.ContainsKey(payloadVehicle) ? _connectedPayloads[payloadVehicle] : null;
            if (connectedPayloadClient == null)
                return string.Empty;

            var telemetryInfo = connectedPayloadClient.Connection.GetPayloadTelemetryCommand(connectedPayloadClient.PayloadVehicle);

            return telemetryInfo;
        }

        public void SendMessage(string message, string spacecraftName)
        {
            throw new NotImplementedException();
        }

        public void StopTelemetry(string spacecraft)
        {
            throw new NotImplementedException();
        }
    }
}
