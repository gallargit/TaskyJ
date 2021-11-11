using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Security.Claims;
using TaskyJ.Business;
using TaskyJ.Globals.Data.DataObjects;
using TaskyJ.Interface.AspNet.Models;

namespace TaskyJ.Interface.AspNetCore.Controllers
{
    [Authorize]
    public class TaskyJController : Controller
    {
        private DBSessionJ _currentsession = null;

        public DBSessionJ CurrentSession
        {
            get
            {
                if (_currentsession == null && HttpContext?.User != null)
                {
                    var listClaims = HttpContext.User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.UserData);
                    if (listClaims != null)
                        _currentsession = TaskyJManager.GetDBSessionJ(listClaims.Value);
                }
                return _currentsession;
            }
        }

        public ActionResult Details(IFormCollection collection, int id = 0)
        {
            if (id == 0)
            {
                if (!int.TryParse(collection["TaskList"], out id))
                    id = 0;
            }
            //0 => add new
            if (id != 0)
            {
                var tsk = TaskyJManager.GetTaskById(id);
                if (tsk == null)
                    throw new Exception("id not found");
                else
                {
                    return View(
                        new TaskyJViewModel
                        {
                            ID = tsk.ID,
                            Name = tsk.Name,
                            Description = tsk.Description,
                            Completed = tsk.Completed,
                            CreationDate = tsk.CreationDate,
                            FullName = tsk.ToString()
                        });
                }
            }
            else
            {
                return View(new TaskyJViewModel());
            }
        }

        public ActionResult Index()
        {
            var lst = new TaskyJListViewModel();

            TaskyJManager.RetrieveTasks(CurrentSession).ToList().ForEach(tsk =>
                lst.TaskList.Add(
                    new TaskyJViewModel
                    {
                        ID = tsk.ID,
                        Name = tsk.Name,
                        Description = tsk.Description,
                        Completed = tsk.Completed,
                        FullName = tsk.ToString()
                    }
            ));

            return View(lst);
        }

        public ActionResult Save(TaskyJViewModel t)
        {
            TaskyJManager.PushTask(
                new DBTaskJ
                {
                    ID = t.ID,
                    Name = t.Name,
                    Description = t.Description,
                    Completed = t.Completed
                }, CurrentSession);
            return RedirectToAction("Index");
        }

        public ActionResult Create()
        {
            return View();
        }

        public ActionResult Delete(int id)
        {
            if (id != 0)
                TaskyJManager.RemoveTask(id);
            return RedirectToAction("Index");
        }
    }
}
