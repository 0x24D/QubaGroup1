using System;
using System.IO;
using System.Net;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Http;
using System.Threading.Tasks;
using QubaGroup1;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace QubaGroup1
{
    class Program
    {
        static string URL1 = "http://shugroupproject1.quba.co.uk"; // --  needs to changing so it not hard coded
        static string URL2 = "http://shugroupproject1.quba.co.uk"; // --  needs to changing so it not hard coded
        static string Repository = "https://quba.svn.beanstalkapp.com/shu-group-project-1/";

        //currently being tested, just comment it out if it causes issues.
        static string filePath = @"F:\MyWork\Test"; //Directory.GetCurrentDirectory();

        static string dir1 = @"C:\Users\mikep\Desktop\index";

        static string dir2 = @"C:\Users\mikep\Desktop\indexOld";
            // for this testcase, this will act as the previous version of the repo



        static void Main(string[] args)
        {
            List<string >fileKill = new List<string>();
            PingTest p = new PingTest();
            CompareFiles cmpFiles = new CompareFiles();
            LinkTest check = new LinkTest();
                // added this into PingTest however i am unsure of the potenial uses for it 
            FileComp f = new FileComp();

            List<string> filesList = new List<string>();
            List<string> filessList = new List<string>();

            string tempClonePath = @"C:\Users\mikep\Desktop\index";
            string Repos = @"https://github.com/M1K3L08/QubaWebsiteG1.git";


           f.cloneRepo(Repos, tempClonePath, dir2);





            bool allFilesMatch = true;
            f.DirSearch(dir1, filesList);
            f.DirSearch(dir2, filessList);
            for (int i = 0; i < filesList.Count; i++)
            {
                string sum1 = (f.checkMD5(filesList[i]));
                string sum2 = (f.checkMD5(filessList[i]));
                if (sum1.Equals(sum2))
                {

                }
                else
                {
                    fileKill.Add(filessList[i]);
                    allFilesMatch = false;
                }

            }


            if (allFilesMatch)
            {
                //Environment.Exit(0);
                Console.WriteLine("a");
            }
            else
            {
                // Environment.Exit(1);
               for (int i = 0;i<fileKill.Count;i++)
                    Console.WriteLine(fileKill[i]);
            }

            DirectoryInfo dirI1 = new DirectoryInfo(dir1);
            setAttributesNormal(dirI1);

            DirectoryInfo dirI2 = new DirectoryInfo(dir2);
            setAttributesNormal(dirI2);

            Directory.Delete(dir2, true); // Will throw exception due to file perms

            Directory.Move(dir1, dir2);

 

            Console.ReadLine();
        }


        static void setAttributesNormal(DirectoryInfo dir)
        {
            foreach (var subDir in dir.GetDirectories())
                setAttributesNormal(subDir);
            foreach (var file in dir.GetFiles())
            {
                file.Attributes = FileAttributes.Normal;
            }

        }
    }
}