using Ocelot.Cache;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiGateway.Handlers.Caching
{
    public class OcelotCache : IOcelotCache
    {
        public IOcelotCache<CachedResponse> Cache()
        {
            throw new NotImplementedException();
        }
    }
}
