using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace Zamin.EndPoints.Web.Extentions.NonValidateValidator;

public class NonValidatingValidator : IObjectModelValidator
{
    public void Validate(ActionContext actionContext, ValidationStateDictionary? validationState, string prefix, object? model)
    {
    }
}