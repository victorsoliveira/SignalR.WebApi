using Microsoft.AspNet.SignalR;
using Microsoft.AspNet.SignalR.Hubs;

namespace SignalR.WebApi.Hubs
{
    [HubName("todo")]
    public class ToDoHub : Hub
    {}
}