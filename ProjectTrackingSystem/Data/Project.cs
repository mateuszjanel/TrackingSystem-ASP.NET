﻿using ProjectTrackingSystem.Models;
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
        [DataType(DataType.MultilineText)]
        public string Description { get; set; }
        public virtual ICollection<ApplicationUser> Contributors { get; set; }
        public virtual ICollection<Task> Tasks { get; set; }

    }
}