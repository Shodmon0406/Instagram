using Microsoft.AspNetCore.SignalR;

namespace WebApi.Hubs;

public class ChatHub : Hub
{
    public async Task Send(string userName, string message)
    {
        await Clients.All.SendAsync("Receive", userName, message);
    }
}