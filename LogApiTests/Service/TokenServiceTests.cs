using LogApi.Model;
using LogApi.Service;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace LogApiTests.Service
{
    [TestClass]
    public class TokenServiceTests
    {
        private IOptions<ApiConfigurationModel> config { get; set; }
        private ILogger<TokenService> logger { get; set; }

        public TokenServiceTests()
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

            logger = new Mock<ILogger<TokenService>>().Object;
        }

        [TestMethod]
        public void GenerateTokenTest()
        {
            //Arrange 
            var service = new TokenService(config, logger);

            //Act
            var result =  service.GenerateToken();

            //Assert
            Assert.IsTrue(result.Length > 0);
        }
    }
}
