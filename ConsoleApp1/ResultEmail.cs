﻿using System;
using System.Collections.Generic;
using System.Text;

namespace ParserScopus
{
    public class ResultEmail
    {
        public string Email { get; }
        public string Fio { get; }

        public ResultEmail(string fio, string email)
        {
            Fio = fio;
            Email = email;
        }


    }
}
