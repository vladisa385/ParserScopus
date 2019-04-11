using System;
using System.Collections.Generic;
using System.Threading;

namespace ScopusModel
{
    public class SleepRetryerScopusParser : IParse
    {
        private readonly IParse _parser;
        private readonly uint _countAttempts;
        private readonly uint _miliSecondForSleeping;
        public SleepRetryerScopusParser(IParse parser, uint countAttempts, uint miliSecondForSleeping)
        {
            _parser = parser;
            _countAttempts = countAttempts;
            _miliSecondForSleeping = miliSecondForSleeping;
        }

        public void Dispose()
        {
            _parser.Dispose();
        }

        public int GetCountArticle(string url)
        {
            Thread.Sleep((int)_miliSecondForSleeping);
            return InvokeMethodWithCountAttempts(_parser.GetCountArticle, url);
        }

        public string GetNextArticle(string url)
        {
            Thread.Sleep((int)_miliSecondForSleeping);
            return InvokeMethodWithCountAttempts(_parser.GetNextArticle, url);
        }

        public List<ResultEmail> ParseSpecificArticle(string url)
        {
            Thread.Sleep((int)_miliSecondForSleeping);
            return InvokeMethodWithCountAttempts(_parser.ParseSpecificArticle, url);
        }

        private TOUTPUT InvokeMethodWithCountAttempts<TPARAMETR, TOUTPUT>(Func<TPARAMETR, TOUTPUT> func, TPARAMETR delegateParam)
        {
            var localCountAttemops = 0;
            TOUTPUT result = default(TOUTPUT);
            try
            {
                result = func(delegateParam);
            }
            catch (Exception e)
            {
                localCountAttemops += 1;
                if (localCountAttemops == _countAttempts)
                    throw e;
            }
            return result;
        }

    }
}
