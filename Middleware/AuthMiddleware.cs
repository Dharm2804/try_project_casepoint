using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

namespace project.Middleware
{
    public class AuthMiddleware
    {
        private readonly RequestDelegate _next;

        public AuthMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            var path = context.Request.Path.ToString().ToLower();

            // Public paths to skip
            if (path.StartsWith("/home/index") || path.StartsWith("/home/logins")
                || path.Contains(".css") || path.Contains(".js") || path.Contains(".png")
                || path.Contains(".jpg"))
            {
                await _next(context);
                return;
            }

            var email = context.Session.GetString("Mail");
            var role = context.Session.GetString("Role");

            // Not logged in
            if (string.IsNullOrEmpty(email))
            {
                context.Response.Redirect("/Home/Index");
                return;
            }

            // Role-based access
            if (path.StartsWith("/admin") && role != "Admin")
            {
                context.Response.Redirect("/Home/Index");
                return;
            }

            if (path.StartsWith("/employe") && role != "Employee")
            {
                context.Response.Redirect("/Home/Index");
                return;
            }

            await _next(context);
        }
    }
}
