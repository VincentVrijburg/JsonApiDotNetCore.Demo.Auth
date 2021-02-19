using JsonApiDotNetCore.Demo.Auth.Data.Entities;
using JsonApiDotNetCore.Configuration;
using JsonApiDotNetCore.Controllers;
using JsonApiDotNetCore.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Logging;

namespace JsonApiDotNetCore.Demo.Auth.Api.Controllers
{
    [Authorize]
    public class CarsController : JsonApiController<Car>
    {
        public CarsController(
            IJsonApiOptions options, 
            ILoggerFactory loggerFactory, 
            IResourceService<Car> resourceService)
            : base(options, loggerFactory, resourceService)
        {
        }
    }
}