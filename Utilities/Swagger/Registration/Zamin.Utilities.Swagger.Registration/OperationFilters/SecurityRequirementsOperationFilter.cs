using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using Zamin.Utilities.Swagger.Registration.Options;

namespace Zamin.Utilities.Swagger.Registration.OperationFilters;

public class SecurityRequirementsOperationFilter(SwaggerOption option) : IOperationFilter
{
    private readonly SwaggerOption _option = option;

    public void Apply(OpenApiOperation operation, OperationFilterContext context)
    {
        operation.Security ??= new List<OpenApiSecurityRequirement>();

        _option.EnabledSecurities.ForEach(security =>
        {
            operation.Security.Add(security.ToOpenApiSecurityRequirement());
        });
    }
}