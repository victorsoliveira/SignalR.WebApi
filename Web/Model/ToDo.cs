using System;
using System.Runtime.Serialization;
using Newtonsoft.Json;

namespace SignalR.WebApi.Model
{
    [Serializable]
    [DataContract]
    public class ToDo
    {
        [DataMember]
        [JsonProperty("id")]
        public string Id { get; set; }

        [DataMember]
        [JsonProperty("description")]
        public string Description { get; set; }
    }
}