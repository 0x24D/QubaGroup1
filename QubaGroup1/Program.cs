using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Http;
using System.Threading.Tasks;
using System.Net;

namespace QubaGroup1
{
    class Program

    {
        static string url = "http://shugroupproject1.quba.co.uk/";
        static void Main(string[] args)
        {
            Ping(url);
        }
        static public bool Ping(string url)
        {
            try
            {
                HttpWebRequest request = (HttpWebRequest)HttpWebRequest.Create(url);
                request.Timeout = 3000;
                request.AllowAutoRedirect = false; // find out if this site is up and don't follow a redirector
                request.Method = "HEAD";

                using (var response = request.GetResponse())
                {
                    Console.WriteLine("website online");
                    Console.ReadLine();
                    return true;
                }

            }
            catch
            {
                return false;
            }

        }
    }
}
