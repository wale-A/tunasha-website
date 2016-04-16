using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace TunashaProjects.Misc
{
    public static class HtmlExtensions
    {
        public static MvcHtmlString TextFormatter (this HtmlHelper htmlHelper, string text){
            if (string.IsNullOrEmpty (text))
                return MvcHtmlString.Create(text);
            
            StringBuilder newText = new StringBuilder();
            string[] lines = text.Split('\n');
            foreach(string line in lines){
                newText.Append(line);
                newText.AppendLine("<br/>");
            }
            return MvcHtmlString.Create(newText.ToString());
        }
    }
}