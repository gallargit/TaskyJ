using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TaskyJ.Business.API.AspNetCore.Library;
using TaskyJ.Globals.Data.DataObjects;

namespace TaskyJ.Business.API.AspNetCore.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : Controller
    {
        [AllowAnonymous]
        [HttpPost("authenticate")]
        public IActionResult Authenticate([FromBody] DBUserJ model)
        {
            var session = TokenManagement.DoAuthenticate(HttpContext, model);
            if (session == null)
                return BadRequest(new { message = "Username or password is incorrect" });

            TokenManagement.SetTokenCookie(HttpContext, session.RefreshToken.ToString());

            return Ok(session);
        }

        [AllowAnonymous]
        [HttpPost("refreshtoken")]
        public IActionResult RefreshToken()
        {
            var refreshToken = Request.Cookies["refreshToken"];
            if (refreshToken == null)
                return Unauthorized(new { message = "No token supplied" });

            var refreshedToken = TokenManagement.DoRefreshToken(HttpContext, refreshToken);
            if (refreshedToken == null)
                return Unauthorized(new { message = "Invalid token" });

            TokenManagement.SetTokenCookie(HttpContext, refreshedToken.ToString());

            return Ok(refreshedToken);
        }

        [AllowAnonymous]
        [HttpPost("revoketoken")]
        public IActionResult RevokeToken([FromBody] string token)
        {
            if (string.IsNullOrEmpty(token))
                token = Request.Cookies["refreshToken"];
            if (string.IsNullOrEmpty(token))
                return BadRequest(new { message = "No token supplied" });

            TaskyJManager.RevokeToken(token, TokenManagement.GetIpAddress(HttpContext.Request));

            return Ok();
        }

        [AllowAnonymous]
        [HttpPost("logoff")]
        public IActionResult Logoff([FromBody] string token)
        {/*
            if (string.IsNullOrEmpty(token))
                token = Request.Cookies["refreshToken"];
            if (string.IsNullOrEmpty(token))
                return BadRequest(new { message = "No token supplied" });

            TaskyJManager.RevokeToken(token, TokenManagement.GetIpAddress(HttpContext.Request));
            */
            HttpContext.Response.ContentType = "text/plain";
            return Ok("xxx");
        }
    }
}
