using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LogApi.Model
{
    public class ApiConfigurationModel
    {
        public string SystemKey { get; set; }
        public string ApiKey { get; set; }
        public string ApiGetUrl { get; set; }
        public string ApiPostUrl { get; set; }
    }
}
