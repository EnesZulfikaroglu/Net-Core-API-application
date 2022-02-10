using Ocelot.Cache;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiGateway.Handlers.Caching
{
    public interface IOcelotCache
    {
        IOcelotCache<CachedResponse> Cache();
    }
}
