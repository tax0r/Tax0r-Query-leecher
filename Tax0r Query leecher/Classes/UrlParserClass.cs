using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Tax0r_Query_leecher
{
    class UrlParserClass
    {
        //Nimmt sich den Quelltext des URL's und gibt ihn wider.
        public async Task<string> GetUrlContent(string url)
        {
            HttpClient httpClient = new HttpClient();
            httpClient.Timeout = TimeSpan.FromSeconds(5);
            string content = await httpClient.GetStringAsync(url);
            return content;
        }

        //Sucht in dem URL nach anderen URL's und gibt eine ArrayListe mit diesen wider.
        public string[] SearchForUrls(string content)
        {
            Regex regex = new Regex(@"(http|ftp|https)://([\w_-]+(?:(?:\.[\w_-]+)+))([\w.,@?^=%&:/~+#-]*[\w@?^=%&/~+#-])?", RegexOptions.Multiline);
            MatchCollection matches = regex.Matches(content);
            List<string> resultsList = new List<string>();
            
            foreach (Match match in matches)
            {
                resultsList.Add(match.ToString());
            }

            string[] results = resultsList.ToArray();
            return results;
        }

    }
}
