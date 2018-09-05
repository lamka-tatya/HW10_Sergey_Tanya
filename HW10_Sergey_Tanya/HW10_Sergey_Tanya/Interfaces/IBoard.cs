using System.Collections.Generic;

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
