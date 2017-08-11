using Microsoft.AspNetCore.Mvc.Filters;

namespace SandboxCore.Filters
{
    public class UnhandledExceptionFilter : ExceptionFilterAttribute
    {
        public override void OnException(ExceptionContext context)
        {
            string message = context.Exception.Message;
            string path = context.HttpContext.Request.Path;
            
            context.ExceptionHandled = false;
        }
    }
}
