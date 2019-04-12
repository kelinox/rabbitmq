using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Microservices.Services.Core.Interface.Services;
using Microservices.Services.Core.Providers;
using Microservices.Services.Core.Repositories;
using Microservices.Services.Core.Services;
using Microservices.Services.Infrastructure.Providers;
using Microservices.Services.Infrastructure.Repositories;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace workout
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
            services.AddTransient<IDbProvider, DbProvider>();
            services.AddTransient<IWorkoutRepository, WorkoutRepository>();
            services.AddTransient<IWorkoutService, WorkoutService>();

            services.AddCors(options =>
            {
                options.AddPolicy("AllowAll", builder =>
                {
                    builder.AllowAnyOrigin()
                            .AllowAnyHeader()
                            .AllowAnyMethod();
                });
            });

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    var base64 = "b3Oh9TaOEG+pEQguLbEJXBZnIlkXGjXHF2IFivJcYeh7YAQ3qwRAOLFNb6B4HBGVsEhkX3aRWhQbQmJrpTinwmfFE3xZHX8Bj2pamvm42nFbAt6nvJUnY4AdcEJE+rIVGM7YOZgLSVmaseXIxDM5C5+ELg==";

                    Console.Out.WriteLine(base64);
                    var signinKey = Convert.FromBase64String(base64);
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = false,
                        ValidateAudience = false,
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(signinKey)
                    };
                });

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);

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
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseCors("AllowAll");
            app.UseAuthentication();

            app.UseMvc();
        }
    }
}
