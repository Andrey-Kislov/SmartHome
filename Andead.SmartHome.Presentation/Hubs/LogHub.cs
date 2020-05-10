using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;

namespace Andead.SmartHome.Presentation.Hubs
{
    public interface ILogHub
    {
        Task NewEvent(DateTimeOffset dateTimeOffset, string message);
    }

    public class LogHub : Hub<ILogHub>
    {
        public async Task SendNewEvent(DateTimeOffset dateTimeOffset, string message)
        {
            await Clients.All.NewEvent(dateTimeOffset, message);
        }
    }
}
