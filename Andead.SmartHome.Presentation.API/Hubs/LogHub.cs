using Microsoft.AspNetCore.SignalR;
using System;
using System.Threading.Tasks;

namespace Andead.SmartHome.Presentation.API.Hubs
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
