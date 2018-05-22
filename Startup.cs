using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using NSwag.AspNetCore;

namespace nswagscratch
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
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseMvc();

app.UseSwaggerUi3(
    controllerTypes: new [] { typeof(Controllers.ValuesController) },
    configure: settings =>
{
    settings.SwaggerRoute = "/nswag/v1/swagger.json";
        settings.SwaggerUiRoute = "/nswag";
        
        var genSettings = settings.GeneratorSettings;
        genSettings.DefaultPropertyNameHandling = NJsonSchema.PropertyNameHandling.CamelCase        ;
        genSettings.Description = "asdsds"; //"Servizio API di Zeus Retail 4.0"
        genSettings.Title = "asdsds"; //"Zeus.API"
        genSettings.Version = "asdsds"; // "0.0.1"
},
schemaGenerator: new NSwag.SwaggerGeneration.SwaggerJsonSchemaGenerator(
    new NSwag.SwaggerGeneration.WebApi.WebApiToSwaggerGeneratorSettings()
          { DefaultReferenceTypeNullHandling = NJsonSchema.ReferenceTypeNullHandling.NotNull
          , IsAspNetCore = true
          })
);
        }
    }
}
