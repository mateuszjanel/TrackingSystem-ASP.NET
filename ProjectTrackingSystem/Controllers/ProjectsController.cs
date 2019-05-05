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
    public class ProjectsController : Controller
    {
        private MainDbContext db = new MainDbContext();

        // GET: Projects
        public ActionResult Index()
        {
            var userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(db));
            ApplicationUser user = userManager.FindById(User.Identity.GetUserId());
            //List<Project> allProjects = db.Projects.ToList();
            var model = new ProjectsIndexModel
            {
                ProjectList = db.Projects.Where(p => p.Contributors.Where(c => c.Id == user.Id).ToList().Count != 0).ToList()
            };

            return View(model);
        }

        public ActionResult ProjectSite(int? id, string sortOrder)
        {
            if (id == null)
            {
                return Redirect("Index");
            }
            var userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(db));
            ApplicationUser user = userManager.FindById(User.Identity.GetUserId());
            Project chosenProject = db.Projects.Find(id);
            //Tasks sort
            var tasks = chosenProject.Tasks;
            ViewBag.NameSortParm = String.IsNullOrEmpty(sortOrder) ? "Id_desc" : "";
            ViewBag.DateSortParm = sortOrder == "Name" ? "name_desc" : "Name";

            switch (sortOrder)
            {
                case "name_desc":
                    tasks = tasks.OrderByDescending(s => s.Name).ToList();
                    break;
                case "Name":
                    tasks = tasks.OrderBy(s => s.Name).ToList();
                    break;
                case "Id_desc":
                    tasks = tasks.OrderByDescending(s => s.ID).ToList();
                    break;
                default:
                    tasks = tasks.OrderBy(s => s.ID).ToList();
                    break;
            }

            //-------------------------------
            if (chosenProject.Contributors.Contains(user))
            {
                var model = new ProjectSiteModels
                {
                    projectID = chosenProject.projectID,
                    projectName = chosenProject.projectName,
                    Description = chosenProject.Description,
                    Contributors = chosenProject.Contributors,
                    Tasks = tasks,
                    IsUserContributor = true
                };
                return View(model);
            }
            return RedirectToAction("Index");
        }

        public ActionResult CreateTask(int? projectId)
        {
            var userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(db));
            ApplicationUser user = userManager.FindById(User.Identity.GetUserId());
            Project chosenProject = db.Projects.Find(projectId);
            if (projectId != null && chosenProject.Contributors.Contains(user))
            {
                var task = new CreateTaskModels();
                task.ProjectId = (int)projectId;
                task.Project = chosenProject;
                var users = chosenProject.Contributors.ToList().OrderBy(x => x.Surname);
                var list = users.Select(x => new SelectListItem() { Value = x.Id, Text = x.Name + " " + x.Surname });
                task.Users = list;
                return View(task);
            }
            return RedirectToAction("Index");
        }

        [HttpPost]
        public ActionResult CreateTask(CreateTaskModels taskToAdd)
        {
            if (ModelState.IsValid)
            {
                var userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(db));
                ApplicationUser user = userManager.FindById(User.Identity.GetUserId());
                var DevId = taskToAdd.DevAssigneeId;
                var POId = taskToAdd.POAssigneeId;
                var task = new Task
                {
                    Name = taskToAdd.Name,
                    Description = taskToAdd.Description,
                    Status = (Task.StatusE)taskToAdd.Status,
                    Project = db.Projects.Find(taskToAdd.ProjectId),
                    Reporter = user,
                    DevAssignee = db.Users.Find(taskToAdd.DevAssigneeId),
                    POAssignee = db.Users.Find(taskToAdd.POAssigneeId)
                };
                db.Tasks.Add(task);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(taskToAdd);
        }

        public ActionResult TaskDetails(int? Id)
        {
            if (Id == null)
            {
                return RedirectToAction("Index");
            }

            Task task = db.Tasks.Find(Id);
            if (task == null)
            {
                return RedirectToAction("Index");
            }

            var userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(db));
            ApplicationUser user = userManager.FindById(User.Identity.GetUserId());
            Project projectFromTask = task.Project;
            if (projectFromTask.Contributors.Contains(user))
            {
                var users = projectFromTask.Contributors.ToList().OrderBy(x => x.Surname);
                var list = users.Select(x => new SelectListItem() { Value = x.Id, Text = x.Name + " " + x.Surname });
                var model = new TaskDetailsModels
                {
                    ID = task.ID,
                    Name = task.Name,
                    Description = task.Description,
                    ProjectName = task.Project.projectName,
                    Status = (Models.TaskDetailsModels.StatusE)task.Status,
                    ReporterName = task.Reporter.Name + " " + task.Reporter.Surname,
                    ReporterId = task.Reporter.Id,
                    Comments = task.Comments,
                    Worklogs = task.Worklogs,
                    Users = list
                };
                if (task.DevAssignee != null)
                {
                    model.DevAssigneeName = task.DevAssignee.Name + " " + task.DevAssignee.Surname;
                    model.DevAssigneeId = task.DevAssignee.Id;
                }
                if (task.POAssignee != null)
                {
                    model.POAssigneeName = task.POAssignee.Name + " " + task.POAssignee.Surname;
                    model.POAssigneeId = task.POAssignee.Id;
                }
                return View(model);
            }
            return RedirectToAction("Index");

        }

        [HttpPost]
        public ActionResult TaskDetails(TaskDetailsModels taskToEdit)
        {
            if (ModelState.IsValid)
            {
                var taskFromDB = db.Tasks.Find(taskToEdit.ID);
                //var task = new Task
                //{
                //    ID = taskToEdit.ID,
                //    Name = taskToEdit.Name,
                //    Description = taskToEdit.Description,
                //    Status = (Task.StatusE)taskToEdit.Status,
                //    Reporter = db.Users.Find(taskToEdit.ReporterId),
                //    DevAssignee = db.Users.Find(taskToEdit.DevAssigneeId),
                //    POAssignee = db.Users.Find(taskToEdit.POAssigneeId),
                //    Project = taskFromDB.Project,
                //    Comments = taskFromDB.Comments,
                //    Worklogs = taskFromDB.Worklogs
                //};

                taskFromDB.Name = taskToEdit.Name;
                taskFromDB.Description = taskToEdit.Description;
                taskFromDB.DevAssignee = db.Users.Find(taskToEdit.DevAssigneeId);
                taskFromDB.POAssignee = db.Users.Find(taskToEdit.POAssigneeId);
                taskFromDB.Status = (Task.StatusE)taskToEdit.Status;

                db.Entry(taskFromDB).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("TaskDetails", new { Id = taskToEdit.ID });
            }
            return View(taskToEdit);
        }

        // GET: Projects/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Projects/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Projects/Create
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

        // GET: Projects/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Projects/Edit/5
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

        // GET: Projects/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Projects/Delete/5
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
