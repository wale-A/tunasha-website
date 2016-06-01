using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TunashaProjects.Misc
{
    public static class MyExtensionMethods
    {
        public static bool ContainsForSearch(this string text, string query)
        {
            if (String.IsNullOrWhiteSpace(query))
                return false;

            //int begin = query.IndexOf("\"");
            //int end = query.LastIndexOf("\"");
            if (query.StartsWith("\"") && query.EndsWith("\""))
            {
                query = query.Remove(0, 1);
                query = query.Remove(query.Length -1);
                if (text.ToLower().Contains(query.ToLower()))
                    return true;
                return false;
            }

            string[] queryString = query.ToLower().Split(' ');
            string lowerText = text.ToLower();

            foreach (string s in queryString)
                if (lowerText.Contains(s))
                        return true;

            return false;
        }
    }
}