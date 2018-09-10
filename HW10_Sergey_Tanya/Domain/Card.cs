using Domain.Interfaces;
using System;
using Domain.Extensions;

namespace Domain
{
    internal class Card : ICard
    {
        public Status Status { get; set; }

        public Guid PlayerId { get; private set; }

        public bool IsBlocked { get; private set; }

        public Card()
        {
            Status = Status.New;
        }

        public void AssignTo(Guid playerId)
        {
            PlayerId = playerId;
        }

        public void Block()
        {
            IsBlocked = true;
        }

        public void UnBlock()
        {
            IsBlocked = false;
        }
    }
}