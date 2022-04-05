using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using PhoneGuide.Reports.Services.Abstract;
using PhoneGuide.Reports.Services.Concrete;
using PhoneGuide.Reports.Services.RabbitMQ;
using PhoneGuide.Reports.Utilities.DbConfigurationSettings;
using RabbitMQ.Client;

namespace PhoneGuide.Reports
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.Configure<DatabaseSettings>(Configuration.GetSection(nameof(DatabaseSettings)));
            services.AddSingleton<IDatabaseSettings>(sp => sp.GetRequiredService<IOptions<DatabaseSettings>>().Value);

            services.AddSingleton(sp => new ConnectionFactory() 
            { 
                HostName="localhost"
                ,DispatchConsumersAsync=true
            });
            services.AddTransient<IReportManager, ReportManager>();

            services.AddAutoMapper(typeof(Startup));

            services.AddSingleton<RabbitMQPublisher>();
            services.AddSingleton<RabbitMQClientService>();
            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "PhoneGuide.Reports", Version = "v1" });
            });
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "PhoneGuide.Reports v1"));
            }

            app.UseRouting();
            app.UseStaticFiles();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
