using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication2.Server
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
    public class RoleAttriub : Attribute, IAuthorizationFilter
    {
        private readonly string  _Role;
        public RoleAttriub(string Role)
        {
            _Role = Role;
        }
        public void OnAuthorization(AuthorizationFilterContext context)
        {
            if(!(context.HttpContext.User.Identity.Name == ""))
            {
                context.Result = new ForbidResult();
            }
        }
    }
}
