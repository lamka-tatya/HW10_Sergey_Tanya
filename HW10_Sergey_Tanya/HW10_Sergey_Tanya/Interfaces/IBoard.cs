using Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces
{
    internal interface IBoard
    {
        ICard GiveNewCard();

        IEnumerable<ICard> CardsThat(Status status);

        bool WipLimitIsReached(Status status);

        IWipLimit WipLimit { get; }
    }
}
