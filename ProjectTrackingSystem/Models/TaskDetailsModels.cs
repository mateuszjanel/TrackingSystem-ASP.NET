using ProjectTrackingSystem.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ProjectTrackingSystem.Models
{
    public class TaskDetailsModels
    {
        [Key] public int ID { get; set; }
        [Required] public string Name { get; set; }
        [DataType(DataType.MultilineText)]
        public string Description { get; set; }
        public string ProjectName { get; set; }
        public enum StatusE
        {
            [Display(Name = "Nowe")]
            New,
            [Display(Name = "Przyjęte")]
            Taken,
            [Display(Name = "W trakcie")]
            InProgress,
            [Display(Name = "Do poprawki")]
            AwaitingFixes,
            [Display(Name = "Zakończone")]
            Done
        }
        public StatusE Status { get; set; }
        public string ReporterName { get; set; }
        public string ReporterId { get; set; }
        public string DevAssigneeName { get; set; }
        public string DevAssigneeId { get; set; }
        public string POAssigneeName { get; set; }
        public string POAssigneeId { get; set; }
        public ICollection<Comment> Comments { get; set; }
        public ICollection<Worklog> Worklogs { get; set; }

        public IEnumerable<SelectListItem> Users { get; set; }
    }
}