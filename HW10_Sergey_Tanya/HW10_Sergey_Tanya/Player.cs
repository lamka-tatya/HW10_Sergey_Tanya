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

        internal void Take(Card card)
        {
            card.AssignTo(this);
            card.MoveNextStatus();
        }
    }
}
