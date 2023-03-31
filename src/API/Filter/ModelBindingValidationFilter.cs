using FluentValidation.Results;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace API.Filter
{
    public class ModelBindingValidationFilter : ActionFilterAttribute
    {
        public override void OnActionExecuted(ActionExecutedContext context)
        {
            if (!context.ModelState.IsValid)
            {
                var errors = context
                    .ModelState
                    .SelectMany(x => x.Value.Errors.Select(er => new ValidationFailure(x.Key.Split(".").LastOrDefault(), er.ErrorMessage.Split("Path").FirstOrDefault())));

                IDictionary<string, string[]> formatedErrors = errors.GroupBy(e => e.PropertyName, e => e.ErrorMessage).ToDictionary(e => e.Key, e => e.ToArray());

                formatedErrors.Remove(formatedErrors.FirstOrDefault());

                var errorsObject = new
                {
                    Errors = formatedErrors
                };

                context.Result = new JsonResult(errorsObject)
                {
                    StatusCode = 400
                };
            }
        }
    }
}
