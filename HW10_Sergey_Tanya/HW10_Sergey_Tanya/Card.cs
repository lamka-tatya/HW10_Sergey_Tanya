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
            // todo проверку на > done
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