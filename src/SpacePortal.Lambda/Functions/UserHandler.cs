using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Amazon.Lambda.Core;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;

namespace SpacePortal.Lambda.Functions
{
    public class UserHandler

    {
        private IServiceProvider _serviceProvider;
        private IHttpContextAccessor _contextAccessor;
        public UserHandler() : this
        (Startup.BuildContainer()
            .BuildServiceProvider(), new HttpContextAccessor())
        {
        }

        private UserHandler(ServiceProvider serviceProvider, IHttpContextAccessor context)
        {
            _serviceProvider = serviceProvider;
            _contextAccessor = context;
        }

        [LambdaSerializer(typeof(Amazon.Lambda.Serialization.Json.JsonSerializer))]
        public async Task<string> HandleAsync(ILambdaContext ctx)
        {
            var httpContext = _serviceProvider.GetService<IHttpContextAccessor>();
            var af = _contextAccessor?.HttpContext?.User?.Identity?.IsAuthenticated;

            return await Task.FromResult(ReturnString());
        }

        private string ReturnString()
        {
            return "AM an authorized lambda";
        }
    } 
}
