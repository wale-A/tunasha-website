using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace TunashaProjects.Models
{
    public class Question
    {
        public Question()
        {
            Replies = new List<Reply>();
        }

        public int ID { get; set; }
        [DataType(DataType.MultilineText), Required, Display(Name = "Question")]
        public string Text { get; set; }
        [Required, Display(Name = "Full Name")]
        public string Name { get; set; }
        [Required, DataType(DataType.EmailAddress), Display(Name = "Email Address")]
        public string Email { get; set; }
        //[ScaffoldColumn(false)]
        public DateTime Date { get; set; }

        public virtual ICollection<Reply> Replies { get; set; }
    }

    public class Reply
    {
        public int ID { get; set; }
        [DataType(DataType.MultilineText), Required, Display (Name = "Reply")]
        public string Text { get; set; }
        //[Required]
        public string Name { get; set; }
        //[ScaffoldColumn(false)]
        public DateTime Date { get; set; }

        public int QuestionID { get; set; }
        public int UserID { get; set; }
    }
}