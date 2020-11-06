using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Exemplo.API.Extensions;
using Exemplo.API.Resources;

namespace Exemplo.API.Controllers.Config
{
    public static class InvalidModelStateResponseFactory
    {
        public static IActionResult ProduceErrorResponse(ActionContext context)
        {
            var errors = context.ModelState.GetErrorMessages();
            var response = new ErrorResource(messages: errors);
            
            return new BadRequestObjectResult(response);
        }
    }
}