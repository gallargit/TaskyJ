using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using TaskyJ.Business.API.AspNetCore.Library;
using TaskyJ.Globals.Data.DataObjects;
using TaskyJ.Interface.AspNet.Models;

namespace TaskyJ.Interface.AspNetCore.Controllers
{
    [Authorize]
    public class AuthController : Controller
    {
        [AllowAnonymous]
        public IActionResult Index()
        {
            return View();
        }

        [AllowAnonymous]
        [HttpPost]
        public IActionResult Authenticate(AuthViewModel authmodel)
        {
            var model = new DBUserJ
            {
                UserName = authmodel.UserName,
                Password = authmodel.Password
            };

            var session = TokenManagement.DoAuthenticate(HttpContext, model);
            if (session == null)
            {
                ViewData["Error"] = "Wrong login";
                return Index();
            }

            var userClaims = new List<Claim>()
            {
                new Claim(ClaimTypes.Email, "demo@demo.com"),
                new Claim(ClaimTypes.UserData, session.RefreshToken.ToString())
            };
            var userIdentity = new ClaimsIdentity(userClaims, "User Identity");
            var userPrincipal = new ClaimsPrincipal(new[] { userIdentity });
            HttpContext.SignInAsync(userPrincipal);

            try
            {
                var uri = new Uri(HttpContext.Request.Headers["Referer"]);
                var redirect = System.Web.HttpUtility.ParseQueryString(uri.Query).Get("ReturnUrl");
                if (!string.IsNullOrEmpty(redirect))
                    return Redirect(redirect);
            }
            catch { }

            return RedirectToAction("Index", "TaskyJ");
        }
    }
}
