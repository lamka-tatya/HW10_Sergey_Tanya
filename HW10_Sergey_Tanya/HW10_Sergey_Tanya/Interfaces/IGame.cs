using System.Collections.Generic;
using Domain.Interfaces;

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
        IEnumerable<IPlayer> TakePlayers();
    }
}