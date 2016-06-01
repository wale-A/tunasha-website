using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace TunashaProjects.Models
{
    public class PostedFile
    {
        public int ID { get; set; }
        public string Text { get; set; }
        [DataType (DataType.Upload)]
        public string FilePath { get; set; }
        public DateTime DateAdded { get; set; }

    }
}