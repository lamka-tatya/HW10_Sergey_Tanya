using System;

namespace Domain
{
    public class NullCoinException : ArgumentException
    {
        public override string Message => "Игра без монеты не может быть создана";
    }
}
