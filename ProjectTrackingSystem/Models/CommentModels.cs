using ProjectTrackingSystem.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ProjectTrackingSystem.Models
{
    public class CommentModels
    {
        [Key] public int CommentId { get; set; }
        [Required]
        [DataType(DataType.MultilineText)]
        public string Content { get; set; }
        public virtual int TaskId { get; set; }
        //public virtual Task Task { get; set; }
        public ICollection<Comment> Comments { get; set; }
    }
}