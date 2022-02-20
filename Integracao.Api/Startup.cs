using Amazon;
using Amazon.DynamoDBv2;
using Integracao.Api.Interfaces;
using Integracao.Api.Model;
using Integracao.Api.Repository;
using Integracao.Api.Repository.Common;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;

namespace Integracao.Api
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public static IConfiguration Configuration { get; private set; }

        // This method gets called by the runtime. Use this method to add services to the container
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "v1",
                    Title = "Integração API",
                });
            });

            services.AddScoped<IIntegracaoRepository, IntegracaoRepository>();

            var clientConfig = new AmazonDynamoDBConfig()
            {
                RegionEndpoint = RegionEndpoint.USWest2,
                //ServiceURL = "http://localhost:8042"
            };

            services.AddScoped<IDynamoDbContext<ResultadoIntegracaoModel>>(provider =>
                            new DynamoDbContext<ResultadoIntegracaoModel>(new AmazonDynamoDBClient(clientConfig)));


            services.AddControllers();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
               
            }

            app.UseSwagger();
            app.UseSwaggerUI();

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
