using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;
using Tax0r_Query_leecher.Classes;
using Console = Colorful.Console;

namespace Tax0r_Query_leecher
{
    class Program
    {
        static async Task Main(string[] args)
        {
            FileHelperClass fileHelper = new FileHelperClass();
            UrlParserClass urlParser = new UrlParserClass();
            FilterHelperClass filterHelper = new FilterHelperClass();

            Console.WriteLine("Please drag&drop your query-list.", Color.LightPink);

            filterHelper.addFilters("yahoo.com");
            filterHelper.addFilters("google.com");
            filterHelper.addFilters("bing.com");
            filterHelper.addFilters("bingj.com");
            filterHelper.addFilters("live.com");
            filterHelper.addFilters("w3.org");
            filterHelper.addFilters("microsofttranslator.com");
            filterHelper.addFilters("wikipedia.org");
            filterHelper.addFilters("twitter.com");
            filterHelper.addFilters("youtube.com");
            filterHelper.addFilters("facebook.com");
            filterHelper.addFilters("instagram.com");
            filterHelper.addFilters("microsoft.com");
            filterHelper.addFilters("giga.de");
            filterHelper.addFilters("msn.com");
            filterHelper.addFilters("outlook.com");
            filterHelper.addFilters("creativecommons.org");
            filterHelper.addFilters("trustscam.nl");
            filterHelper.addFilters("aol.de");
            filterHelper.addFilters("yandex.com");
            filterHelper.addFilters("verbraucherschutz.de");
            filterHelper.addFilters("whois.com");
            filterHelper.addFilters("bingparachute.com");
            filterHelper.addFilters("duckduckgo.com");

            string[] engines = { "https://www.bing.com/search?q="};

            string toReplace = '"'.ToString();
            string input = Console.ReadLine().Replace(toReplace, string.Empty);
            Console.WriteLine(input);
            Console.Clear();

            string[] querys = fileHelper.readLinesFromFile(input);
            List<string> foundUrls = new List<string>();

            foreach (string engine in engines)
            {
                Console.WriteLine("[Current Engine]: " + engine, Color.LightYellow);
                foreach (string query in querys)
                {
                    try
                    {
                        string url = engine + query;
                        Console.WriteLine("[Current URL]: " + url, Color.LightGreen);

                        string content = await urlParser.GetUrlContent(url);
                        string[] tmpFoundUrls = urlParser.SearchForUrls(content);
                        foreach (string tmpFoundUrl in tmpFoundUrls)
                        {
                            if (filterHelper.isFiltered(tmpFoundUrl))
                            {
                                foundUrls.Add(tmpFoundUrl);
                                Console.WriteLine("[New URL]: " + tmpFoundUrl, Color.LightPink);
                            }
                        }
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine("[ERROR]: " + e.Message, Color.Red);
                    }
                }
            }

            List<string> distinctUrls = foundUrls.Distinct().ToList();

            Console.Clear();
            Console.WriteLine("[Success!]: URL's we're scraped successfully!");
            Console.WriteLine("[Information]: New URL's found: " + foundUrls.Count(), Color.LightBlue);
            Console.WriteLine("[Information]: Distinct URL's found: " + distinctUrls.Count(), Color.LightPink);

            fileHelper.saveToFile(distinctUrls.ToArray(), distinctUrls.Count());

            Console.WriteLine("\npress any key to exit the process...");
            Console.ReadKey();
        }
    }
}
