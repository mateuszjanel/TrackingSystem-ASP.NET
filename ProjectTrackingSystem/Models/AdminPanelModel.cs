using ProjectTrackingSystem.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProjectTrackingSystem.Models
{
    public class AdminPanelModel
    {
        public List<ApplicationUser> Users { get; set; }
        public List<Project> Projects { get; set; }
    }
}