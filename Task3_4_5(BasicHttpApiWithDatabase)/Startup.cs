using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Task3.Middlewares;
using Task3.Services;

namespace Task3 {
    public class Startup {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services) {
            services.AddTransient<IStudentsServiceDb, SqlServerStudentDbService>();
            services.AddTransient<IDbService, MockDbService>();
            services.AddControllers();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IDbService dbService) {
            if (env.IsDevelopment()) {
                app.UseDeveloperExceptionPage();
            }

            // Here you can define middleware (it is importan to write them before routing)
            app.UseMiddleware<LoggingMiddleware>();

            app.Use(async (context, next) => {
                if(!context.Request.Headers.ContainsKey("Index")) {
                    context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                    await context.Response.WriteAsync("There is no Index header in the request");
                    return;
                } else {
                    var index = context.Request.Headers["Index"].ToString();
                    if(!dbService.ExistsIndexNumber(index)){
                        await context.Response.WriteAsync("There is no such Index in the database");
                    }
                }
                await next();
            });
            
            app.UseRouting();

            app.UseEndpoints(endpoints => {
                endpoints.MapControllers();
            });
        }
    }
}
