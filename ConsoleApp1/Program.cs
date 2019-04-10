using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Edge;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.IE;

namespace ParserScopus
{
    class Program
    {
        static void Main()
        {
            IWebDriver driver = new EdgeDriver(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location));
            IParse parser = new ScopusParser(driver);
            var emailWithAuthor = new List<ResultEmail>();
            var nextUrl = "https://www.scopus.com/record/display.uri?origin=recordpage&eid=2-s2.0-85063440381&citeCnt=0&noHighlight=false&sort=plf-f&src=s&nlo=&nlr=&nls=&sid=05b7b965a025ff8795f30fbd1fa9dcef&sot=b&sdt=cl&cluster=scopubyr%2c%222019%22%2ct&sl=110&s=AF-ID%28%22Tomskij+Gosudarstvennyj+Universitet+Sistem+Upravlenija+i+Radioelektroniki%22+60010892%29+AND+SUBJAREA%28PHYS%29&relpos=0";
            var countArticles = 42;
            var currentArticle = 1;
            do
            {
                var result = parser.ParseSpecificArticle(nextUrl);
                nextUrl = parser.GetNextArticle(nextUrl);
                if (countArticles == 0)
                    countArticles = parser.GetCountArticle(nextUrl);
                Console.WriteLine($"CurrentArticle is {currentArticle}/{countArticles}");
                if (result.Count != 0)
                    emailWithAuthor.AddRange(result);
                currentArticle += 1;
            } while (nextUrl != null);

            foreach (var item in emailWithAuthor)
            {
                Console.WriteLine($"{item.Email} {item.Fio}");
            }
            parser.Dispose();
            Console.ReadKey();
        }
    }
}
