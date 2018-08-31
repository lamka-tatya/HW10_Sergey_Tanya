using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HW10_Sergey_Tanya
{
    public class Player
    {
        private readonly IList<Card> allCards = new List<Card>();

        public Guid Id { get; private set; }
        public IEnumerable<Card> AllCards => allCards;
        

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

            allCards.Add(card);

            card.AssignTo(this);
            card.MoveNextStatus();
        }
    }
}
