using HashidsNet;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using UrlUtility.API.Interfaces;
using UrlUtility.API.Repository.Cosmo;
using UrlUtility.API.Repository.Sql;
using UrlUtility.API.Repository.Sql.Repositories;
using UrlUtility.API.Services;

namespace UrlUtility.API
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
            //Mongo DB and Cosmo DB using Mongo DB API
            //services.Configure<MongoDbOptions>(Configuration.GetSection(nameof(MongoDbOptions)));
            //services.AddSingleton<IUrlRepository, MongoUrlRepository>();
            //MongoConfiguration.InitializeMongoMapping();

            //Cosmo DB Core (SQL) API
            services.AddSingleton<IUrlRepository>(CosmoConfiguration.InitializeCosmosClientInstanceAsync(Configuration).GetAwaiter().GetResult());

            //Cosmo DB entityframewrok
            //services.AddDbContext<UrlDbContext>(opt =>
            //{
            //    opt.UseCosmos(Configuration["Cosmo:ConnectionString"], Configuration["Cosmo:DatabaseName"]);
            //});
            //services.AddScoped<IUrlRepository, CosmoEntityUrlRepository>();

            //Sql server configuration
            services.AddDbContext<SqlUrlDbContext>(opt =>
            {
                opt.UseSqlServer(Configuration["SqlServer:ConnectionString"]);
            });
            services.AddScoped<IShortUrlRepository, ShortUrlRepository>();

            services.AddScoped<ITime, TimeService>();
            services.AddScoped<IHashids>(_ => new Hashids(Configuration["HashIds:Salt"], Configuration.GetValue<int>("HashIds:Lenght")));

            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "UrlUtility.API", Version = "v1" });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "UrlUtility.API v1"));
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
