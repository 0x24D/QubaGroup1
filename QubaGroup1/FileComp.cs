using System;
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




        public void DirSearch(string sDir, List<string> list)
        {
            try
            {
                foreach (string d in Directory.GetDirectories(sDir))
                {
                    foreach (string f in Directory.GetFiles(d))
                    {
                        list.Add(f);
                    }
                    DirSearch(d, list);
                }
            }
            catch (System.Exception excpt)
            {
                Console.WriteLine(excpt.Message);
            }
        }

        public  string checkMD5(string filename)
        {
            using (var md5 = MD5.Create())
            {
                using (var stream = File.OpenRead(filename))
                {
                    return Encoding.Default.GetString(md5.ComputeHash(stream));
                }
            }
        }




        public void cloneRepo(string Repos, string tempClonePath,string oldPath)
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
            catch (System.Exception excpt)
            {
                Console.WriteLine(excpt.Message);
            }
        }
    }

}