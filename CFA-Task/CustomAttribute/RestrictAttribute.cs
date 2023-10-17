using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;

namespace CFA_Task.CustomAttribute
{
    public class RestrictAttribute : TypeFilterAttribute
    {
        public RestrictAttribute() : base(typeof(RestrictFilter)) { }
    }
    public class RestrictFilter : IAuthorizationFilter
    {
        public void OnAuthorization(AuthorizationFilterContext context)
        {
            context.Result = new StatusCodeResult(StatusCodes.Status404NotFound);
        }
    }
}
