using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace TunashaProjects.Models
{
    public class Question
    {
        public Question()
        {
            Replies = new List<Reply>();
        }

        public int ID { get; set; }
        [Required, Display(Name = "Question")]
        [UIHint("tinymce_jquery_full"), AllowHtml]
        public string Text { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Email { get; set; }
        //[ScaffoldColumn(false)]
        public DateTime Date { get; set; }

        public string MiniText
        {
            get
            {
                if (Text.Length > 200)
                    return Text.Substring(0, 200);
                else return Text.Substring(0, Text.Length);
            }
        }
        public virtual ICollection<Reply> Replies { get; set; }
    }

    public class Reply
    {
        public int ID { get; set; }
        [Required, Display (Name = "Reply")]
        [UIHint("tinymce_jquery_full"), AllowHtml]
        public string Text { get; set; }
        //[Required]
        public string Name { get; set; }
        //[ScaffoldColumn(false)]
        public DateTime Date { get; set; }

        public int QuestionID { get; set; }
        public int UserID { get; set; }
    }

    public class QuestionViewModel
    {
        [UIHint("tinymce_jquery_full"), AllowHtml, Required, Display(Name = "Question")]
        public string Text { get; set; }
        [Required, Display(Name = "Full Name")]
        public string Name { get; set; }
        [Required, DataType(DataType.EmailAddress), Display(Name = "Email Address")]
        public string Email { get; set; }
        //[ScaffoldColumn(false)]
        public DateTime Date { get; set; }

    }


}