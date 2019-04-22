using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProjectTrackingSystem.Models
{
    public class ProjectDetails_AdminPanelModels
    {
        public Data.Project Project { get; set; }
        public List<ApplicationUser> Users { get; set; }
    }
}