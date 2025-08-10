using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json.Serialization.Metadata;
using System.Text.Json;

namespace Zamin.EndPoints.Web.Filters
{
    [AttributeUsage(AttributeTargets.Method)]
    public class CamelCaseJsonAttribute : Attribute
    {
    }


    public class CamelCaseJsonFilter : IAsyncResultFilter
    {
        public async Task OnResultExecutionAsync(ResultExecutingContext context, ResultExecutionDelegate next)
        {
            var hasCamelCaseAttr = context.ActionDescriptor.EndpointMetadata
                .OfType<CamelCaseJsonAttribute>()
                .Any();

            if (hasCamelCaseAttr && context.Result is ObjectResult objectResult)
            {
                var options = new JsonSerializerOptions
                {
                    PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                    TypeInfoResolver = new DefaultJsonTypeInfoResolver()
                };

                objectResult.Formatters.Clear();
                objectResult.Formatters.Add(new SystemTextJsonOutputFormatter(options));
            }

            await next();
        }
    }
}
