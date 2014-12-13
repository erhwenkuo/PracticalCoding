using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PracticalCoding.Web.Models.SignalRHub
{
    public class SignalREvent
    {
        public SignalREvent()
        {
            EventDate = DateTime.Now;
        }

        public SignalREvent(string eventName, object eventBody)
            : this()
        {
            this.EventName = eventName;
            this.EventBody = eventBody;
        }

        public SignalREvent(string eventName, object eventBody, string eventSource)
            : this(eventName, eventBody)
        {
            this.EventSource = eventSource;
        }

        public string EventName { get; set; }

        public string EventSource { get; set; }

        public object EventBody { get; set; }

        public DateTime EventDate { get; set; }
    }
}

