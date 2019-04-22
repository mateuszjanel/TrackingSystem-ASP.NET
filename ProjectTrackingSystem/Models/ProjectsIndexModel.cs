using ProjectTrackingSystem.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ProjectTrackingSystem.Models
{
    public class ProjectsIndexModel
    {
       public ICollection<Project> ProjectList { get; set; }
    }
}