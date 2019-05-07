using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using ProjectTrackingSystem.Data;
using ProjectTrackingSystem.Models;

namespace ProjectTrackingSystem.Controllers
{
    [Authorize(Roles = "Administrator")]
    public class AdminPanelController : Controller
    {
        private MainDbContext db = new MainDbContext();
        // GET: AdminPanel
        public ActionResult Index()
        {
            List<ApplicationUser> users = db.Users.ToList();
            var model = new AdminPanelModel()
            {
                Users = db.Users.ToList(),
                Projects = db.Projects.ToList()
            };
            return View(model);
        }

        public ActionResult CreateProject()
        {
            Project project = new Project();
            return View(project);
        }

        [HttpPost]
        public ActionResult CreateProject(Project projectToAdd)
        {
            if (ModelState.IsValid)
            {
                db.Projects.Add(projectToAdd);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(projectToAdd);

        }

        public ActionResult ProjectDetails(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var model = new ProjectDetails_AdminPanelModels
            {
                Project = db.Projects.Find(id)
            };
            if (model.Project != null)
            {
                return View(model);
            }
            return RedirectToAction("Index");
        }

        [HttpPost]
        public ActionResult ProjectDetails(ProjectDetails_AdminPanelModels projectToEdit)
        {
            if (ModelState.IsValid)
            {
                db.Entry(projectToEdit.Project).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(projectToEdit);
        }

        public ActionResult _AddUserToProject(int Id)
        {
            var users = db.Users.ToList().OrderBy(x => x.Surname);
            var list = users.Select(x => new SelectListItem() { Value = x.Id, Text = x.Name + " " + x.Surname }).ToList();
            //var contr = db.Projects.Find(Id).Contributors;

            //foreach(var usr in list)
            //{
            //    if (contr.Contains(users.ToList().Find(usr.Value)))
            //    {

            //    }
            //}

            var model = new AddUserToProjectModels
            {
                ProjectId = Id,
                Users = list,
                Contributors = db.Projects.Find(Id).Contributors
            };
            model.Contributors = model.Contributors.OrderBy(x => x.Surname).ToList();

            return PartialView("_AddUserToProject", model);
        }

        [HttpPost]
        public ActionResult _AddUserToProject(AddUserToProjectModels model)
        {
            if (model.UserToAddId == null)
            {
                return RedirectToAction("ProjectDetails", new { id = model.ProjectId });
            }

            Project project = db.Projects.Find(model.ProjectId);
            ApplicationUser userToAdd = db.Users.Find(model.UserToAddId);

            project.Contributors.Add(userToAdd);
            db.Entry(project).State = System.Data.Entity.EntityState.Modified;
            db.SaveChanges();

            return RedirectToAction("ProjectDetails", new { id = model.ProjectId });
        }

        public ActionResult RemoveUserFromProject(string Id, int projectId)
        {
            Project project = db.Projects.Find(projectId);
            ApplicationUser userToRemove = db.Users.Find(Id);
            project.Contributors.Remove(userToRemove);
            db.Entry(project).State = System.Data.Entity.EntityState.Modified;
            db.SaveChanges();

            return RedirectToAction("ProjectDetails", new { id = projectId });
        }

        public ActionResult RemoveProject(int Id)
        {
            Project project = db.Projects.Find(Id);
            db.Projects.Remove(project);
            db.SaveChanges();

            return RedirectToAction("Index");
        }

        //public ActionResult MakeAdmin(int Id)
        //{
        //    var UserManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(db));
        //    try
        //    {
        //        ApplicationUser user = UserManager.FindById(Id);


        //    }
        //}

        // GET: AdminPanel/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: AdminPanel/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: AdminPanel/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: AdminPanel/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: AdminPanel/Edit/5
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

        // GET: AdminPanel/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: AdminPanel/Delete/5
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
