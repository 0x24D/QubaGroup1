using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using NUnit;

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
                    Console.ReadLine();
                }

            }
            catch
            {
                Assert.Fail();
            }

        }
    }

    public class CheckLinks
    {
        [TestCase("http://shugroupproject1.quba.co.uk/product")]
        public void TestCase(string url2)
        {
            try
            {
                HttpWebRequest request = (HttpWebRequest)HttpWebRequest.Create(url2);
                request.Timeout = 3000;
                request.AllowAutoRedirect = false; // find out if this site is up and don't follow a redirector
                request.Method = "HEAD";

                using (var response = request.GetResponse())
                {
                    Console.WriteLine("working --  Shugroupproject1.quba.co.uk/products is online");
                    Console.ReadLine();
                }

            }
            catch
            {
                Console.WriteLine("failed --  Shugroupproject1.quba.co.uk/product is not online");
                Console.ReadLine();
            }

        }
    }
}
