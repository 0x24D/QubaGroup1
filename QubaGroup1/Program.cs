using System;
using System.Net;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Http;
using System.Threading.Tasks;
using NUnit;
using QubaGroup1;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;

namespace QubaGroup1
{
    class Program
    {
        static string URL1 = "http://shugroupproject1.quba.co.uk"; // --  needs to changing so it not hard coded
        static string URL2 = "http://shugroupproject1.quba.co.uk"; // --  needs to changing so it not hard coded
        static string Repository = "https://quba.svn.beanstalkapp.com/shu-group-project-1/";

        //currently being tested, just comment it out if it causes issues.
         static string filePath = @"F:\MyWork\Test";//Directory.GetCurrentDirectory();

        static void Main(string[] args)
        {
            PingTest p = new PingTest();
            CompareFiles cmpFiles = new CompareFiles();
            LinkTest check = new LinkTest(); // added this into PingTest however i am unsure of the potenial uses for it 

            p.Ping(URL1);
            check.TestCase(URL2);
            cmpFiles.getFileDetails(Repository, filePath);
            cmpFiles.compareTheFiles(URL1, Repository, filePath);
            string sum1 = cmpFiles.CalculateMd5Hash("C:\\Users\\b5021991\\Desktop\\Test.txt");
            string sum2 = cmpFiles.CalculateMd5Hash("C:\\Users\\b5021991\\Desktop\\Test2.txt");
            if (sum1.Equals(sum2))
                Console.WriteLine("MD5sums match");
            else
                Console.WriteLine("MD5sums do not match");
            Console.ReadLine();
        }
        
    }
}