using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;

namespace Andead.SmartHome.Presentation.Hubs
{
    public interface IDeviceStateHub
    {
        Task NewEvent(DateTimeOffset dateTimeOffset, int deviceId, int attributeId, string value);
    }

    public class DeviceStateHub : Hub<IDeviceStateHub>
    {
        public async Task SendNewEvent(DateTimeOffset dateTimeOffset, int deviceId, int attributeId, string value)
        {
            await Clients.All.NewEvent(dateTimeOffset, deviceId, attributeId, value);
        }
    }
}
