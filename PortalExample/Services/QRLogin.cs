using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;

namespace PortalExample.Services
{
    public class QRLogin : Hub
    {

   
        public override Task OnConnectedAsync()
        {
            Clients.Caller.SendAsync("SendCode", Context.ConnectionId);
            return base.OnConnectedAsync();
        }

        
    }
}
