using System;

namespace Domain
{
    public class NullPlayerException : ArgumentException
    {
        public override string Message => "Игрок должен быть сыт";
    }
}
