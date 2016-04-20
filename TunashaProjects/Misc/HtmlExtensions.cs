using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using TunashaProjects.Models;

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

        public static MvcHtmlString ImageFormatter(this HtmlHelper htmlHelper, PostedFile image)
        {
            StringBuilder newText = new StringBuilder();
            if (image != null)
            {
                newText.Append(@"<img class='img-responsive' src=");
                //newText.Append(HttpContext.Current.Server.MapPath(image.FilePath));
                newText.Append(image.FilePath);
                newText.Append(@" alt=");
                newText.Append(image.Text);
                newText.Append(@"'>");
            }
            return MvcHtmlString.Create(newText.ToString());
        }
    }
}