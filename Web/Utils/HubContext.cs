using System;
using Microsoft.AspNet.SignalR;
using Microsoft.AspNet.SignalR.Hubs;

namespace SignalR.WebApi.Utils
{
    public class HubContext<THub>
        where THub : IHub
    {
        private readonly Lazy<IHubContext> _hub = new Lazy<IHubContext>(() => GlobalHost.ConnectionManager.GetHubContext<THub>());

        public IHubContext Hub
        {
            get { return _hub.Value; }
        }
    }
}