using System;
using System.Collections.Generic;
using System.Net;
using HtmlAgilityPack;

namespace Link_Test
{
    public class LinkTest
    {
        List<string> links = new List<string>();
        public void Test(string url)
        {
            int j = 0;
            HtmlWeb hw = new HtmlWeb();
            HtmlDocument doc = hw.Load(url);
            foreach (HtmlNode link in doc.DocumentNode.SelectNodes("//a[@href]"))
            {
                string hrefValue = link.GetAttributeValue("href", string.Empty);
                links.Add(hrefValue);
                j++;
                Console.WriteLine(j + " Link(s) found");
                Console.WriteLine("");
            }
            for (int i = 0; i < links.Count; i++)
            {
                Console.WriteLine(links[i]);
            }
            Console.WriteLine("");
            for (int i = 0; i < links.Count; i++)
            {

                try
                {
                    HttpWebRequest request = WebRequest.Create(links[i]) as HttpWebRequest;
                    request.Timeout = 3000;
                    request.AllowAutoRedirect = true; // find out if this site is up and follow a redirector
                    request.Method = "HEAD";

                    using (var response = request.GetResponse())
                    {
                        Console.WriteLine(links[i] + " connected");
                    }
                }
                catch
                {
                    Console.WriteLine(links[i] + " failed");
                }
            }
            Console.ReadLine();
        }
    }
}