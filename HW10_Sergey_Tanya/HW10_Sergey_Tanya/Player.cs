using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Interfaces;

namespace Domain
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

        public void Toss(ICoin coin)
        {
            var coinResult = coin.Toss();

            if (coinResult == CoinResult.Head)
            {
                var notBlockedCard = allCards.FirstOrDefault(x => !x.IsBlocked); // TODO добавить проверку на done, либо убирать карту из карт игрока

                if (notBlockedCard != null)
                {
                    notBlockedCard.Block();
                }
            }
        }
    }
}
