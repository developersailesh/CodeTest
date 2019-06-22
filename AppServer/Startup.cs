
namespace AppServer
{
    using System.IO;
    using System.Net;
    using System.Net.Http;
    using System.Reflection;
    using System.Threading.Tasks;
    using AppServer.ErrorHandling;
    using AppServer.Handlers;
    using AppServer.Helpers;
    using AppServer.HttpClientFactory;
    using AppServer.Interfaces;
    using AutoMapper;
    using MediatR;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Hosting;
    using Microsoft.Extensions.Logging;
    using Serilog;
    using Serilog.Events;

    public class Startup
    {
        public IConfiguration _configuration { get; }

        public Startup(IConfiguration configuration)
        {
            _configuration = new ConfigurationBuilder()
                    .SetBasePath(Directory.GetCurrentDirectory())
                    .AddJsonFile("appsettings.json")
                    .Build();
             
        }


        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            // for get appsettings from anywhere
            services.AddSingleton(_configuration);

            //Swagger
            services.AddSwaggerGen(setupaction =>
                    {
                        setupaction.SwaggerDoc(
                            "PetNamesByGenderOpenApiSpecification",
                            new Microsoft.OpenApi.Models.OpenApiInfo()
                            {
                                Title = "PetNamesByGender Open Api",
                                Version = "1.0"
                            }); ;
                    }
                );

            //Automapper
            services.AddAutoMapper(typeof(Startup));
            var mapper = services.BuildServiceProvider().GetService<IMapper>();
            mapper.ConfigurationProvider.AssertConfigurationIsValid();

            //DI: IPeopleClientService => PeopleClientService
            services.AddScoped<IPeopleClientService, PeopleClientService>();

            //Cors Enabled
            services.AddCors(options =>
            {
                options.AddPolicy("*",
                    builder => builder.AllowAnyOrigin()
                    .AllowAnyMethod()
                    .AllowAnyHeader());
            });

            services.AddMvc()
                .AddNewtonsoftJson()
            .SetCompatibilityVersion(CompatibilityVersion.Version_3_0);

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();

            }
            app.UseRouting(routes =>
            {
                routes.MapControllers();
            });

            app.UseAuthorization();

            //Cors
            app.UseCors(options => options.AllowAnyOrigin());

           

            //Swagger
            app.UseSwagger();
            app.UseSwaggerUI(setupAction =>
            {
                setupAction.SwaggerEndpoint(
                    "/swagger/PetNamesByGenderOpenApiSpecification/swagger.json", "");
            });

        }
    }
}
