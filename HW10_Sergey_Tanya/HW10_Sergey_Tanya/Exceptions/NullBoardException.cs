using System;

namespace Domain
{
    public class NullBoardException : ArgumentException
    {
        public override string Message => "Доска не может быть пустой!";
    }
}
