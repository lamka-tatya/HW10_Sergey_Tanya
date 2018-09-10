using System;

namespace Domain.Interfaces
{
    internal interface ICard
    {
        Status Status { get; set; }

        bool IsBlocked { get; }

        Guid PlayerId { get; }

        void AssignTo(IPlayer player);

        void Block();

        void UnBlock();
    }

}
