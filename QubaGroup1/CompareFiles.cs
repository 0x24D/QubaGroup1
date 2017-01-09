using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LibGit2Sharp;
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
        }
        List<file> files = new List<file>();
//        string FileName;

//        CompareFiles(string FileName)
//        {
//            this.FileName = FileName;
//        }
        public CompareFiles()
        {
//            FileName = null;
        }

        //check names of "to be deployed" files
        //Probably using files that have been edited/added/deleted recently ie. "today".        
        //[TestCase("")]
        public void getFileDetails(string Repos, string WorkEnvironment)
        {
            string User = "b5010811";
            string Pass = "310e95ed404ab86756d75833d9a3689bbbb99aaef2536af0b2";
            WorkEnvironment = @"F:\MyWork\Test";//Directory.GetCurrentDirectory();
            try
            {
                CloneOptions co = new CloneOptions();
                co.CredentialsProvider = (_url, _user, _cred) =>
                new UsernamePasswordCredentials { Username = User, Password = Pass };

                using (Repository repo = new Repository(Repository.Clone(Repos, WorkEnvironment,co)))
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

        //find and get files with same name from "live" server
        //compare to files from "to be deployed" area
        public void compareTheFiles(string URL, string Repository)
        {
//            if (FileName == null)
//            {
//            getFileDetails(Repository);
//            foreach (file file in files)
 //           {
 //               if ()
  //              {

  //              }
 //           }
//            }
//            else
//            {
//            }
        }

    }
}
