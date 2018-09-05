using System;
using System.Collections.Generic;

namespace Domain.Interfaces
{
    internal interface IPlayer
    {
        IEnumerable<ICard> AllCards { get; }

        Guid Id { get; }

        bool TryTakeNewCard();

        bool TryUnblockCard();

        bool TryMoveCardNextStatus(ICard card);

        void Toss(ICoin coin);

        void JoinGame(IGame game);

        void HelpOtherPlayer();

        void BlockCard();
    }
}