using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace VersaoDesktop.Scans
{
    public class ScanVulcanNovel
    {
        public static async Task<string> GetConteudoAsync(string url)
        {

            HttpClient client = new HttpClient();
            var response = await client.GetAsync(url);
            var pageContents = await response.Content.ReadAsStringAsync();
            var doc = new HtmlDocument();
            doc.LoadHtml(pageContents);
            string ff = "main";
            var div = doc.DocumentNode.SelectNodes("//main[@id='" + ff + "']");
            string html = div[0].InnerHtml.Replace("<br>", "\n");
            doc.LoadHtml(html);

            //foreach (HtmlNode p in div.DocumentNode.SelectNodes("//br"))
            //{
            //    Console.WriteLine(p.PreviousSibling.InnerText.Trim());
            //}

            string decoded = System.Net.WebUtility.HtmlDecode(doc.DocumentNode.InnerText);

            return decoded;
        }

        public static string ProxPag(string url, int pag)
        {
            url = url + pag;
            return url;
        }
    }
}
