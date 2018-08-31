using System;

namespace HW10_Sergey_Tanya
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

        public void MoveNextStatus()
        {
            Status++;
        }

        public void AssignTo(Player player)
        {
            PlayerId = player.Id;
        }
    }
}