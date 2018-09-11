using System;

namespace Domain.Interfaces
{
    internal interface ICard
    {
        Status Status { get; }

        bool IsBlocked { get; }

        Guid PlayerId { get; }

        void AssignTo(Guid playerId);

        void Block();

        void UnBlock();

        void NextStatus();
    }

}
