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
        public void TestCase(string url)
        {
            try
            {
                HttpWebRequest request = (HttpWebRequest)HttpWebRequest.Create(url);
                request.Timeout = 3000;
                request.AllowAutoRedirect = false; // find out if this site is up and don't follow a redirector
                request.Method = "HEAD";

                using (var response = request.GetResponse())
                {

                }

            }
            catch
            {
                Assert.Fail();
            }

        }
    }
}
