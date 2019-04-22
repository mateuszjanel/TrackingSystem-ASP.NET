﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
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
        public Models.ApplicationUser Reporter { get; set; }
        public Models.ApplicationUser DevAssignee { get; set; }
        public Models.ApplicationUser POAssignee { get; set; }
        public ICollection<Comment> Comments { get; set; }
        public ICollection<Worklog> Worklogs { get; set; }
        public Project Project { get; set; }

    }
}