using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace MiniBlog.Endpoints.API.Extentions.DependencyInjection.Swaggers.Filters;

public class AddParamsToHeader : IOperationFilter
{
    public void Apply(OpenApiOperation operation, OperationFilterContext context)
    {
        if (operation.Security == null)
            operation.Security = new List<OpenApiSecurityRequirement>();

        operation.Security.Add(new OpenApiSecurityRequirement()
        {
            {
                new OpenApiSecurityScheme()
                {
                    Name="Oauth2",
                    Scheme= "Oauth2",
                    In = ParameterLocation.Header,
                    Reference = new OpenApiReference()
                    {
                        Type= ReferenceType.SecurityScheme,
                        Id= "Oauth2"
                    }
                }, new List<string>()
            }
        });
    }
}
