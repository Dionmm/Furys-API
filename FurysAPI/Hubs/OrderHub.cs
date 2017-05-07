using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.SignalR;

namespace FurysAPI.Hubs
{
    public class OrderHub : Hub
    {
        public void Hello()
        {
            Clients.All.hello("hello");
        }
    }
}