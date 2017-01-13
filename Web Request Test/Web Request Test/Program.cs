using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Web_Request_Test
{
    class Program
    {
        static void Main(string[] args)
        {
            string url = "http://shugroupproject1.quba.com";
            PingTest pingTest = new PingTest();
            pingTest.Ping(url);
        }
    }
}
