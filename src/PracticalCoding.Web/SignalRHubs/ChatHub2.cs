using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.SignalR;

namespace PracticalCoding.Web.SignalRHubs
{
    public class ChatHub2 : Hub
    {        
        public void Send(ChatMessage chatMsg)
        {
            // Call the broadcastMessage method to update clients 
            // (except the caller client).
            Clients.Others.broadcastMessage(chatMsg);
        }
    }

    public class ChatMessage
    {
        public string Name { get; set; }
        public string Message { get; set; }
        public DateTime SubmitDate { get; set; }
    }
}