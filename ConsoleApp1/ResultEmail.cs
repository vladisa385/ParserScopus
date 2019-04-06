using System;
using System.Collections.Generic;
using System.Text;

namespace ParserScopus
{
    public class ResultEmail
    {
        private readonly string _email;
        private readonly string _fio;

        public ResultEmail(string fio, string email)
        {
            _fio = fio;
            _email = email;
        }


    }
}
