using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ProjectTrackingSystem.Data
{
    public class Comment
    {
        [Key] public int CommentId { get; set; }
        public virtual Models.ApplicationUser Author { get; set; }
        public DateTime CreationTime { get; set; }
        [Required][DataType(DataType.MultilineText)]
        public string Content { get; set; }
        public virtual Task Task { get; set; }
    }
}