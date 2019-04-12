using System;
using System.Collections.Generic;
using System.Threading;

namespace ParserModel
{
    public class SleepRetryerParser : IParse
    {
        private readonly IParse _parser;
        private readonly uint _countAttempts;
        private readonly uint _miliSecondForSleeping;
        public SleepRetryerParser(IParse parser, uint countAttempts, uint miliSecondForSleeping)
        {
            _parser = parser;
            _countAttempts = countAttempts;
            _miliSecondForSleeping = miliSecondForSleeping;
        }

        public void Dispose()
        {
            //_parser.Dispose();
        }

        public int GetCountArticle(string url)
        {
            Thread.Sleep((int)_miliSecondForSleeping);
            return InvokeMethodWithCountAttempts(_parser.GetCountArticle, url);
        }

        public string GetNextArticle(string url)
        {
            var localCountAttempts = 0;
            string result = null;
            while (localCountAttempts < _countAttempts)
            {
                try
                {
                    Thread.Sleep((int)(_miliSecondForSleeping + 1000 * localCountAttempts));
                    result = _parser.GetNextArticle(url);
                    if (result != null)
                        return result;
                    localCountAttempts += 1;
                }
                catch (Exception)
                {
                    localCountAttempts += 1;
                    if (localCountAttempts == _countAttempts)
                        throw;
                }
            }

            return result;
        }

        public List<Person> ParseSpecificArticle(string url)
        {
            return InvokeMethodWithCountAttempts(_parser.ParseSpecificArticle, url);
        }

        private TOutput InvokeMethodWithCountAttempts<TParam, TOutput>(Func<TParam, TOutput> func,
            TParam delegateParam)
        {
            var localCountAttempts = 0;
            while (true)
            {
                try
                {
                    Thread.Sleep((int)(_miliSecondForSleeping + 1000 * localCountAttempts));
                    return func(delegateParam);
                }
                catch (Exception)
                {
                    localCountAttempts += 1;
                    if (localCountAttempts == _countAttempts)
                        throw;
                }
            }
        }

    }
}
