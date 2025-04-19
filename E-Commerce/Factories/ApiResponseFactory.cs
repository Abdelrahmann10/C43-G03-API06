using System.Net;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Shared.ErrorModel;

namespace E_Commerce.Factoris
{
    public class ApiResponseFactory
    {
        public static  IActionResult CustomValidationErrors (ActionContext  Context)
        {
            //Get all Errors in ModelState
            var error = Context.ModelState.Where(error => error.Value.Errors.Any())
                .Select(error => new ValidationError
                {
                    Filed = error.Key , //Id
                    Errors = error.Value.Errors.Select(e => e.ErrorMessage)
                     
                });
            // Create Custom Response 
            var response = new ValidationErrorsResponse
            {
                StatusCode = (int)HttpStatusCode.BadRequest,
                ErrorMessage = "There Is A Problem with Validation",
                Errors = error
            };
            //Return
            return new BadRequestObjectResult(response);
        }
    }
}
