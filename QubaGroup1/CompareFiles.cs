﻿using System;
using System.IO;
using System.Web;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LibGit2Sharp; //Allows us to read the beanstalk git files
using NUnit.Framework;
using NUnit;

namespace QubaGroup1
{
    class CompareFiles
    {

        //Creates a "file" which can tell us the name and it's status in the latest commit.
        public class file
        {
            public string Name {get; set;}
            public ChangeKind State {get; set;}
            public DateTime LastModified {get;set;}
        }
        List<file> files = new List<file>();
        file SpecificFile;

        CompareFiles(string FileName)
        {
            this.SpecificFile.Name = FileName;
        }
        public CompareFiles()
        {
        }

        //check names of "to be deployed" files
        //Probably using files that have been edited/added/deleted recently ie. today.        
        //[TestCase("")]
        public void getFileDetails(string Repos, string filePath)
        {
            string User = "b5010811";
            string Pass = "310e95ed404ab86756d75833d9a3689bbbb99aaef2536af0b2";
            filePath = @"F:\MyWork\Test";//Directory.GetCurrentDirectory();
            try
            {
                CloneOptions co = new CloneOptions();
                co.CredentialsProvider = (_url, _user, _cred) =>
                new UsernamePasswordCredentials { Username = User, Password = Pass };

                using (Repository repo = new Repository(Repository.Clone(Repos, filePath, co)))
                {
                    Tree commitTree = repo.Head.Tip.Tree; // Main Tree
                    Tree parentCommitTree = repo.Head.Tip.Parents.Single().Tree; // Secondary Tree

                    var patch = repo.Diff.Compare<Patch>(parentCommitTree, commitTree); // Difference

                    foreach (var ptc in patch)
                    {
                        file file = new file();
                        file.Name = Path.GetFileName(ptc.Path);
                        file.State = ptc.Status;
                        files.Add(file);
                    }
                }
            }
            catch
            {   

            }
        }

        private void getSpecificFileDetails(string Repos, string filePath)
        {

        }

        //find and get files with same name from "live" server
        //compare to files from "to be deployed" area
        public void compareTheFiles(string URL, string Repository, string filePath)
        {
            DateTime Now = DateTime.Now;

            if (SpecificFile.Name == null)
            {
                getFileDetails(Repository, filePath);
                foreach (file file in files)
                {
                    if (file.State.Equals("Modified"))
                    {
                        //Check that the new file has todays date on uploading.
                        //Or at least (not currently done) check if the new file is newer than it's "to-be-deployed" Therefore being valid.
                    }
                    else if (file.State.Equals("Renamed"))
                    {
                        //Check if new file exists
                        //Possibly check if old file was removed? But that would probably be done below.
                    }
                    else if (file.State.Equals("Deleted"))
                    {
                        //Check if file was also deleted on the server.
                    }
                }
            }
            else
            {
                getSpecificFileDetails(Repository, filePath);
                try
                {
                    string[] storage = Directory.GetFiles(filePath, SpecificFile.Name);
                    if (storage.Length == 1)
                    {
                        //means we've got our file.
                        DateTime dateLastWritten = System.IO.File.GetLastWriteTime(filePath);
                        if ((dateLastWritten.Date.Equals(Now.Date) && (SpecificFile.State.Equals("Modified")
                            | SpecificFile.State.Equals("Renamed"))) | dateLastWritten.Date>SpecificFile.LastModified)
                        {
                            //Its all good, what do we do now?
                        }
                        else
                        {
                            Assert.Fail();
                        }
                    }
                    else if (storage.Length >= 2)
                    {
                        //Means it returned multiple files with the same filename
                        //Which, if filetype is included correctly, shouldn't happen.
                        Assert.Fail();
                    }
                }
                catch
                {
                    //File (as entered by user) does not exist.
                    Assert.Fail();
                }
            }
        }

    }
}
