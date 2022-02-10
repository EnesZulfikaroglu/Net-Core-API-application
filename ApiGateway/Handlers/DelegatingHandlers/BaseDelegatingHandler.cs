using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Net.Http;
using System.Threading;
using Serilog.Core;
using System.Text;
using System.Net;

namespace ApiGateway.Handlers.DelegatingHandlers
{
    public class BaseDelegatingHandler : DelegatingHandler
    {
        private readonly Logger _logger;
        public BaseDelegatingHandler(Logger logger)
        {
            _logger = logger;
        }
        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            // Changes to the request should be added here 

            var response = await base.SendAsync(request, cancellationToken);

            // Changes to the Response should be added here

            var logBuilder = new StringBuilder();
            logBuilder.Append(" - Response Status Code: ");
            logBuilder.Append(response.StatusCode.ToString());
            logBuilder.Append(" - Response Success Status Code: ");
            logBuilder.Append(response.IsSuccessStatusCode.ToString());

            if (response.IsSuccessStatusCode)
                _logger.Information(logBuilder.ToString());
            
            else
                _logger.Error(" - Request: " + request.RequestUri.ToString() + logBuilder.ToString());
            
            return response;
        }
    }
}
