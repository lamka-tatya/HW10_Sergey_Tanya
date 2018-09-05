using Domain.Interfaces;
using System.Collections.Generic;

namespace Domain
{
    internal interface IGame
    {
        IEnumerable<ICard> DoneCards { get; }

        IEnumerable<ICard> WorkCards { get; }

        void AddPlayer(IPlayer player);

        int CardsCount(Status status);

        ICard GiveNewCard();

        void HelpOtherPlayer();

        void PlayRound();
    }
}