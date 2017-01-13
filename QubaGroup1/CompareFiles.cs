﻿using System;
using System.IO;
using System.Web;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using LibGit2Sharp; //Allows us to read the beanstalk git files
using Octopus;

namespace QubaGroup1
{
    class CompareFiles
    {

        //Creates a "file" which can tell us the name and it's status in the latest commit.
        public class file
        {
            public string Name { get; set; }
            public ChangeKind State { get; set; }
            public DateTime LastModified { get; set; }
        }

        List<file> files = new List<file>();
        file SpecificFile = new file();
        List<string> storage = new List<string>();

        CompareFiles(string FileName)
        {
            this.SpecificFile.Name = FileName;
        }

        public CompareFiles()
        {
        }

        private bool whichFileIsNewer(string f1, string f2)
        {
            FileInfo fi1 = new FileInfo(f1);
            FileInfo fi2 = new FileInfo(f2);


            if (fi1.CreationTimeUtc > fi2.CreationTimeUtc)
                //file 1 is newer
                return true;
            else
                //file 2 is newer
                return false;
        }

        //Might just have to checksum what we have and make a guess as to it being "up-to-date"
        //Or Perhaps if the server has rollback files stored locally, we could work with that.
        private bool comparingFiles(string f1, string f2)
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

                return ((file1byte - file2byte) == 0);
            }
        }

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
                        CloneOptions co = new CloneOptions();
                        co.CredentialsProvider = (_url, _user, _cred) =>
                        new UsernamePasswordCredentials {Username = User, Password = Pass};

                        using (Repository repo = new Repository(Repository.Clone(Repos, tempClonePath, co)))
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
                        CloneOptions co = new CloneOptions();
                        co.CredentialsProvider = (_url, _user, _cred) =>
                        new DefaultCredentials();

                        using (Repository repo = new Repository(Repository.Clone(Repos, tempClonePath, co)))
                        {
                            //May find the last commit of a file NEEDS TESTING ONCE CLONING WORKS
                            var path = tempClonePath + @"\" + SpecificFile.Name;
                            var commit = repo.Head.Tip;
                            var gitObj = commit[path].Target;
                            var set = new HashSet<string>();
                            var queue = new Queue<Commit>();
                            queue.Enqueue(commit);
                            set.Add(commit.Sha);

                            while (queue.Count > 0)
                            {
                                commit = queue.Dequeue();
                                var go = false;
                                foreach (var parent in commit.Parents)
                                {
                                    var tree = parent[path];
                                    if (tree == null)
                                        continue;
                                        var eq = tree.Target.Sha == gitObj.Sha;
                                        if (eq && set.Add(parent.Sha))
                                            queue.Enqueue(parent);
                                        go = go || eq;
                                }
                                if (!go)
                                   break;
                            }

                            Tree commitTree = repo.Head.Tip.Tree; // Main Tree
                            Tree parentCommitTree = repo.Head.Tip.Parents.Single().Tree; // Secondary Tree

                            var patch = repo.Diff.Compare<Patch>(parentCommitTree, commitTree); // Difference

                            foreach (var ptc in patch)
                            {
                                if (Path.GetFileName(ptc.Path) == SpecificFile.Name)
                                    SpecificFile.State = ptc.Status;
                            }
                            SpecificFile.LastModified = ConvertFromDateTimeOffset(commit.Committer.When);

                        }
                    }
                    catch
                    {
                        Directory.Delete(tempClonePath, true);
                    }

                    Directory.Delete(tempClonePath, true);
                }

        //find and get files with same name from "live" server
        //compare to files from "to be deployed" area
        public void compareTheFiles(string URL, string Repository, string filePath)
        {
            DateTime Now = DateTime.Now;

//            SpecificFile.Name = "Test.cs";
//            SpecificFile.State = ChangeKind.Modified;
//            SpecificFile.LastModified = new DateTime(2016, 12, 22);

            file file2 = new file();
            file2.Name = "Test21.cs";
            file2.State = ChangeKind.Modified;
            file2.LastModified = new DateTime(2016, 12, 22);

            file file3 = new file();
            file3.Name = "Test22.cs";
            file3.State = ChangeKind.Renamed;
            file3.LastModified = new DateTime(2016, 12, 22);

            file file4 = new file();
            file4.Name = "Test23.cs";
            file4.State = ChangeKind.Modified;
            file4.LastModified = new DateTime(2016, 12, 22);

            file file5 = new file();
            file5.Name = "Test24.cs";
            file5.State = ChangeKind.Deleted;
            file5.LastModified = new DateTime(2016, 12, 22);

            files.Add(file2);
            files.Add(file3);
            files.Add(file4);
            files.Add(file5);


            if (SpecificFile.Name == null)
            {
               //getFileDetails(Repository, filePath);
                foreach (file file in files)
                {
                    storage.Clear();
                    if (file.State == ChangeKind.Modified)
                    {
                        //Check that the new file has todays date on uploading.
                        //Or at least (not currently done) check if the new file is newer than it's "to-be-deployed" Therefore being valid.
                        if (DirSearch(filePath, file.Name))
                        {
                            Console.WriteLine("(MODIFIED) file located, checking against commit.");
                            //Get Checksum files
                            //Compare the file to the clone
                        }
                        else
                        {
                            //Shouldn't get this, it was modified not deleted.
                            Console.WriteLine("ERROR: a given file modified in the last commit was unable to located.");
                        }
                    }
                    else if (file.State == ChangeKind.Renamed | file.State == ChangeKind.Added)
                    {
                        //Check if new file exists
                        //Renamed - Possibly check if old file was removed? But would that probably be done below?
                        if (DirSearch(filePath, file.Name))
                        {
                            //New renamed/added file found, therefore it was successfully renamed.
                            //Check the previous differently named file was removed.
                            Console.WriteLine("(RENAMED) file located, checking against commit.");
                        }
                        else
                        {
                            //File wasn't renamed or added... Fail.
                            Console.WriteLine("ERROR: Renamed file not located.");
                        }

                    }
                    else if (file.State == ChangeKind.Deleted)
                    {
                        //Check if file was also deleted on the server.
                        if (!DirSearch(filePath, file.Name))
                        {
                            //File was deleted
                            Console.WriteLine("(DELETED) file not located, but it was deleted in the last commit.");
                        }
                        else
                        {
                            //File does exist, therefore it wasn't deleted... FAIL.
                            Console.WriteLine("ERROR: Found a file that was supposed to be deleted in the last commit.");
                        }

                    }
                    else
                    {
                        //I don't know what has happening to get here, but just to be safe.
                        Console.WriteLine("ERROR: Something has gone quite wrong.");
                    }
                }
             }
             else
             {
                 //Deals with specfic files.
                 //getSpecificFileDetails(Repository, filePath);
                 try
                 {
                     storage.Clear();
                     DirSearch(filePath, SpecificFile.Name);
                     if (storage.Count == 1)
                     {
                           //means we've got our file.
                           DateTime dateLastWritten = System.IO.File.GetLastWriteTime(filePath);
                           if ((DateTime.Compare(dateLastWritten, SpecificFile.LastModified) >= 0) &&
                              (SpecificFile.State == ChangeKind.Modified | SpecificFile.State == ChangeKind.Renamed | SpecificFile.State == ChangeKind.Added))
                           {
                              Console.WriteLine("Specific file was found.");
                              //Get Checksum files
                              //Compare the file to the clone
                           }
                           else
                           {
                              //File exists but is either older than the last commit... or not modified, added or renamed?  
                              Console.WriteLine("Specfic file found, but is either older than it's last commit. Or in an incorrect state.");
                           }
                      }
                      else if (SpecificFile.State == ChangeKind.Deleted && storage.Count == 0)
                      {
                           //File wasn't found and was deleted, therefore a success.
                           Console.WriteLine("Specific file wasn't found, but it was deleted in a previous commit.");
                      }
                      else
                      {
                           //Means it returned multiple files with the same filename.
                           //Which, if filetype is included correctly, shouldn't happen.
                           //Or it doesn't exist.
                           if (storage.Count > 1) 
                               Console.WriteLine("ERROR: Multiple of the same filename, with the same filetype, too ambiguous.");
                           if (storage.Count == 0)
                               Console.WriteLine("ERROR: File not found or does not exist.");
                       }
                   }
                   catch
                   {
                       //File (as entered by user) does not exist.
                       Console.WriteLine("ERROR: Specific file was not found or does not exist.");
                   }
              }
        }
        private bool DirSearch(string sDir, string fileName)
        {
            try
            {
                foreach (string d in Directory.GetDirectories(sDir))
                {
                    foreach (string f in Directory.GetFiles(d, fileName))
                    {
                        storage.Add(f);
                    }
                        DirSearch(d, fileName);
                }
            }
            catch (System.Exception)
            {
                return false;
            }
            if (storage.Count > 0)
                return true;
            else
                return false;
        }
        private DateTime ConvertFromDateTimeOffset(DateTimeOffset dateTime)
        {
            if (dateTime.Offset.Equals(TimeSpan.Zero))
                return dateTime.UtcDateTime;
            else if (dateTime.Offset.Equals(TimeZoneInfo.Local.GetUtcOffset(dateTime.DateTime)))
                return DateTime.SpecifyKind(dateTime.DateTime, DateTimeKind.Local);
            else
                return dateTime.DateTime;
       }
        public string CalculateMd5Hash(string input)
        {
            string md5Sum;
            // step 1, calculate MD5 hash from input
            MD5 md5 = System.Security.Cryptography.MD5.Create();
            byte[] inputBytes = System.Text.Encoding.ASCII.GetBytes(input);
            byte[] hash = md5.ComputeHash(inputBytes);
            // step 2, convert byte array to hex string
            StringBuilder sb = new StringBuilder();
            md5Sum = System.Text.Encoding.UTF8.GetString(hash);
            return md5Sum;
        }
    }
}