using Domain.Interfaces;
using System;

namespace Domain
{
    public class Card : ICard
    {
        public Status Status { get; private set; }
        public Guid PlayerId { get; private set; }
        public bool IsBlocked { get; private set; }

        public Card()
        {
            Status = Status.New;
        }

        public void MoveNextStatus()
        {
            if (Status == Status.Done)
            {
                throw new CardStatusException();
            }
            Status++;
        }

        public void AssignTo(IPlayer player)
        {
            PlayerId = player.Id;
        }

        public void Block()
        {
            IsBlocked = true; ;
        }
    }
}