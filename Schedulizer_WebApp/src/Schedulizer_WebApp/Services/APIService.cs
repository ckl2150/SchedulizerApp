using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Schedulizer_WebApp.Services
{
	public class APIService
    {
        private ILogger<APIService> _logger;

        public APIService(ILogger<APIService> logger)
        {
            _logger = logger;
        }

		// will pass in classnum and attempt to return some result
		public APIServiceResult Lookup(string classnum)
        {

        }
    }

}