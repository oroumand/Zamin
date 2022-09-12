using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace Zamin.EndPoints.Web.Extentions.NonValidateValidator;

public static class NonValidateValidatorExtentions
{
    public static IServiceCollection AddNonValidateValidator(this IServiceCollection services)
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
