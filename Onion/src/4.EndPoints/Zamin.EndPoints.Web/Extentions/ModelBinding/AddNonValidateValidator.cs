using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Zamin.EndPoints.Web.ModelBinding;
namespace Zamin.EndPoints.Web.Extentions.ModelBinding;

public static class NonValidatingValidatorExtentions
{
    public static IServiceCollection AddNonValidatingValidator(this IServiceCollection services)
    {
        var validator = services.FirstOrDefault(s => s.ServiceType == typeof(IObjectModelValidator));
        if (validator != null)
        {
            services.Remove(validator);
            services.Add(new ServiceDescriptor(typeof(IObjectModelValidator), _ => new NonValidatingValidator(), ServiceLifetime.Singleton));
        }
        return services;
    }
}
