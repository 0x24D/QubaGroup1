using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Link_Test
{
    class Program
    {
        static void Main(string[] args)
        {
            string url = "http://shugroupproject1.quba.co.uk";
            LinkTest linkTest = new LinkTest();
            linkTest.Test(url);
        }
    }
}
