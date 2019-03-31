using System;
using GreenPipes;
using MassTransit;
using Microservices.Services.Users.Infrastructure;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using RabbitMQ.Client;

namespace user
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
            var connection = @"Server=mssql;Database=master;User=sa;Password=Your_password123;";
            services.AddDbContext<UserContext>(options => options.UseSqlServer(connection));
            
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);

            services.AddScoped<CounterEventHandler>();

            services.AddMassTransit(x => 
            {
                x.AddConsumer<CounterEventHandler>();

                x.AddBus(provider => Bus.Factory.CreateUsingRabbitMq(cfg => 
                {
                    var host = cfg.Host("localhost", "/",  h => 
                    {
                        h.Username("guest");
                        h.Password("guest");
                    });

                    cfg.ExchangeType = ExchangeType.Fanout;

                    Console.Out.WriteLine("STARTUP USER : " + host.Settings.HostAddress.ToString());

                    cfg.ReceiveEndpoint(host, "counters", ep => 
                    {
                        ep.PrefetchCount = 16;
                        ep.UseMessageRetry(m => m.Interval(2, 100));

                        ep.ConfigureConsumer<CounterEventHandler>(provider);
                    });
                }));
            });
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

            app.UseHttpsRedirection();
            app.UseMvc();
            UpdateDatabase(app);

            var bus = app.ApplicationServices.GetService<IBusControl>();
            bus.Start();
        }
        

        private static void UpdateDatabase(IApplicationBuilder app)
        {
            using (var serviceScope = app.ApplicationServices
                .GetRequiredService<IServiceScopeFactory>()
                .CreateScope())
            {
                using (var context = serviceScope.ServiceProvider.GetService<UserContext>())
                {
                    if (!context.Database.EnsureCreated())
                        context.Database.Migrate();
                }
            }
        }
    }
}
