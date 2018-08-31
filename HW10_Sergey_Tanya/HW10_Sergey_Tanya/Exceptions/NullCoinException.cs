using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    public class NullCoinException : ArgumentException
    {
        public override string Message => "Игра без монеты не может быть создана";
    }
}
