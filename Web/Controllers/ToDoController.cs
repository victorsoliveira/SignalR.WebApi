using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using Microsoft.AspNet.SignalR;
using SignalR.WebApi.Binders;
using SignalR.WebApi.Hubs;
using SignalR.WebApi.Model;
using SignalR.WebApi.Utils;

namespace SignalR.WebApi.Controllers
{
    public class ToDoController : ApiController
    {
        private static readonly List<ToDo> Db = new List<ToDo>();
        private IHubContext _hubContext;

        private IHubContext HubContext
        {
            get { return _hubContext ?? (_hubContext = new HubContext<ToDoHub>().Hub); }
        }

        private void AddedDispatcher(TodoModelBinder todo)
        {
            HubContext.Clients.All.Added(todo.Instance);
            HubContext.Clients.Client(todo.ConnectionId).Notification("Item adicionado com sucesso!", "success");
            HubContext.Clients.AllExcept(todo.ConnectionId).Notification("O item '" + todo.Instance.Description + "' foi adicionado à lista", "info");
        }

        private void RemovedDispatcher(ToDo todo)
        {
            HubContext.Clients.All.Removed(todo);
            HubContext.Clients.All.Notification("O item '" + todo.Description + "' foi removido da lista", "warning");
        }

        public IEnumerable<ToDo> Get()
        {
            lock (Db)
            {
                return Db.ToArray();
            }
        }

        public ToDo Get(string id)
        {
            lock (Db)
            {
                return Db.FirstOrDefault(x => x.Id == id);
            }
        }

        public void Post([FromBody] TodoModelBinder todo)
        {
            lock (Db)
            {
                Db.Add(todo.Instance);
                AddedDispatcher(todo);
            }
        }

        public void Put(Guid id, [FromBody]string value)
        {
        }

        public void Delete(string id)
        {
            lock (Db)
            {
                var todo = Db.FirstOrDefault(x => x.Id == id);
                if (todo != null)
                {
                    Db.Remove(todo);
                    RemovedDispatcher(todo);                   
                }
            }
        }

    }
}
