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
    public class PingTest
    {
        [TestCase("http://shugroupproject1.quba.co.uk/")]
        public void TestCase(string url1)
        {
            try
            {
                HttpWebRequest request = (HttpWebRequest) HttpWebRequest.Create(url1);
                request.Timeout = 3000;
                request.AllowAutoRedirect = false; // find out if this site is up and don't follow a redirector
                request.Method = "HEAD";

                using (var response = request.GetResponse())
                {
                    Console.WriteLine("working --  Shugroupproject1.quba.co.uk is online");
                    Console.ReadLine();
                }

            }
            catch
            {
                Console.WriteLine("failed --  Shugroupproject1.quba.co.uk is not online");
                Console.ReadLine();
            }

        }
    }

    public class CheckLinks
    {
        List<string> links = new List<string>();

        [TestCase("http://shugroupproject1.quba.co.uk")]
        public void TestCase(string url1)
        {
            HtmlWeb hw = new HtmlWeb();
            HtmlDocument doc = hw.Load("http://shugroupproject1.quba.co.uk/");
            foreach (HtmlNode link in doc.DocumentNode.SelectNodes("//a[@href]"))
            {
                string hrefValue = link.GetAttributeValue("href", string.Empty);
                links.Add(hrefValue);
            }
            for (int i = 0; i < links.Count; i++)
            {
                Console.WriteLine(links[i]);
            }
            Console.ReadLine();

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
                            Console.WriteLine(links[i]);
                            Console.ReadLine();
                        }
                }
                catch
                {
                    Console.WriteLine("failed");
                    Console.ReadLine();
                }
            }
            ////////////////////////////////////
        }
    }
}
