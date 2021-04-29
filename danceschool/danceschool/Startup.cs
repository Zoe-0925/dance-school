using danceschool.Context;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using MediatR;
using FirebaseAdmin;
using Google.Apis.Auth.OAuth2;
using Microsoft.IdentityModel.Tokens;
using danceschool.Models;
using Newtonsoft.Json;
using System.IO;
using Microsoft.AspNetCore.SpaServices.ReactDevelopmentServer;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using System;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using System.Reflection;
using danceschool.Middlewares;
using System.Collections.Generic;
using Microsoft.AspNetCore.HttpOverrides;
using System.Threading.Tasks;
using StackExchange.Redis;

namespace danceschool
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }
        public IWebHostEnvironment HostingEnvironment { get; set; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddCors();


            services.AddControllers().AddNewtonsoftJson(options =>
            options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore);

            Firebase fbconfig = new Firebase();
            Configuration.Bind("Firebase", fbconfig);

            var json = JsonConvert.SerializeObject(fbconfig);

            FirebaseApp.Create(new AppOptions()
            {
                Credential = GoogleCredential.FromJson(json),
            });

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
         .AddJwtBearer(options =>
         {
             options.Authority = "https://securetoken.google.com/danceschool-45356";
             options.TokenValidationParameters = new TokenValidationParameters
             {
                 ValidateIssuer = true,
                 ValidIssuer = "https://securetoken.google.com/danceschool-45356",
                 ValidateAudience = true,
                 ValidAudience = "danceschool-45356",
                 ValidateLifetime = true
             };
         });

            services.AddMvc(option =>
            {
                option.EnableEndpointRouting = false;
                option.Filters.Add(typeof(ModelStateFeatureFilter));
            });
            services.AddSpaStaticFiles(configuration =>
            {
                configuration.RootPath = "./Client/build";
            });

            services.AddRazorPages();

            services.AddSwaggerGen(c =>
                   {
                       c.SwaggerDoc("v1", new OpenApiInfo { Title = "test", Version = "v1" });
                   });

            services.AddSwaggerGen(c =>
            {
                c.AddSecurityDefinition("oauth2", new OpenApiSecurityScheme
                {
                    Description = "Standard Authorization header using the Bearer scheme. Example: \"bearer {token}\"",
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "bearer"
                });

                c.AddSecurityRequirement(new OpenApiSecurityRequirement{
            {
             new OpenApiSecurityScheme{
             Reference = new OpenApiReference{
                Id = "Bearer", //The name of the previously defined security scheme.
                Type = ReferenceType.SecurityScheme
            }
            },new List<string>()
            }
            });

                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "Dance school API",
                    Version = "v1",
                    Contact = new OpenApiContact
                    {
                        Name = "Zoe Zhang",
                        Email = "zoeszha@gmail.com",
                        Url = new Uri("https://github.com/Zoe-0925/"),
                    }
                });

                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                c.IncludeXmlComments(xmlPath);
            });

            services.AddStackExchangeRedisCache(options =>
                  {
                      options.Configuration = Configuration.GetConnectionString("RedisAzure");
                      options.InstanceName = "master";
                  });

            services.AddDbContext<ApplicationContext>(options =>
                 options.UseSqlServer(
            Configuration.GetConnectionString("Production"),
            b => b.MigrationsAssembly(typeof(ApplicationContext).Assembly.FullName)));

            services.AddMediatR(typeof(Startup));

            services.AddAutoMapper(typeof(Startup));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {

            app.UseCors(builder => builder.AllowAnyOrigin()
            .AllowAnyMethod()
            .AllowAnyHeader());

            app.UseMvc();
            app.UseSpa(spa =>
            {
                spa.Options.SourcePath = Path.Join(env.ContentRootPath, "Client");
                if (env.IsDevelopment())
                {
                    spa.UseReactDevelopmentServer(npmScript: "start");
                }
            });


            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                /**
                c =>
                {
                    c.RouteTemplate = "danceschool/swagger/{documentName}/swagger.json";
                }**/


                /**  app.UseSwaggerUI(c =>
                  {
                      c.RoutePrefix = "v1";
                      var basePath = string.IsNullOrWhiteSpace(c.RoutePrefix) ? "." : "..";
                      c.SwaggerEndpoint($"{basePath}/swagger/{c.RoutePrefix}/swagger.json", "Name");
                  });*/

                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "danceschool"));

            }

            app.UseRequestValidator();//Check if the request header's authentication token is present
                                      // app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseSpaStaticFiles(new StaticFileOptions { RequestPath = "/danceschool/Client/build" });

            app.UseExceptionHandler(c => c.Run(async context =>
            {
                var exception = context.Features
    .Get<IExceptionHandlerPathFeature>()
    .Error;
                var response = new { error = exception.Message };
                await context.Response.WriteAsJsonAsync(response);
            }));

            app.UseForwardedHeaders(new ForwardedHeadersOptions
            {
                ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto
            });

            app.UseRouting();

            app.UseAuthentication();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
