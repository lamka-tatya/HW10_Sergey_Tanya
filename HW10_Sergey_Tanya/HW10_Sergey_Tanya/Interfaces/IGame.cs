using System.Collections.Generic;
using Domain.Interfaces;

namespace Domain
{
    internal interface IGame
    {
        void AddPlayer(IPlayer player);
        int CardsCount(Status status);
        IEnumerable<ICard> CardsThat(Status status);
        ICard GiveNewCard();
        void HelpOtherPlayer();
        void PlayRound();
        IEnumerable<IPlayer> TakePlayers();
    }
}