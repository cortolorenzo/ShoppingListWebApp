using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Extensions;
using Microsoft.OpenApi.Models;

namespace API
{
    public class Startup
    {
        private readonly IConfiguration _config;

        public Startup(IConfiguration config)
        {
            
            _config = config;
        }

        
        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            //extension method
            services.AddApplicationServices(_config);

            services.AddControllers();
            services.AddCors();
            
            //extension method
            // services.AddIdentityServices(_config);
            // services.AddSignalR(o =>
            // {
            // o.EnableDetailedErrors = true;
            // });


            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "API", Version = "v1" });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
           //app.UseMiddleware<ExceptionMiddleware>();
           
            // if (env.IsDevelopment())
            // {
            //     app.UseDeveloperExceptionPage();
            // }
           
            app.UseHttpsRedirection();

            app.UseRouting();
            app.UseCors(policy => policy.WithOrigins("https://localhost:7251")
                                        .AllowAnyMethod()
                                        .AllowAnyHeader()
                                        .AllowCredentials());


            // app.UseAuthentication();
            // app.UseAuthorization();

            // //use index html
            // app.UseDefaultFiles();
            // app.UseStaticFiles();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                // endpoints.MapHub<PresenceHub>("hubs/presence");
                // endpoints.MapHub<MessageHub>("hubs/message");
                //endpoints.MapFallbackToController("Index", "Fallback");
            });
        }


        
    }
}