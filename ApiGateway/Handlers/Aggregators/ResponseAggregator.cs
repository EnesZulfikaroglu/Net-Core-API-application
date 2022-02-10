using Microsoft.AspNetCore.Http;
using Ocelot.Middleware;
using Ocelot.Multiplexer;
using Serilog.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace ApiGateway.Handlers.Aggregators
{
    public class ResponseAggregator : IDefinedAggregator
    {
        private readonly Logger _logger;

        public ResponseAggregator(Logger logger)
        {
            _logger = logger;
        }
        public async Task<DownstreamResponse> Aggregate(List<HttpContext> responses)
        {
            var contentBuilder = new StringBuilder();

            foreach (var response in responses)
            {
                try
                {
                    contentBuilder.Append(await response.Items.DownstreamResponse().Content.ReadAsStringAsync());
                    contentBuilder.Append(",");
                }
                catch (Exception ex)
                {
                    _logger.Error(ex.ToString() + " - Error on Downstream Response: " + response.Items.DownstreamRequest().ToString());
                }

            }

            /*
            for (int i = 0; i < responses.Count; i++)
            {
                contentBuilder.Append(await responses[i].Items.DownstreamResponse().Content.ReadAsStringAsync());
                contentBuilder.Append(",");
            }
            */

            var stringContent = new StringContent(EditContent(contentBuilder.ToString()))
            {
                Headers = { ContentType = new MediaTypeHeaderValue("application/json") }
            };

            return new DownstreamResponse(stringContent, HttpStatusCode.OK, new List<KeyValuePair<string, IEnumerable<string>>>(), "OK");
        }

        private string EditContent(string content)
        {
            while(content.EndsWith(","))
            {
                content = content.Remove(content.Length - 1);
            }
            return content;
        }
    }
}
