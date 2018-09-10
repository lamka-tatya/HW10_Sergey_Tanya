using System;

namespace Domain.Interfaces
{
    internal interface ICard
    {
        Status Status { get; set; }

        bool IsBlocked { get; }

        Guid PlayerId { get; }

        void AssignTo(Guid playerId);

        void Block();

        void UnBlock();
    }

}
