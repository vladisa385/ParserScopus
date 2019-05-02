using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using static System.Threading.Tasks.Task;

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
            _parser.Dispose();
        }

        public async Task<int> GetCountArticle(string url)
        {
            await Delay((int)_miliSecondForSleeping);
            var result = await InvokeMethodWithCountAttempts(_parser.GetCountArticle, url);
            return result;
        }

        public async Task Restart()
        {
            await _parser.Restart();
        }

        public async Task<string> GetNextArticle(string url)
        {
            var localCountAttempts = 0;
            string result = null;
            while (localCountAttempts < _countAttempts)
            {
                try
                {
                    await Delay((int)_miliSecondForSleeping);
                    result = await _parser.GetNextArticle(url);
                    if (result != null)
                        return result;
                    localCountAttempts += 1;
                }
                catch (Exception)
                {
                    await Restart();
                    localCountAttempts += 1;
                    if (localCountAttempts == _countAttempts)
                        throw;
                }
            }

            return result;
        }

        public async Task<List<Person>> ParseSpecificArticle(string url)
        {
            var result = await InvokeMethodWithCountAttempts(_parser.ParseSpecificArticle, url);
            return result;
        }

        private async Task<TOutput> InvokeMethodWithCountAttempts<TParam, TOutput>(Func<TParam, Task<TOutput>> func, TParam delegateParam)
        {
            var localCountAttempts = 0;
            while (true)
            {
                try
                {
                    await Delay((int)_miliSecondForSleeping);
                    return await func(delegateParam);
                }
                catch (Exception)
                {
                    await Restart();
                    localCountAttempts += 1;
                    if (localCountAttempts == _countAttempts)
                        throw;
                }
            }
        }

    }
}
