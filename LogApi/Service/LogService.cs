using LogApi.IService;
using LogApi.Model;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace LogApi.Service
{
    public class LogService : ILogService
    {
        private  ApiConfigurationModel apiConfigurationModel { get; set; }
        private readonly ILogger<LogService> logger;
        public LogService(IOptions<ApiConfigurationModel> option, ILogger<LogService> _logger)
        {
            apiConfigurationModel = option.Value;
            logger = _logger;
        }

        public async Task<List<LogModel>> GetData()
        {
            List<LogModel> logModel = new List<LogModel>();
            try
            {

                using (var client = new HttpClient())
                {
                    client.DefaultRequestHeaders.Add("Authorization", "Bearer " + apiConfigurationModel.ApiKey);
                    var response = await client.GetAsync(apiConfigurationModel.ApiGetUrl);
                    if (response.IsSuccessStatusCode)
                    {
                        var stringResonse = await response.Content.ReadAsStringAsync();
                        var jObject = JObject.Parse(stringResonse);
                        foreach(var message in jObject["records"])
                        {
                            logModel.Add(JsonConvert.DeserializeObject<LogModel>(message["fields"].ToString()));
                        }
                    }
                }
            }
            catch(Exception ex)
            {
                logger.LogError($"Failed to get data : {ex.Message} ");
            }

            return logModel;
        }
        public async Task<List<LogModel>> PostData(LogModel logModel)
        {
            List<LogModel> responseMessage = new List<LogModel>();
            var messages = new
            {
                records = new[] { new { fields = logModel } }
            };

            try
            {
                using (var client = new HttpClient())
                {
                    client.DefaultRequestHeaders.Add("Authorization", "Bearer " + apiConfigurationModel.ApiKey);
                    var json = JsonConvert.SerializeObject(messages);
                    var data = new StringContent(json, Encoding.UTF8, "application/json");
                    var response = await client.PostAsync(apiConfigurationModel.ApiPostUrl, data);
                    if (response.IsSuccessStatusCode)
                    {
                        var stringResonse = await response.Content.ReadAsStringAsync();
                        var jObject = JObject.Parse(stringResonse);
                        foreach (var message in jObject["records"])
                        {
                            responseMessage.Add(JsonConvert.DeserializeObject<LogModel>(message["fields"].ToString()));
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                logger.LogError($"Failed to post data :  {ex.Message} ");
            }

            return responseMessage;
        }
    }
}
