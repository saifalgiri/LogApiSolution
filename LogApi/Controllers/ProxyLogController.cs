using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LogApi.IService;
using LogApi.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace LogApi.Controllers
{
    [ApiController]
    [Authorize]
    [Route("[controller]")]
    public class ProxyLogController : Controller
    {
        private readonly ILogger<ProxyLogController> logger;
        private readonly ITokenService tokenService;
        private readonly ILogService logService;
        public ProxyLogController(ILogger<ProxyLogController> _logger, 
            ITokenService _tokenService, ILogService _logService)
        {
            logger = _logger;
            tokenService = _tokenService;
            logService = _logService;
        }

        [HttpGet("token")]
        [AllowAnonymous]
        public string GetToken()
        {
            return tokenService.GenerateToken();
        }

        [HttpGet("Getdata")]
        [Authorize]
        public async Task<List<LogModel>> GetData()
        {
            return await logService.GetData();
        }

        [HttpPost("Postdata")]
        [Authorize]
        public async Task<List<LogModel>> PostData(LogModel logModel)
        {
            logModel.Id = Guid.NewGuid().ToString();
            logModel.ReceivedAt = DateTime.UtcNow;
            return await logService.PostData(logModel);
        }
    }
}
