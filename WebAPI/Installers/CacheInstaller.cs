using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Core.Utilities.Cache;

namespace WebAPI.Installers
{
    public class CacheInstaller : IInstaller
    {
        private RedisCacheSettings _redisCacheSettings;
        public void InstallServices(IServiceCollection services, IConfiguration configuration)
        {
            var redisCacheSettings = new RedisCacheSettings();
            _redisCacheSettings = configuration.GetSection("RedisCacheSettings").Get<RedisCacheSettings>();
            services.AddSingleton(_redisCacheSettings);

            if (!_redisCacheSettings.Enabled)
            {
                return;
            }

            services.AddStackExchangeRedisCache(options => options.Configuration = redisCacheSettings.ConnectionString);
            services.AddSingleton<IResponseCacheService, ResponseCacheService>();
        }
    }
}
