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
        public bool DisableComment { get; set; }

        [ScaffoldColumn (false)]
        public string MiniContent { get { return this.Content.Substring(0, 150) + "...."; } }

        public int UserID { get; set; }
        public virtual ICollection<Comment> Comments { get; set; }
        public virtual ICollection<PostedFile> Images { get; set; }
        public virtual User PostedBy { get; set; }
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
        [Required]
        public string Text { get; set; }
        [Required]
        public string Name { get; set; }
        public DateTime Date { get; set; }

        public int PostID { get; set; }
    }

    public class CommentViewModel
    {
        [Required]
        public string Name { get; set; }
        [Required, DataType(DataType.MultilineText)]
        public string Text { get; set; }
    }
}