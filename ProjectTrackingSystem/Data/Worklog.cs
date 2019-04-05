using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProjectTrackingSystem.Data
{
    public class Worklog
    {
        public int ID { get; set; }
        public Models.ApplicationUser Logger { get; set; }
        public float LoggedTime { get; set; }
    }
}