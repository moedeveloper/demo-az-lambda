using System.IO;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.AzureAD.UI;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SpacePortal.Lambda.Configuration;

namespace SpacePortal.Lambda
{
    public class Startup
    {
        public static IConfiguration Configuration { get; private set; }

        public static IServiceCollection BuildContainer()
        {
            Configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddEnvironmentVariables()
                .Build();
            var res = ConfigureServices();
            return res;
        }

        private static IServiceCollection ConfigureServices()
        {
            var services = new ServiceCollection();

            var awsOptions = Configuration.GetAWSOptions();
            services.AddDefaultAWSOptions(awsOptions);

            
            services.AddServices(); 

            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

            services
                .AddAuthentication(sharedOptions =>
                {
                    sharedOptions.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
                })
                .AddJwtBearer(options =>
                {
                    var authSettings = Configuration.GetSection("AzureAd").Get<AzureAdOptions>();

                    options.Audience = authSettings.ClientId;
                    options.Authority = authSettings.Authority;
                });


            //services.Configure<CookiePolicyOptions>(options =>
            //{
            //    // This lambda determines whether user consent for non-essential cookies is needed for a given request.
            //    options.CheckConsentNeeded = context => true;
            //    options.MinimumSameSitePolicy = SameSiteMode.None;
            //});

            //services.AddAuthentication(AzureADDefaults.AuthenticationScheme)
            //    .AddAzureAD(options => Configuration.Bind("AzureAd", options));

            //services.Configure<OpenIdConnectOptions>(AzureADDefaults.OpenIdScheme, options =>
            //{
            //    options.Authority = options.Authority + "/v2.0/";         // Microsoft identity platform

            //    options.TokenValidationParameters.ValidateIssuer = false; // accept several tenants (here simplified)
            //});

            //services.AddMvc(options =>
            //    {
            //        var policy = new AuthorizationPolicyBuilder()
            //            .RequireAuthenticatedUser()
            //            .Build();
            //        options.Filters.Add(new AuthorizeFilter(policy));
            //    })
            //    .SetCompatibilityVersion(CompatibilityVersion.Version_2_1);

            return services;
        }
    }
}
