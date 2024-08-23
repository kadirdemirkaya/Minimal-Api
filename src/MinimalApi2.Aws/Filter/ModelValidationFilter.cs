
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace MinimalApi2.Aws.Filter
{
    public class ModelValidationFilter : IEndpointFilter
    {
        public async ValueTask<object?> InvokeAsync(EndpointFilterInvocationContext context, EndpointFilterDelegate next)
        {
            foreach (var arg in context.Arguments)
            {
                if (arg == null)
                {
                    continue;
                }

                var validationContext = new ValidationContext(arg);
                var validationResults = new List<ValidationResult>();

                if (!Validator.TryValidateObject(arg, validationContext, validationResults, validateAllProperties: true))
                {
                    var errors = validationResults.Select(vr => vr.ErrorMessage).ToList();
                    return new BadRequestObjectResult(errors);
                }
            }

            return await next(context);
        }
    }
}
