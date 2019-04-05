using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ProjectTrackingSystem.Data
{
    public class Project
    {
        [Key] public int projectID { get; set; }
        [Required] public string projectName { get; set; }
        public string Description { get; set; }
        public ICollection<Models.ApplicationUser> Contributors { get; set; }

    }
}