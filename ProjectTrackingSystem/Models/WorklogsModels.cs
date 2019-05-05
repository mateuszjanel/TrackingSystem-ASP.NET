using ProjectTrackingSystem.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ProjectTrackingSystem.Models
{
    public class WorklogsModels
    {
        [Key] public int WorklogId { get; set; }
        [Required]
        public float LoggedTime { get; set; }
        public virtual int TaskId { get; set; }
        public ICollection<Worklog> Worklogs { get; set; }
        public float TaskTime { get; set; }
    }
}