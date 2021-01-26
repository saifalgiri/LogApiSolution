using LogApi.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LogApi.IService
{
    public interface ILogService
    {
        Task<List<LogModel>> GetData();
        Task<List<LogModel>> PostData(LogModel logModel);
    }
}
