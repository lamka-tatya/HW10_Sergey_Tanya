using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    public class PlayersEmptyException : ArgumentException
    {
        public override string Message => "Игра без игроков не может быть создана";
    }
}
