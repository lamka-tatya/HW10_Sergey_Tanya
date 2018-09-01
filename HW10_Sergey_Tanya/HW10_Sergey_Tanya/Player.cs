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
        private readonly IList<ICard> allCards = new List<ICard>();

        public Guid Id { get; private set; }
        public IEnumerable<ICard> AllCards => allCards;
        

        public Player()
        {
            Id = new Guid();
        }

        public void Take(ICard card)
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
                var notBlockedCard = TakeCardReadyForAction();

                if (notBlockedCard != null)
                {
                    notBlockedCard.Block();
                }
            }
            else
            {
                var cardToMove = TakeCardReadyForAction();

                if(cardToMove != null)
                {
                    cardToMove.MoveNextStatus();
                }
            }
        }

        private ICard TakeCardReadyForAction()
        {
            return allCards.FirstOrDefault(x => !x.IsBlocked); // TODO добавить проверку на done, либо убирать карту из карт игрока
        }
    }
}
