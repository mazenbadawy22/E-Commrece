using System.Net;
using Microsoft.AspNetCore.Mvc;
using Shared.ErrorModels;

namespace E_Commrece.Factories
{
    public class ApiResponseFactory
    {
        public static IActionResult CustomValidationErrors(ActionContext context)
        {
            var errors = context.ModelState.Where(error => error.Value.Errors.Any())
                .Select(error => new ValidationError
                {
                    Filed=error.Key,
                    Errors=error.Value.Errors.Select(e=>e.ErrorMessage)
                });
            var reponse = new ValidationErrorResponse
            {
                StatusCode = (int)HttpStatusCode.BadRequest,
                ErrorMessage = "There Is a Problem With Validation",
                Errors = errors
            };
            return new BadRequestObjectResult(reponse);
        }
    }
}
