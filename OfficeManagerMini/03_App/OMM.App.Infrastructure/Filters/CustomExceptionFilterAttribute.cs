using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Routing;
using OMM.Data;
using OMM.Domain;
using System;

namespace OMM.App.Infrastructure.Filters
{
    public class CustomExceptionFilterAttribute : ExceptionFilterAttribute
    {
        private readonly OmmDbContext dbContext;

        public CustomExceptionFilterAttribute(OmmDbContext context)
        {
            this.dbContext = context;
        }

        public override void OnException(ExceptionContext context)
        {
            var ommException = new OmmException
            {
                UserEmail = context.HttpContext.User.Identity.Name,
                ExceptionType = context.Exception.GetType().ToString(),
                ExceptionMessage = context.Exception.Message,
                ExceptionDate = DateTime.UtcNow,
                CallingMethod = context.ActionDescriptor.DisplayName,
                
            };
            this.dbContext.OmmExceptions.Add(ommException);
            this.dbContext.SaveChangesAsync().GetAwaiter().GetResult();

            context.ExceptionHandled = true;
            context.Result = new RedirectToRouteResult(new RouteValueDictionary
            {
                { "controller", "Home" },
                { "action", "Error" },
            });
        }
    }
}
