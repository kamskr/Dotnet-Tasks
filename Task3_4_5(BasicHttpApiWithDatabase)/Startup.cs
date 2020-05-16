using System.Text;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using Task3.Handlers;
using Task3.Middlewares;
using Task3.Services;
using Task3.Entities;
using Microsoft.EntityFrameworkCore;


namespace Task3 {
    public class Startup {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        private IConfiguration Configuration;
        public Startup(IConfiguration configuration){
            this.Configuration = configuration;
        }
        public void ConfigureServices(IServiceCollection services) {
            // services.AddAuthentication("AuthenticationBasic")
            //         .AddScheme<AuthenticationSchemeOptions, BasicAuthHandler>("AuthenticationBasic", null);

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                    .AddJwtBearer(options =>{
                        options.TokenValidationParameters = new TokenValidationParameters {
                            ValidateIssuer = true,
                            ValidateAudience = true,
                            ValidIssuer = "Kamil",
                            ValidAudience = "Students",
                            ValidateLifetime = true,
                            // IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["SecretKey"]))
                            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("asidnvasdiuhvasdvaspoih"))
                        };
                    });
            services.AddTransient<IStudentsServiceDb, SqlServerStudentDbService>();
            services.AddTransient<IDbService, MockDbService>();
            services.AddControllers();
            services.AddDbContext<UniversityAPBDContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("UniversityAPBDDataBase")));
            
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IDbService dbService) {
            if (env.IsDevelopment()) {
                app.UseDeveloperExceptionPage();
            }
            // Here you can define middleware (it is importan to write them before routing)
            // app.UseMiddleware<LoggingMiddleware>();
            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints => {
                endpoints.MapControllers();
            });
        }
    }
}
