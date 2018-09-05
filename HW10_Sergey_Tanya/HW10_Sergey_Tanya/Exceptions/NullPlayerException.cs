using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    public class NullPlayerException : ArgumentException
    {
        public override string Message => "Игрок должен быть сыт";
    }
}
