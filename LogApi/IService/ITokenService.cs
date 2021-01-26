using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LogApi.IService
{
    public interface ITokenService
    {
        string GenerateToken();
    }
}
