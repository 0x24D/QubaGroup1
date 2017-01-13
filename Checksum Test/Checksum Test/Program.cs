using System;
using System.IO;
using System.Collections.Generic;

namespace Checksum_Test
{
    class Program
    {
        static string dir1 = @"F:\Download\index";
        static string dir2 = @"F:\Download\indexOld"; // for this testcase, this will act as the previous version of the repo
        static void Main(string[] args)
        {
            List<string> fileKill = new List<string>();
            FileCompare f = new FileCompare();
            List<string> filesList = new List<string>();
            List<string> filessList = new List<string>();
            string tempClonePath = @"F:\Download\index";
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
                Console.WriteLine("Files Match");
            }
            else
            {
                // Environment.Exit(1);
                for (int i = 0; i < fileKill.Count; i++)
                    Console.WriteLine(fileKill[i]);
            }

            Console.WriteLine("Scan Complete, Cleaning Up...");
            DirectoryInfo dirI1 = new DirectoryInfo(dir1);
            setAttributesNormal(dirI1);
            DirectoryInfo dirI2 = new DirectoryInfo(dir2);
            setAttributesNormal(dirI2);
            Directory.Delete(dir2, true); // Will throw exception due to file perms
            Directory.Move(dir1, dir2);
            Console.WriteLine("Cleaned Up, Press Any Key To Continue.");
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