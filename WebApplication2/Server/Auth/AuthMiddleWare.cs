using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication2.Server.Auth
{
    public class AuthMiddleWare
    {
        private readonly RequestDelegate _next;

        public AuthMiddleWare(RequestDelegate next)
        {
            _next = next;
        }

        public async Task check (HttpContext request)
        {
            if(!request.Request.Headers.TryGetValue("", out var value))
            {
                request.Response.StatusCode = 401;
                await request.Response.WriteAsync("Missing key");
                return;
            }
            else
            {
                if(value.Equals(""))
                {
                    await _next(request);
                }
                else
                {
                    request.Response.StatusCode = 401;
                    return;
                }
            }
        }
    }
}
