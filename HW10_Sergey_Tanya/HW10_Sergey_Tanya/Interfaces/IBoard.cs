using Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces
{
    public interface IBoard
    {
        ICard GiveNewCard();

        IEnumerable<ICard> CardsThat(Status status);

        bool WipLimitIsReached(Status status);
    }
}
