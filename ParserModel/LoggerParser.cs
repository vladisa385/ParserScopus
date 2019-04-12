using System;
using System.Collections.Generic;
using System.IO;

namespace ParserModel
{
    public class LoggerParser : IParse
    {
        private readonly IParse _parser;
        private readonly string _pathToLog;

        public LoggerParser(IParse parser)
        {
            _parser = parser;
            string rootPath = Directory.GetCurrentDirectory() + "\\Logs.txt";
            InvokeFuncWithLogException(u => !File.Exists(u) ? File.CreateText(u) : null, rootPath);
            _pathToLog = rootPath;
        }

        public void Dispose()
        {
            LogWithDate("[Dispose] Начинаем выполнение");
            _parser.Dispose();
            LogWithDate("[Dispose] Успешно выполнили");
        }

        public List<Person> ParseSpecificArticle(string url)
        {
            LogWithDate($"[ParseSpecificArticle] Начинаем выполнение для {url}");
            var res = InvokeFuncWithLogException(_parser.ParseSpecificArticle, url);
            LogWithDate($"[ParseSpecificArticle] Успешно выполнили и вернули  {res.Count} имейлов");
            return res;
        }

        public string GetNextArticle(string url)
        {
            LogWithDate("[GetNextArticle] Начинаем выполнение");
            var res = InvokeFuncWithLogException(_parser.GetNextArticle, url);
            LogWithDate(res != null
                ? $"[GetNextArticle] Успешно выполнили и вернули {url})"
                : "[GetNextArticle] Успешно выполнили, но следующую статью не вернули");
            return res;
        }

        public int GetCountArticle(string url)
        {
            LogWithDate("[GetCountArticle] Начинаем выполнение");
            var res = InvokeFuncWithLogException(_parser.GetCountArticle, url);
            LogWithDate($"[GetCountArticle] Успешно выполнили и вернули  {res} количество");
            return res;
        }

        private void LogWithDate(string text)
        {
            try
            {
                using (TextWriter tw = new StreamWriter(_pathToLog, true))
                {
                    var line = $"[{DateTime.Now}]{text}";
                    tw.WriteLine(line);
                }
            }
            catch (Exception)
            {
                // ignored
            }
        }

        private TOutput InvokeFuncWithLogException<TParam, TOutput>(Func<TParam, TOutput> func, TParam delegateParam)
        {

            try
            {
                return func(delegateParam);
            }
            catch (Exception e)
            {
                LogWithDate(e.Message);
            }
            return func(delegateParam);
        }

    }
}
