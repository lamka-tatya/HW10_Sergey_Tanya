﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    public class BoardIsNullException : ArgumentException
    {
        public override string Message => "Доска не может быть пустой!";
    }
}
