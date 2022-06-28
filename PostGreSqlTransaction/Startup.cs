using Microsoft.EntityFrameworkCore;
using PostGreSqlTransaction.Entities;
using PostGreSqlTransaction.Extenstions;

namespace PostGreSqlTransaction
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }
        // public Startup(IHostEnvironment env)
        // {
        //     var builder = new ConfigurationBuilder()
        //         .SetBasePath(env.ContentRootPath)
        //         .AddJsonFile("appsettings.json", false, true)
        //         .AddJsonFile($"appsettings.{env.EnvironmentName}.json", false, true)
        //         .AddEnvironmentVariables();
        //     configuration = builder.Build();
        // }
        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddAutoMapper(typeof(Startup));
            services.AddControllers();
 
            // add config swagger
            services.AddSwagger();
            var connectionString = Configuration["DbContextSettings:ConnectionString"];
            services.AddDbContext<TransContext>(
                    opts => opts.UseNpgsql(connectionString, (options) =>
                    {
                        options.EnableRetryOnFailure(10, TimeSpan.FromSeconds(30), new string[] { "57P01" });
                    })
            );
            services.ConfigureCors();
            services.ConfigureLoggerService();
            services.ConfigureRepositories();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "API v1"));
            }

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
