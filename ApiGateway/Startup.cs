using ApiGateway.Handlers.DelegatingHandlers;
using ApiGateway.Handlers.Aggregators;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using Ocelot.DependencyInjection;
using Ocelot.Middleware;
using Ocelot.Provider.Consul;
using Ocelot.Cache.CacheManager;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Serilog;
using Serilog.Exceptions;
using Serilog.Formatting.Elasticsearch;
using Serilog.Sinks.Elasticsearch;
using Core.Utilities.Security.Jwt;
using Core.Utilities.Security.Encryption;

namespace ApiGateway
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();

            // Authentication
            /*
            var authenticationProviderKey = "TestKey";

            Action<JwtBearerOptions> options = o =>
            {
                o.Authority = "www.altamira.com";
                // etc
            };

            services.AddAuthentication()
                .AddJwtBearer(authenticationProviderKey, options);
            */

            // Ocelot

            services
                .AddOcelot()
                .AddSingletonDefinedAggregator<ResponseAggregator>()
                .AddDelegatingHandler<BaseDelegatingHandler>(true)
                .AddCacheManager(x =>
                {
                    x.WithDictionaryHandle();
                })
                .AddConsul();


            // Elasticsearch & Logger

            string connectionStringByEnvironmentVariables = string.Empty;
            string elasticSearchConnectionsStr = string.Empty;

            elasticSearchConnectionsStr = Convert.ToString(Configuration["ElasticConfiguration:Uri"]);
            connectionStringByEnvironmentVariables = "DefaultConnection";

            var log = new LoggerConfiguration()
                .Enrich.WithThreadId()
                .Enrich.WithThreadName()
                .Enrich.WithMachineName()
                .Enrich.WithEnvironmentUserName()
                .Enrich.WithExceptionDetails()
                .Enrich.FromLogContext()
                .WriteTo.Debug()
                .WriteTo.Console()
                .WriteTo.Elasticsearch(new ElasticsearchSinkOptions(new Uri(Configuration["ElasticConfiguration:Uri"]))
                {
                    AutoRegisterTemplate = true,
                    CustomFormatter = new ElasticsearchJsonFormatter()
                })
                .CreateLogger();

            services.AddSingleton(log);

            var tokenOptions = Configuration.GetSection("TokenOptions").Get<TokenOptions>();
            var authenticationProviderKey = "Bearer";

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(authenticationProviderKey, options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidIssuer = tokenOptions.Issuer,
                    ValidAudience = tokenOptions.Audience,
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = SecurityKeyHelper.CreateSecurityKey(tokenOptions.SecurityKey)
                };
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public async void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            //app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            await app.UseOcelot();
        }
    }
}
