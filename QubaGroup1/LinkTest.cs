using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using NUnit;
using HtmlAgilityPack;
using Microsoft.SqlServer.Server;

namespace QubaGroup1
{
    public class LinkTest
    {
        List<string> links = new List<string>();
        [TestCase("http://shugroupproject1.quba.co.uk")]
        public void TestCase(string url2)
        {
            int j = 0;
            HtmlWeb hw = new HtmlWeb();
            HtmlDocument doc = hw.Load(url2);
            foreach (HtmlNode link in doc.DocumentNode.SelectNodes("//a[@href]"))
            {
                string hrefValue = link.GetAttributeValue("href", string.Empty);
                links.Add(hrefValue);
                j++;
                Console.WriteLine(j + " Link(s) found");
            }
            for (int i = 0; i < links.Count; i++)
            {
                Console.WriteLine(links[i]);
            }

            ////////////////////////////////////ping each hyperlink
            for (int i = 0; i < links.Count; i++)
            {
                try
                {
                        HttpWebRequest request = WebRequest.Create(links[i]) as HttpWebRequest;
                        request.Timeout = 3000;
                        request.AllowAutoRedirect = true; // find out if this site is up and don't follow a redirector
                        request.Method = "HEAD";

                        using (var response = request.GetResponse())
                        {
                            Console.WriteLine(links[i] + " connected");
                        }
                }
                catch
                {
                    Console.WriteLine("failed");
                }
            }
        }
    }
}
