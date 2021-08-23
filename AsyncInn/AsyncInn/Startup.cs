using AsyncInn.Data;
using AsyncInn.Models;
using AsyncInn.Models.Interfaces;
using AsyncInn.Models.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AsyncInn
{
    public class Startup
    {
        public IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<AsyncInnDbContext>(options =>
            {
                string connectionString = Configuration.GetConnectionString("DefaultConnection");
                options.UseSqlServer(connectionString);
            });

            services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1", new OpenApiInfo()
                {
                    Title = "Async Inn Demo",
                    Version = "v1"
                });
            });

            services.AddIdentity<ApplicationUser, IdentityRole>(options =>
            {
                options.User.RequireUniqueEmail = true;
            }).AddEntityFrameworkStores<AsyncInnDbContext>();
            // Dependency Injection Goes Here

            services.AddAuthentication(options =>
            {
                options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = JwtTokenService.GetValidationParameters(Configuration);
            });

            services.AddAuthorization(options =>
            {
                options.AddPolicy("createRoom", policy => policy.RequireClaim("permissions", "createRoom"));
                options.AddPolicy("updateRoom", policy => policy.RequireClaim("permissions", "updateRoom"));
                options.AddPolicy("deleteRoom", policy => policy.RequireClaim("permissions", "deleteRoom"));

                options.AddPolicy("createHotel", policy => policy.RequireClaim("permissions", "createHotel"));
                options.AddPolicy("updateHotel", policy => policy.RequireClaim("permissions", "updateHotel"));
                options.AddPolicy("deleteHotel", policy => policy.RequireClaim("permissions", "deleteHotel"));

                options.AddPolicy("createAmenity", policy => policy.RequireClaim("permissions", "createAmenity"));
                options.AddPolicy("updateAmenity", policy => policy.RequireClaim("permissions", "updateAmenity"));
                options.AddPolicy("deleteAmenity", policy => policy.RequireClaim("permissions", "deleteAmenity"));
            });

            services.AddTransient<IUser, IdentityUserService>();
            services.AddTransient<IHotel, HotelService>();
            services.AddTransient<IRoom, RoomService>();
            services.AddTransient<IAmenity, AmenityService>();
            services.AddScoped<JwtTokenService>();

            services.AddControllers().AddNewtonsoftJson(options => 
            options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();

            app.UseSwagger(options =>
            {
                options.RouteTemplate = "/api/{documentName}/swagger.json";
            });

            app.UseSwaggerUI(options =>
            {
                options.SwaggerEndpoint("/api.v1/swagger.json", "Async Inn Demo");
                options.RoutePrefix = "";
            });

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
