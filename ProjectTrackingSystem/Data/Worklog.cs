using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ProjectTrackingSystem.Data
{
    public class Worklog
    {
        [Key] public int WorklogId { get; set; }
        public Models.ApplicationUser Logger { get; set; }
        public float LoggedTime { get; set; }
    }
}