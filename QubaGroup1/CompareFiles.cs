using System;
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

        //check names of "to be deployed" files
        //Probably using files that have been edited/added/deleted recently ie. "today".        
        //[TestCase("")]
        private void getFileNames(string Repository)
        {
            using (Repository repo = new Repository(Repository))
            {
                Tree commitTree = repo.Head.Tip.Tree; // Main Tree
                Tree parentCommitTree = repo.Head.Tip.Parents.Single().Tree; // Secondary Tree

                var patch = repo.Diff.Compare<Patch>(parentCommitTree, commitTree); // Difference

                foreach (var ptc in patch)
                {
                    file file = new file();
                    file.Name = ptc.Path;
                    file.State = ptc.Status;
                    files.Add(file);
                }
            }
        }

        //find and get files with same name from "live" server
        //compare to files from "to be deployed" area
        public bool compareFiles(string fileNames, string URL)
        {
            getFileNames(URL);
            return true;
        }

    }
}
