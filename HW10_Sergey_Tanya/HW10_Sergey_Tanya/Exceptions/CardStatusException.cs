using System;

namespace Domain
{
    public class CardStatusException : Exception
    {
        public override string Message => "Дальше статуса нет!";
    }
}
