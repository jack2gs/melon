using Com.Melon.Wrap.Site.Core.Domain;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;

namespace Com.Melon.Wrap.Site.Core.Port.Adapter.Mvc
{
    public class CookieAuthenticationMiddleware
    {
        private readonly RequestDelegate _next;

        public CookieAuthenticationMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context, ISessionRepository sessionRepository)
        {
            string token = context.Request.Cookies["SessionID"];

            if (!string.IsNullOrEmpty(token))
            {
                var session =  sessionRepository.GetSessionByToken(token);

                if (session != null)
                {
                    var claim = new Claim(ClaimTypes.NameIdentifier, session.UserId.ToString());
                    var claimIdentity = new ClaimsIdentity(MelonAuthenticationDefaults.AuthenticationSchema);
                    claimIdentity.AddClaim(claim);

                    var claimPriciple = new ClaimsPrincipal(claimIdentity);
                    context.User = claimPriciple;
                    Thread.CurrentPrincipal = claimPriciple;
                }
            }

            // Call the next delegate/middleware in the pipeline
            await _next(context);
        }
    }
}
