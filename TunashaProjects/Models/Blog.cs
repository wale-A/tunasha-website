using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using System.Net;
using System.Text.RegularExpressions;
using System.ComponentModel.DataAnnotations.Schema;

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

        [ScaffoldColumn (false), AllowHtml]
        //public string MiniContent { get { return this.Content.Substring(0, 150) + "...."; } }
        public string MiniContent { get { return WebUtility.HtmlDecode(Regex.Replace(this.Content, "<[^>]*(>|$)", string.Empty)).Substring(0, 200); } }
        

        public int UserID { get; set; }
        public virtual ICollection<Comment> Comments { get; set; }
        public virtual ICollection<PostedFile> Images { get; set; }
        public virtual User PostedBy { get; set; }
    }

    public class PostViewModel
    {
        [Required]
        public string Title { get; set; }
        [Required, UIHint("tinymce_jquery_full"), AllowHtml]
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

        [Required]
        public int PostID { get; set; }
        public virtual Post BlogPost { get; set; }
    }

    public class CommentViewModel
    {
        [Required]
        public string Name { get; set; }
        [Required, UIHint("tinymce_jquery_full"), AllowHtml]
        public string Text { get; set; }
    }
}