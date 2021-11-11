using Microsoft.AspNetCore.Mvc;
using System;

namespace TaskyJ.Business.API.AspNetCore.Controllers
{
    public class HomeController : Controller
    {
        // GET: HomeController
        //[ValidateAntiForgeryToken]
        public ActionResult Index()
        {
            //return Content("Alive");
            var urls = new string[]
            {
                "/swagger/index.html",
                "/swagger/v1/swagger.json",
                "/api/taskyj/GetMaster",
                "/api/categoryj",
            };

            var str = "";
            var host = Request.Scheme + "://" + Request.Host.ToString();
            if (!string.IsNullOrEmpty(Request.Headers["x-forwarded-host"]))
                host = Request.Headers["x-forwarded-proto"].ToString() + "://" + Request.Headers["x-forwarded-host"].ToString();
            foreach (var s in urls)
            {
                str += "<a target=\"_blank\" href=\"" + host + s + "\">" + host + s + "</a><br/><br/>" + Environment.NewLine;
            }
            return Content(str, Microsoft.Net.Http.Headers.MediaTypeHeaderValue.Parse("text/html"));
        }
    }
}
