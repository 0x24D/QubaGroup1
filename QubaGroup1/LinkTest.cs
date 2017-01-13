using HtmlAgilityPack;

List<string> links = new List<string>();
int j = 0;
string url2 = "http://www.shugroupproject1.quba.co.uk/";
HtmlWeb hw = new HtmlWeb();
HtmlDocument doc = hw.Load(url2);
foreach (HtmlNode link in doc.DocumentNode.SelectNodes("//a[@href]"))
{
    string hrefValue = link.GetAttributeValue("href", string.Empty);
    links.Add(hrefValue);
    j++;
    Console.WriteLine(j + " Link(s) found");
}
for (int i = 0; i < links.Count; i++)
{
    Console.WriteLine(links[i]);
}
Console.WriteLine("");
////////////////////////////////////ping each hyperlink
for (int i = 0; i < links.Count; i++)
{
    try
    {

        List<string> links = new List<string>();
        [TestCase("http://shugroupproject1.quba.co.uk")]
        public void TestCase(string url2)
        {
            int j = 0;
            HtmlWeb hw = new HtmlWeb();
            HtmlDocument doc = hw.Load(url2);
            foreach (HtmlNode link in doc.DocumentNode.SelectNodes("//a[@href]"))
            {
                string hrefValue = link.GetAttributeValue("href", string.Empty);
                links.Add(hrefValue);
                j++;
                Console.WriteLine(j + " Link(s) found");
                Console.WriteLine(""); 
            }
            for (int i = 0; i < links.Count; i++)
            {
                Console.WriteLine(links[i]);
            }
            Console.WriteLine("");
            for (int i = 0; i < links.Count; i++)
            {
               
                try
                {
                    HttpWebRequest request = WebRequest.Create(links[i]) as HttpWebRequest;
                    request.Timeout = 3000;
                    request.AllowAutoRedirect = true; // find out if this site is up and follow a redirector
                    request.Method = "HEAD";

                    using (var response = request.GetResponse())
                    {
                        Console.WriteLine(links[i] + " connected"); 
                    }
                }
                catch
                {
                    Console.WriteLine(links[i] + " failed");
                }

            }
    }
    catch
    {
        Console.WriteLine(links[i] + " failed");
    }
}