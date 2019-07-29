using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Services.Description;

namespace LinkScanner
{
    public class Scanner
    {
        private static string urlPattern = @"http(s)?://([\w-]+\.)+[\w-]+(/[\w- ./?%&=]*)?";
        private static string tagPattern = @"<a\b[^>]*(.*?)</a>";
        private static string emailPattern = @"\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*";


        /// <summary>
        /// gets all links that the url contains
        /// </summary>
        public static List<string> getInnerUrls(string url) {
            List<string> innerUrls = new List<string>();

            //create the WebRequest for url eg "http://www.codeproject.com"
            WebRequest request = WebRequest.Create(url);

            //get the stream from the web response
            StreamReader reader = new StreamReader(request.GetResponse().GetResponseStream());

            //get the htmlCode
            string htmlCode = reader.ReadToEnd();

            List<string> links = getMatches(htmlCode);
            foreach(string link in links) {
                if (!Regex.IsMatch(link,urlPattern) && !Regex.IsMatch(link,emailPattern)) {
                    string absoluteUrlPath = getAblosuteUrl(getDomainName(url), link);
                    innerUrls.Add(absoluteUrlPath);
                }
                else {
                    innerUrls.Add(link);
                }
            }
            return innerUrls;
        }

        /// <summary>
        /// gets all pages links
        /// </summary>
        /// <param name="source">page html code</param>
        /// <returns> list of links </returns>
        private static List<string> getMatches(string source)
        {
            var matchesList = new List<string>();
            //reg expression for A Tag [html] 
            //get the collection that match the pattern
            MatchCollection matches = Regex.Matches(source, tagPattern);
            //add the text under the href attribute
            //to the list
            foreach (Match match in matches)
            {
                string val = match.Value.Trim();
                if (val.Contains("href=\""))
                {
                    string link = getSubstring(val, "href=\"", "\"");
                    matchesList.Add(link);
                }
            }

            return matchesList;
        }

        private static string getSubstring(string source, string start, string end)
        {
            int startIndex = source.IndexOf(start) + start.Length;
            int length = source.IndexOf(end, startIndex) - startIndex;
            return source.Substring(startIndex, length);
        }

        /// <summary>
        /// creates an absolute url for the source whitch the site contains
        /// </summary>
        /// <param name="domainName"></param>
        /// <param name="path">path of the source</param>
        /// <returns></returns>
        private static string getAblosuteUrl(string domainName, string path)
        {
            string absoluteUrl = "";
            if (domainName[domainName.Length - 1] == '/')
            {
                absoluteUrl += domainName;
            }
            else
            {
                absoluteUrl += domainName + "/";
            }

            if (path.Contains("../"))
            {
                string temp = domainName.Substring(0, domainName.LastIndexOf("/", 1));
                temp = temp.Substring(0, temp.LastIndexOf("/", 1));
                absoluteUrl = temp + path.Substring(3);
                return absoluteUrl;
            }
            if (path.Contains("./"))
            {
                string temp = domainName.Substring(0, domainName.LastIndexOf("/", 1));
                absoluteUrl = temp + path.Substring(2);
                return absoluteUrl;
            }
            if (path[0] == '/')
            {
                absoluteUrl += path.Substring(1);
                return absoluteUrl;
            }
            absoluteUrl += path;

            return absoluteUrl;
        }

        private static string getDomainName(string url)
        {
            int length = url.IndexOf("/", 8);
            string domainName = url.Substring(0, length);
            return domainName;
        }

    }
}
