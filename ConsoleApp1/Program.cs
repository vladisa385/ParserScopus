using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Reflection;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace ParserScopus
{
    class Program
    {
        static void Main()
        {
            var driver = new ChromeDriver(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location));
            var parser = new ScopusParser(driver);
            List<ResultEmail> emailWithAuthor = new List<ResultEmail>();
            var firstUrl = "https://www.scopus.com/record/display.uri?origin=recordpage&eid=2-s2.0-85061176683&citeCnt=0&noHighlight=false&sort=plf-f&src=s&nlo=&nlr=&nls=&sid=05b7b965a025ff8795f30fbd1fa9dcef&sot=b&sdt=cl&cluster=scopubyr%2c%222019%22%2ct&sl=110&s=AF-ID%28%22Tomskij+Gosudarstvennyj+Universitet+Sistem+Upravlenija+i+Radioelektroniki%22+60010892%29+AND+SUBJAREA%28PHYS%29&relpos=1";
            var result = parser.ParseSpecificArticle(firstUrl, out var nextUrl);
            if (result.Count != 0)
                emailWithAuthor.AddRange(result);
            if (nextUrl != null)
                parser.ParseSpecificArticle(nextUrl, out nextUrl);
            foreach (ResultEmail var in emailWithAuthor)
            {
                Console.WriteLine($"{var.Fio}!!!{var.Email}");
            }
            Console.ReadKey();
        }
    }
}
