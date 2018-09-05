using System;

namespace Domain
{
    public class PlayersEmptyException : ArgumentException
    {
        public override string Message => "Игра без игроков не может быть создана";
    }
}
