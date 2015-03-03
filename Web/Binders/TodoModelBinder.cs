using System;
using System.Runtime.Serialization;
using Newtonsoft.Json;
using SignalR.WebApi.Model;

namespace SignalR.WebApi.Binders
{
    [Serializable]
    [DataContract]
    public class TodoModelBinder
    {
        [DataMember]
        [JsonProperty("connId")]
        public string ConnectionId { get; set; }

        [DataMember]
        [JsonProperty("instance")]
        public ToDo Instance { get; set; }
    }
}