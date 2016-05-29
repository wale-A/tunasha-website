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

        public static MvcHtmlString TimeDifference(this HtmlHelper htmlHelper, DateTime dateSent)
        {
            StringBuilder newText = new StringBuilder();
            var date = DateTime.Now.ToUniversalTime() - dateSent.ToUniversalTime();
            if (date.Days > 1) {
                newText.Append(date.Days.ToString());
                newText.Append(" days");
            }
            else if (date.Days > 0)
            {
                newText.Append(date.Days.ToString());
                newText.Append(" day");
            }
            else if (date.Hours > 1)
            {
                newText.Append(date.Hours.ToString());
                newText.Append(" hours");
            }
            else if (date.Hours > 0)
            {
                newText.Append(date.Hours.ToString());
                newText.Append(" hour");
            }
            else if (date.Minutes > 1)
            {
                newText.Append(date.Minutes.ToString());
                newText.Append(" minutes");
            }
            else if (date.Minutes > 0)
            {
                newText.Append(date.Minutes.ToString());
                newText.Append(" minute");
            }
            else if (date.Seconds > 1)
            {
                newText.Append(date.Seconds.ToString());
                newText.Append(" seconds");
            }
            else if (date.Seconds > 0)
            {
                newText.Append(date.Seconds.ToString());
                newText.Append(" second");
            }
            newText.Append(" ago");
            return MvcHtmlString.Create(newText.ToString());
        }

        public static MvcHtmlString CommentState(this HtmlHelper htmlHelper, bool commentState)
        {
            StringBuilder newText = new StringBuilder();
            if (commentState)
                return MvcHtmlString.Create("enable comments");
            return MvcHtmlString.Create("disable comments");
        }
    }
}