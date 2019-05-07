using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ProjectTrackingSystem.Models
{
    public class AddUserToProjectModels
    {
        public int ProjectId { get; set; }
        public IEnumerable<SelectListItem> Users { get; set; }
        public string UserToAddId { get; set; }
        public ICollection<ApplicationUser> Contributors { get; set; }
    }
}