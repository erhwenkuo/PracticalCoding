using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.SignalR;
using PracticalCoding.Web.Models.SignalRHub;

namespace PracticalCoding.Web.SignalRHubs
{
    public class EventBusHub : Hub
    {
        public void BroadcastAll(SignalREvent signalREvent)
        {
            Clients.All.broadcastSignalrEvent(signalREvent);
        }
    }
}


