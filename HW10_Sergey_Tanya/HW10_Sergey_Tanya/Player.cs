using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HW10_Sergey_Tanya
{
    public class Player
    {
        public Guid Id { get; private set; }

        public Player()
        {
            Id = new Guid();
        }

        public void Take(Card card)
        {
            if (card.Status != Status.New)
            {
                throw new CardStatusIsNotNewException();
            }

            card.AssignTo(this);
            card.MoveNextStatus();
        }
    }
}
