using System;

namespace Domain
{
    public class Card
    {
        public Status Status { get; private set; }
        public Guid PlayerId { get; private set; }
        public bool IsBlocked { get; private set; }

        public Card()
        {
            Status = Status.New;
        }

        public virtual void MoveNextStatus()
        {
            Status++;
        }

        public void AssignTo(Player player)
        {
            PlayerId = player.Id;
        }

        internal void Block()
        {
            IsBlocked = true; ;
        }
    }
}