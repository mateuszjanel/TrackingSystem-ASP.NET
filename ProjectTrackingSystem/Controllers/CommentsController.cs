using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using ProjectTrackingSystem.Data;
using ProjectTrackingSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ProjectTrackingSystem.Controllers
{
    [Authorize]
    public class CommentsController : Controller
    {

        private MainDbContext db = new MainDbContext();


        public ActionResult _CreateComment(int taskId)
        {
            var model = new CommentModels();
            model.TaskId = (int)taskId;
            //model.Task = db.Tasks.Find(taskId);
            model.Comments = db.Tasks.Find(taskId).Comments;
            return PartialView("_CreateComment", model);
        }

        [HttpPost]
        public ActionResult _CreateComment(CommentModels commentToAdd)
        {
            if (ModelState.IsValid)
            {
                var userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(db));
                ApplicationUser user = userManager.FindById(User.Identity.GetUserId());
                var comment = new Comment
                {
                    Author = user,
                    CreationTime = DateTime.Now,
                    Content = commentToAdd.Content,
                    Task = db.Tasks.Find(commentToAdd.TaskId)
                };
                db.Comments.Add(comment);
                db.SaveChanges();
            }

            var model = new CommentModels
            {
                TaskId = commentToAdd.TaskId,
                Comments = db.Tasks.Find(commentToAdd.TaskId).Comments
            };
            //return PartialView("_CreateComment", model);
            return RedirectToAction("TaskDetails", "Projects", new { Id = commentToAdd.TaskId });
        }

        public ActionResult _CreateWorklog(int taskId)
        {
            var model = new WorklogsModels
            {
                TaskId = taskId,
                Worklogs = db.Tasks.Find(taskId).Worklogs
            };
            float taskTime = 0f;
            foreach (var wl in model.Worklogs)
            {
                taskTime += wl.LoggedTime;
            }
            model.TaskTime = taskTime;
            return PartialView("_CreateWorklog", model);
        }

        [HttpPost]
        public ActionResult _CreateWorklog(WorklogsModels worklogToAdd)
        {
            if (ModelState.IsValid)
            {
                var userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(db));
                ApplicationUser user = userManager.FindById(User.Identity.GetUserId());
                var worklog = new Worklog
                {
                    Logger = user,
                    CreationTime = DateTime.Now,
                    LoggedTime = worklogToAdd.LoggedTime,
                    Task = db.Tasks.Find(worklogToAdd.TaskId)
                };
                db.Worklogs.Add(worklog);
                db.SaveChanges();
            }

            var model = new WorklogsModels
            {
                TaskId = worklogToAdd.TaskId,
                Worklogs = db.Tasks.Find(worklogToAdd.TaskId).Worklogs
            };
            float taskTime = 0f;
            foreach (var wl in model.Worklogs)
            {
                taskTime += wl.LoggedTime;
            }
            model.TaskTime = taskTime;

            //return PartialView("_CreateWorklog", model);
            return RedirectToAction("TaskDetails", "Projects", new { Id = worklogToAdd.TaskId });
        }

        // GET: Comments/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Comments/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Comments/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Comments/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
