using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace TunashaProjects.Models
{
    public class Post
    {
        public Post()
        {
            Comments = new List<Comment>();
            Images = new List<PostedFile>();
        }

        [Key]
        public int PostID { get; set; }
        [Required]
        public string Title { get; set; }
        [Required, DataType(DataType.MultilineText)]
        public string Content { get; set; }
        public DateTime Date { get; set; }

        public int UserID { get; set; }
        public virtual ICollection<Comment> Comments { get; set; }
        public virtual ICollection<PostedFile> Images { get; set; }
    }

    public class PostViewModel
    {
        [Required]
        public string Title { get; set; }
        [Required, DataType(DataType.MultilineText)]
        public string Content { get; set; }
    }

    public class Comment
    {
        [Key]
        public int CommentID { get; set; }
        [Required, DataType(DataType.MultilineText)]
        public string Text { get; set; }
        [Required, Display(Name = "Full Name")]
        public string Name { get; set; }
        [Required, DataType(DataType.EmailAddress), Display(Name = "Email Address")]
        public string Email { get; set; }
        public DateTime Date { get; set; }

        public int PostID { get; set; }
    }
}