using System;

namespace Domain.Interfaces
{
    internal interface ICard
    {
        Status Status { get; }

        bool IsBlocked { get; }

        Guid PlayerId { get; }

        bool TryMoveNextStatus();

        void AssignTo(IPlayer player);

        void Block();

        void UnBlock();
    }

}
