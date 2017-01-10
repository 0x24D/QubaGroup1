using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Http;
using System.Threading.Tasks;
using System.Net;
using NUnit;

namespace QubaGroup1
{
    class Program
    {
        static string URL = "http://shugroupproject1.quba.co.uk/";
        static string Repository = "https://quba.svn.beanstalkapp.com/shu-group-project-1/";
        static string filePath;
        static void Main(string[] args)
        {
            PingTest ping = new PingTest();
            CompareFiles CmpFiles = new CompareFiles();
            ping.TestCase(URL);
            CmpFiles.getFileDetails(Repository, filePath);
        }
        
    }
}
