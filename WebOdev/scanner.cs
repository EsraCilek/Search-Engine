using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text.RegularExpressions;
using System.Web;
using System.Collections;

namespace WebOdev
{
    public static class scanner
    {
        private static string urlPattern = @"http(s)?://([\w-]+\.)+[\w-]+(/[\w- ./?%&=]*)?";
        private static string tagPattern = @"<a\b[^>]*(.*?)</a>";
        private static string emailPattern = @"\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*";


        public static ArrayList Url(string url)
        {
            ArrayList Url = new ArrayList();
            WebRequest request = WebRequest.Create(url);            
            StreamReader reader = new StreamReader(request.GetResponse().GetResponseStream());

            string htmlCode = reader.ReadToEnd();

            ArrayList links = Eslesme(htmlCode);
            foreach (string link in links)
            {
                if (link == null || link.Trim() == "")
                { }
                else { 
                    if (!Regex.IsMatch(link, urlPattern) && !Regex.IsMatch(link, emailPattern))
                    {
                        string yol = AnaUrl(getDomainName(url), link);
                        Url.Add(yol);
                    }
                    else
                    {
                        Url.Add(link);
                    }
                }
            }
            return Url;
        }


        private static ArrayList Eslesme(string source)
        {
            var matchesList = new ArrayList();
        
            MatchCollection matches = Regex.Matches(source, tagPattern);
            foreach (Match match in matches)
            {
                string val = match.Value.Trim();
                if (val.Contains("href=\""))
                {
                    string link = Substring(val, "href=\"", "\"");
                    matchesList.Add(link);
                }
            }

            return matchesList;
        }

        private static string Substring(string source, string baslangıc, string bitis)
        {
            int baslangıcIndex = source.IndexOf(baslangıc) + baslangıc.Length;
            int length = source.IndexOf(bitis, baslangıcIndex) - baslangıcIndex;
            return source.Substring(baslangıcIndex, length);
        }

      
        private static string AnaUrl(string domainName, string path)
        {
            string anaUrl = "";
            if (domainName[domainName.Length - 1] == '/')
            {
                anaUrl += domainName;
            }
            else
            {
                anaUrl += domainName + "/";
            }

            if (path.Contains("../"))
            {
                string temp = domainName.Substring(0, domainName.LastIndexOf("/", 1));
                temp = temp.Substring(0, temp.LastIndexOf("/", 1));
                anaUrl = temp + path.Substring(3);
                return anaUrl;
            }
            if (path.Contains("./"))
            {
                string temp = domainName.Substring(0, domainName.LastIndexOf("/", 1));
                anaUrl = temp + path.Substring(2);
                return anaUrl;
            }
            if (path[0] == '/')
            {
                anaUrl += path.Substring(1);
                return anaUrl;
            }
            anaUrl += path;

            return anaUrl;
        }

        public static string getDomainName(string url)
        {
            int length = url.IndexOf("/", 8);
            string domainName = url.Substring(0, length);
            return domainName;
        }
    }
}