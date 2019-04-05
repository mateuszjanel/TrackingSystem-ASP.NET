using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ProjectTrackingSystem.Data
{
    public class Comment
    {
        [Key] public int ID { get; set; }
        public Models.ApplicationUser Author { get; set; }
        public DateTime CreationTime { get; set; }
        public string Content { get; set; }
    }
}