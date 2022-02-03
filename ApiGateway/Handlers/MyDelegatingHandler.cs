using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Net.Http;
using System.Threading;

namespace ApiGateway.Handlers
{
    public class MyDelegatingHandler : DelegatingHandler
    {
        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {

            // Console.WriteLine(request.ToString());

            // request.RequestUri = new Uri("http://localhost:9000/api/persons/fail-endpoint");

            
            var headerToCheck = "Deneme";

            request.Headers.TryGetValues(headerToCheck, out var value);

            if (value.FirstOrDefault() == "fail")
            {
                request.RequestUri = new Uri("http://localhost:9000/api/persons/fail-endpoint");
            }
            else
            {
                request.RequestUri = new Uri("http://localhost:9000/api/persons/getbyid?id=3");
            }
            

            var response = await base.SendAsync(request, cancellationToken);

            //response değişiklikleri

            return response;
        }
    }
}
