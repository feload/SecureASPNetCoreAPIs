using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.AspNetCore.Mvc.Versioning;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using SecureASPNetCoreAPIs.Context;
using SecureASPNetCoreAPIs.Filters;
using SecureASPNetCoreAPIs.Models;

namespace SecureASPNetCoreAPIs {
    public class Startup {
        public IConfiguration Configuration { get; }
        public int? _httpsPort { get; set; }
        public Startup (IConfiguration configuration, IHostingEnvironment env) {
            Configuration = configuration;
            if (env.IsDevelopment ()) {
                // Get the HTTPs port (only in development);
                var launchJsConfig = new ConfigurationBuilder ()
                    .SetBasePath (env.ContentRootPath)
                    .AddJsonFile ("Properties//launchSettings.json")
                    .AddJsonFile ("appsettings.json", optional : false, reloadOnChange : true)
                    .AddJsonFile ($"appsettings.{env.EnvironmentName}.json", optional : true)
                    .AddEnvironmentVariables ()
                    .Build ();

                _httpsPort = launchJsConfig.GetValue<int> ("iisSettings:iisExpress:sslPort");
            }
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices (IServiceCollection services) {
            services
                .AddMvc (opt => {
                    opt.Filters.Add (typeof (JsonExceptionFilter));

                    opt.SslPort = _httpsPort;
                    opt.Filters.Add (typeof (RequireHttpsAttribute));

                    var jsonFormatter = opt.OutputFormatters.OfType<JsonOutputFormatter> ().Single ();
                    opt.OutputFormatters.Remove (jsonFormatter);
                    opt.OutputFormatters.Add (new IonOutputFormatter (jsonFormatter));
                })
                .SetCompatibilityVersion (CompatibilityVersion.Version_2_1);

            services.AddRouting (opt => opt.LowercaseUrls = true);
            services.AddDbContext<HotelApiContext> (options =>
                options.UseSqlServer (Configuration.GetConnectionString ("SecureASPNetAPIs")));

            // This line does two things: 1. It creates an instance out of the data it grabbed from appsettings file. 2. It wraps up this new instance in order to allow it to be injected in the controller.
            services.Configure<HotelInfo> (Configuration.GetSection ("Info"));

            services.AddApiVersioning (opt => {
                opt.ApiVersionReader = new MediaTypeApiVersionReader ();
                opt.AssumeDefaultVersionWhenUnspecified = true;
                opt.ReportApiVersions = true;
                opt.DefaultApiVersion = new ApiVersion (1, 0);
                opt.ApiVersionSelector = new CurrentImplementationApiVersionSelector (opt);
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure (IApplicationBuilder app, IHostingEnvironment env, IServiceProvider serviceProvider) {
            if (env.IsDevelopment ()) {
                app.UseDeveloperExceptionPage ();

                var context = serviceProvider.GetRequiredService<HotelApiContext> ();
                AddTestData (context);

            } else {
                app.UseHsts (opt => {
                    opt.MaxAge (days: 180);
                    opt.IncludeSubdomains ();
                    opt.Preload ();
                });
            }
            app.UseHttpsRedirection ();
            app.UseMvc ();
        }

        private static void AddTestData (HotelApiContext context) {

            /*
            context.Rooms.Add (new RoomEntity {
                    Name = "Oxford Suite",
                    Rate = 10119
            });

            context.Rooms.Add (new RoomEntity {
                    Name = "Driscoll Suite",
                    Rate = 23959
            });

            context.SaveChanges ();
             */
        }
    }
}