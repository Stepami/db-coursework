using CourseWork.API.Handlers;
using CourseWork.Lib;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Threading.Tasks;

namespace CourseWork.API
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
            services.AddDbContext<CWContext>();

            services.AddCors();

            services
                .AddControllers(opt => 
                {
                    opt.OutputFormatters.RemoveType<HttpNoContentOutputFormatter>();
                })
                .AddNewtonsoftJson(setup =>
                {
                    setup.SerializerSettings.ContractResolver = Utils.Settings.ContractResolver;
                    setup.SerializerSettings.ReferenceLoopHandling = Utils.Settings.ReferenceLoopHandling;
                    setup.SerializerSettings.Formatting = Utils.Settings.Formatting;
                });

            services.AddAuthentication("BasicAuthentication")
                .AddScheme<AuthenticationSchemeOptions, BasicAuthenticationHandler>("BasicAuthentication", null);

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "CourseWork.API", Version = "v1" });
                c.AddSecurityDefinition("basic", new OpenApiSecurityScheme
                {
                    Name = "Authorization",
                    Type = SecuritySchemeType.Http,
                    Scheme = "basic",
                    In = ParameterLocation.Header,
                    Description = "Basic Authorization header using the Bearer scheme."
                });
                c.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "basic"
                            }
                        },
                        Array.Empty<string>()
                    }
                });
                var solutionName = Assembly.GetExecutingAssembly().GetName().Name.Split('.')[0];
                string solutionDirectory = Directory.GetParent(Environment.CurrentDirectory).FullName;
                var sep = RuntimeInformation.IsOSPlatform(OSPlatform.Windows) ? @"\" : "/";
                var pathToXmlDocumentsToLoad = AppDomain.CurrentDomain.GetAssemblies()
                    .Where(x => x.FullName.StartsWith(solutionName))
                    .Select(x => $@"{solutionDirectory}{sep}{x.GetName().Name}{sep}bin{sep}{x.GetName().Name}.xml")
                    .ToList();
                pathToXmlDocumentsToLoad.ForEach(path => c.IncludeXmlComments(path));
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseDeveloperExceptionPage();
            app.UseSwagger();
            app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "ExpoScope.API v1"));

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseCors(builder => builder
                .AllowAnyOrigin()
                .AllowAnyMethod()
                .AllowAnyHeader());

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
