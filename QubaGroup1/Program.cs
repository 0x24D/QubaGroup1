using System;
using System.Net;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Http;
using System.Threading.Tasks;
using NUnit;

namespace QubaGroup1
{
    class Program
    {
        static string URL1 = "http://shugroupproject1.quba.co.uk"; // --  needs to changing so it not hard coded
        static string URL2 = "http://shugroupproject1.quba.co.uk"; // --  needs to changing so it not hard coded

        static string Repository = "https://quba.svn.beanstalkapp.com/shu-group-project-1/";
        static string filePath;
        static void Main(string[] args)
        {
            PingTest ping = new PingTest();
            CompareFiles CmpFiles = new CompareFiles();
            CheckLinks check = new CheckLinks(); // added this into PingTest however i am unsure of the potenial uses for it 

            ping.TestCase(URL1);
            check.TestCase(URL2);
            CmpFiles.getFileDetails(Repository, filePath);
           
        }
        
    }
}
