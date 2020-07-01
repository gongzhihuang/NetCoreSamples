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
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using OptionDemo.Extensions;

namespace OptionDemo
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
            services.Configure<RabbitMQConfiguration>(
               options =>
               {
                   options.RabbitHost = Configuration.GetSection("RabbitMQ:Host").Value;
                   options.RabbitPort = int.Parse(Configuration.GetSection("RabbitMQ:Port").Value);
                   options.RabbitUserName = Configuration.GetSection("RabbitMQ:UserName").Value;
                   options.RabbitPassword = Configuration.GetSection("RabbitMQ:Password").Value;
                   options.ExchangeName = Configuration.GetSection("RabbitMQ:ExchangeName").Value;
                   options.QueueName = Configuration.GetSection("RabbitMQ:QueueName").Value;
                   options.RouteKey = Configuration.GetSection("RabbitMQ:RouteKey").Value;
               });


            services.AddControllers();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

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
