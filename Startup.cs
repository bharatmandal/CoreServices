using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CoreServices.Models;
using CoreServices.Repository;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace CoreServices
{
    public class Startup
    {
        //https://www.codeproject.com/Articles/1259484/CRUD-Operation-in-ASP-NET-Core-Web-API-with-Entity

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            //remove default json formatting this we are doing so that we can get angular req from other domain
            services.AddControllers().AddJsonOptions(options =>
            {
                options.JsonSerializerOptions.PropertyNamingPolicy = null;
                options.JsonSerializerOptions.DictionaryKeyPolicy = null;
            });
            //add cors package
            services.AddCors();
            //services.AddControllers();
            services.AddDbContext<BlogDBContext>(item => item.UseSqlServer(Configuration.GetConnectionString("BlogDBConnection")));
            services.AddScoped<IPostRepository, PostRepository>();
            
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            //configurations to cosume the Web API from port : 4200 (Angualr App)
            app.UseCors(options =>
            options.WithOrigins("http://localhost:4200")
            .AllowAnyMethod()
            .AllowAnyHeader());

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
