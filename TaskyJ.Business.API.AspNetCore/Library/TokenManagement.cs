using Microsoft.AspNetCore.Http;
using TaskyJ.Globals.Data.DataObjects;

namespace TaskyJ.Business.API.AspNetCore.Library
{
    public static class TokenManagement
    {
        public static DBSessionJ DoAuthenticate(HttpContext context, DBUserJ model)
        {
            return TaskyJManager.Authenticate(model, GetIpAddress(context.Request));
        }

        public static DBRefreshTokenJ DoRefreshToken(HttpContext context, string refreshToken)
        {
            if (string.IsNullOrEmpty(refreshToken))
                return null;
            var newtoken = TaskyJManager.RefreshToken(refreshToken, GetIpAddress(context.Request));
            if (newtoken == null)
                return null;

            return newtoken;
        }

        public static void SetTokenCookie(HttpContext context, string token)
        {
            var cookieOptions = new CookieOptions
            {
                HttpOnly = true,
                Expires = System.DateTime.UtcNow.AddDays(7)
            };
            if (context.Response != null)
                context.Response.Cookies.Append("refreshToken", token, cookieOptions);
        }

        public static string GetIpAddress(HttpRequest Request)
        {
            var temp = "";
            try
            {
                if (Request == null)
                    return "0.0.0.0";
                if (Request.Headers.ContainsKey("X-Forwarded-For"))
                    temp = Request.Headers["X-Forwarded-For"];
                else
                    temp = Request.HttpContext.Connection.RemoteIpAddress.MapToIPv4().ToString();
            }
            catch { }
            if (string.IsNullOrWhiteSpace(temp))
                temp = "0.0.0.0";
            if (temp == "0.0.0.1")
                temp = "127.0.0.1";
            return temp;
        }
    }
}
