using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace LogApi.Model
{
    public class LogModel
    {
        [JsonProperty("id")]
        public string Id { get; set; }
        [JsonProperty("Summary")]
        [Required]
        public string Title { get; set; }
        [JsonProperty("Message")]
        [Required]
        public string Text { get; set; }
        [JsonProperty("receivedAt")]
        public DateTime ReceivedAt { get; set; }
    }

}
