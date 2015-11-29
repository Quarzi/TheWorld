using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TheWorld.Services
{
    public class CoordService
    {
        private ILogger<CoordService> _logger;

        public CoordService(ILogger<CoordService> logger)
        {
            _logger = logger;
        }

        public CoordServiceResult Lookup(string location)
        {
            return new CoordServiceResult()
            {
                Success = true,
                Message = "Success",
                Latitude = 100.00000,
                Longitude = 100.0000
            };
        }
    }
}
