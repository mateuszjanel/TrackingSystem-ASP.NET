using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ProjectTrackingSystem.Data
{
    public class Task
    {
        [Key] public int ID { get; set; }
        [Required] public string Name { get; set; }
        public string Description { get; set; }
        public Models.ApplicationUser Reporter { get; set; }
        public Models.ApplicationUser DevAssignee { get; set; }
        public Models.ApplicationUser POAssignee { get; set; }
        public ICollection<Comment> Comments { get; set; }
        public ICollection<Worklog> Worklogs { get; set; }

    }
}