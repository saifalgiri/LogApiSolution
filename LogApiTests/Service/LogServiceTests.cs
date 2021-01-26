using LogApi.Service;
using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using LogApi.IService;
using Moq;
using LogApi.Model;
using Microsoft.Extensions.Options;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

namespace LogApi.Service.Tests
{
    [TestClass]
    public class LogServiceTests
    {
        private IOptions<ApiConfigurationModel> config { get; set; }
        private ILogger<LogService> logger { get; set; }
        public LogServiceTests()
        {
            var values = new ApiConfigurationModel
            {
                ApiKey = "key46INqjpp7lMzjd",
                ApiGetUrl = "https://api.airtable.com/v0/appD1b1YjWoXkUJwR/Messages?maxRecords=3&view=Grid%20view",
                ApiPostUrl = "https://api.airtable.com/v0/appD1b1YjWoXkUJwR/Messages"
            };

            var mockSettings = new Mock<IOptions<ApiConfigurationModel>>();
            mockSettings.Setup(v => v.Value).Returns(values);

            config = mockSettings.Object;

            logger = new Mock<ILogger<LogService>>().Object;
        }

        [TestMethod]
        public async Task GetDataTestAsync()
        {
            //Arrange 
            var service = new LogService(config, logger);

            //Act
            var result = await service.GetData();

            //Assert
            Assert.IsTrue(result.Count > 0);
        }

        [TestMethod]
        public async Task PostDataTestAsync()
        {
            //Arrange 
            var logModel = new LogModel
            {
                Id = "1",
                Title = "tests",
                Text = "test messages",
                ReceivedAt = DateTime.UtcNow
            };

            var service = new LogService(config, logger); 

            //Act
            var result = await service.PostData(logModel);

            //Assert
            Assert.IsTrue(result.Count > 0);
        }
    }
}

