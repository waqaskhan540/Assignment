using Funda.Domain.Dtos;
using Funda.Domain.Exceptions;
using Funda.Domain.Interfaces;
using Funda.Domain.Services.Extensions;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using System.Text.Json;

namespace Funda.Api
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
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Funda.Api", Version = "v1" });
            });

            services.AddMediatR(typeof(IApiService).Assembly);
            services.AddServices(Configuration);
            services.AddMemoryCache();

            services.AddCors(options =>
            {
                options.AddDefaultPolicy(
                    builder =>
                    {
                        builder.AllowAnyOrigin();
                        builder.AllowAnyMethod();
                        builder.AllowAnyHeader();
                    });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Funda.Api v1"));
            }

            app.UseExceptionHandler(app =>
            {
                app.Run(async context =>
                {
                    var exceptionHandlerPathFeature =
                            context.Features.Get<IExceptionHandlerPathFeature>();

                    if (exceptionHandlerPathFeature?.Error is ApiUnavailableException)
                    {
                        context.Response.StatusCode = 429; //too many requests status code
                        context.Response.ContentType = "application/json";

                        var exception = exceptionHandlerPathFeature?.Error;
                        var errorResponse = new ErrorResponse { Error = exception.Message };
                        await context.Response.WriteAsync(JsonSerializer.Serialize(errorResponse));
                    }else
                    {
                        context.Response.StatusCode = 500; 
                        context.Response.ContentType = "application/json";

                        var exception = exceptionHandlerPathFeature?.Error;
                        var errorResponse = new ErrorResponse { Error = "Internal Server Error" };
                        await context.Response.WriteAsync(JsonSerializer.Serialize(errorResponse));
                    }
                });
            });


            //app.UseHttpsRedirection();
            app.UseCors();
            app.UseRouting();

            
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
