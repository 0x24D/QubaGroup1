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
                HttpWebRequest request = (HttpWebRequest)HttpWebRequest.Create(url1);
                request.Timeout = 3000;
                request.AllowAutoRedirect = false; // find out if this site is up and don't follow a redirector
                request.Method = "HEAD";

                using (var response = request.GetResponse())
                {
                    Console.WriteLine("working --  Shugroupproject1.quba.co.uk is online");
                }

            }
            catch
            {

            }

        }
    }
}
