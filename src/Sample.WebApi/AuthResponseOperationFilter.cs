using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Swashbuckle.AspNetCore.Swagger;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace Sample.WebApi
{
    public class AuthResponseOperationFilter : IOperationFilter
    {
        public void Apply(Operation operation, OperationFilterContext context)
        {
            var authAttr = context.ApiDescription.ControllerAttributes()
                .Union(context.ApiDescription.ActionAttributes())
                .OfType<AuthorizeAttribute>();

            if (authAttr.Any())
                operation.Responses.Add("401", new Response { Description = "Unauthorized" });
        }
    }
}
