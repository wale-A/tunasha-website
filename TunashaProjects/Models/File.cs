using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TunashaProjects.Models
{
    public class File
    {
        public int ID { get; set; }
        public string Text { get; set; }
        public string FilePath { get; set; }
        public DateTime DateAdded { get; set; }

    }
}