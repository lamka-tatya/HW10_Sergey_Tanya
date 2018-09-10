using Domain.Interfaces;
using System;
using System.Collections.Generic;

namespace Domain
{
    internal interface IGame
    {
        IEnumerable<ICard> DoneCards { get; }

        IEnumerable<ICard> WorkCards { get; }

        void AddPlayer(IPlayer player);

        int CardsCount(Status status);

        void HelpOtherPlayer();

        void PlayRound();

        bool TryTakeNewCard(Guid playerId);

        bool TryUnblockCard(Guid playerId);

        bool TryMoveCardNextStatus(ICard card);

        void BlockCard(Guid playerId);

        ICard GenerateNewCard();
    }
}