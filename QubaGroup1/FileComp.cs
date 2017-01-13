﻿using System;
using System.IO;
using System.Web;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using LibGit2Sharp;

namespace QubaGroup1
{ 

    public class FileComp
    {

        public class file
        {
            public string Name { get; set; }
            public ChangeKind State { get; set; }
            public DateTime LastModified { get; set; }
        }

        List<file> files = new List<file>();
        file SpecificFile = new file();

                //check names of "to be deployed" files
        //Probably using files that have been edited/added/deleted recently ie. today.        
        //[TestCase("")]
                 public void getFileDetails(string Repos, string filePath)
                {
                    string User = "b5010811";
                    string Pass = "310e95ed404ab86756d75833d9a3689bbbb99aaef2536af0b2";

                    string tempClonePath = filePath + @"\tempClone";
                //    System.IO.Directory.CreateDirectory(tempClonePath);

                    try
                    {
                        using (Repository repo = new Repository())
                        {
                            Tree commitTree = repo.Head.Tip.Tree; // Main Tree
                            Tree parentCommitTree = repo.Head.Tip.Parents.Single().Tree; // Secondary Tree

                            var patch = repo.Diff.Compare<Patch>(parentCommitTree, commitTree); // Difference

                            foreach (var ptc in patch)
                            {
                                file file = new file();
                                file.Name = Path.GetFileName(ptc.Path);
                                file.State = ptc.Status;
                                file.LastModified = DateTime.Now;
                                files.Add(file);
                            }
                        }
                    }
                    catch
                    {
                        Directory.Delete(tempClonePath, true);
                    }

                    Directory.Delete(tempClonePath, true);
                }

                private void getSpecificFileDetails(string Repos, string filePath)
                {
                    string User = "b5010811";
                    string Pass = "310e95ed404ab86756d75833d9a3689bbbb99aaef2536af0b2";

                    string tempClonePath = filePath + @"\tempClone";
                 //   System.IO.Directory.CreateDirectory(tempClonePath);

                    try
                    {

                        using (Repository repo = new Repository())
                        {
                            Tree commitTree = repo.Head.Tip.Tree; // Main Tree
                            Tree parentCommitTree = repo.Head.Tip.Parents.Single().Tree; // Secondary Tree

                            var patch = repo.Diff.Compare<Patch>(parentCommitTree, commitTree); // Difference

                            foreach (var ptc in patch)
                            {
                                file file = new file();
                                file.Name = Path.GetFileName(ptc.Path);
                                file.State = ptc.Status;
                                file.LastModified = DateTime.Now;
                                files.Add(file);
                            }
                        }
                    }
                    catch
                    {
                        Directory.Delete(tempClonePath, true);
                    }

                    Directory.Delete(tempClonePath, true);
                }

        List<List<string>> fPaths = new List<List<String>>();
        public FileComp()
        {
        }

        public bool whichFileIsNewer(string f1, string f2)
        {
            FileInfo fi1 = new FileInfo(f1);
            FileInfo fi2 = new FileInfo(f2);


            if (fi1.LastWriteTime > fi2.LastWriteTime)
                //file 1 is newer
                return true;
            else
                //file 2 is newer
                return false;
        }

        public bool comparingFiles(string f1, string f2)
        {
            if (f1 == f2)
                return true;


            FileStream fs1 = new FileStream(f1, FileMode.Open);
            FileStream fs2 = new FileStream(f2, FileMode.Open);




            if (fs1.Length != fs2.Length)
            {

                fs1.Close();
                fs2.Close();


                return false;
            }
            else
            {
                int file1byte;
                int file2byte;
                do
                {
                    file1byte = fs1.ReadByte();
                    file2byte = fs2.ReadByte();
                }
                while ((file1byte == file2byte) && (file1byte != -1));



                fs1.Close();
                fs2.Close();

                return ((file1byte - file2byte) == 0); // will be true if files are identical
            }
        }




        private bool DirSearch(string sDir, string fileName)
        {
            int dirCounter = 0; // to ensure all paths are sorted by directory, allowing easy comparing between different versions of files
            try
            {
                foreach (string d in Directory.GetDirectories(sDir))
                {
                    fPaths.Add(new List<string>());

                    foreach (string f in Directory.GetFiles(d, fileName))
                    {
                        fPaths[dirCounter].Add(f);
                    }
                    DirSearch(d, fileName);
                    dirCounter++;
                }

            }
            catch
            {
                return false;
            }
            return true;
        }




        public void cloneRepo(string Repos, string tempClonePath)
        {
            string user = "mikepress88@gmail.com";
            string pass = "qubagroup1";

            try
            {
                CloneOptions co = new CloneOptions();

                co.CredentialsProvider = (_url,_user,_cred) => new UsernamePasswordCredentials() {Username = user, Password = pass};

                using (Repository repo = new Repository(Repository.Clone(Repos, tempClonePath, co)))
                {
                    
                }
            }
            catch 
            {
                Console.WriteLine("EX_NAME");
        
            }
        }
    }
}