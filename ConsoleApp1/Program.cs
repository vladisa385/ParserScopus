using System;
using System.Collections.ObjectModel;
using System.IO;
using System.Reflection;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace ConsoleApp1
{
    class Program
    {
        static void Main(string[] args)
        {
            IWebDriver drive = new ChromeDriver(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location)); ;
            drive.Navigate().GoToUrl("https://www.scopus.com/record/display.uri?eid=2-s2.0-85063440381&origin=resultslist&sort=plf-f&src=s&nlo=&nlr=&nls=&sid=05b7b965a025ff8795f30fbd1fa9dcef&sot=b&sdt=cl&cluster=scopubyr%2c%222019%22%2ct&sl=110&s=AF-ID%28%22Tomskij+Gosudarstvennyj+Universitet+Sistem+Upravlenija+i+Radioelektroniki%22+60010892%29+AND+SUBJAREA%28PHYS%29&relpos=0&citeCnt=0&searchTerm=");
            ReadOnlyCollection<IWebElement> webElement = drive.FindElements(By.ClassName("correspondenceEmail"));
            foreach (var element in webElement)
            {
                Console.WriteLine((element.GetAttribute("href")));
            }
        }
    }
}
