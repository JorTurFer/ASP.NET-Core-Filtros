using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Filtros.Extensions.Filters
{
    public class HandledByMvcAttribute : ResultFilterAttribute
    {
        public override void OnResultExecuting(ResultExecutingContext context)
        {
            context.HttpContext.Response.Headers.Add("X-Handled-By", "ASP.NET Core MVC");
            base.OnResultExecuting(context);
        }
    }
}
