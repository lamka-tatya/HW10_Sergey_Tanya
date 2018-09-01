using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Interfaces;

namespace Domain
{
    public class Player : IPlayer
    {
        private readonly IList<ICard> _allCards = new List<ICard>();

        private Game _game = null;

        public Guid Id { get; private set; }

        public virtual IEnumerable<ICard> AllCards => _allCards;

        public Player()
        {
            Id = Guid.NewGuid();
        }

        public void JoinGame(Game game)
        {
            _game = game;
        }

        public virtual void TakeNewCard()
        {
            var card = _game.GiveNewCard();

            _allCards.Add(card);
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
                var cardTo = TakeCardReadyForAction();

                // todo возможно порядок обработки стоит поменять
                if(cardTo != null)
                {
                    cardTo.MoveNextStatus();
                }
                else
                {
                    TakeNewCard();
                }
            }
        }

        private ICard TakeCardReadyForAction()
        {
            return AllCards.FirstOrDefault(x => !x.IsBlocked); // TODO добавить проверку на done, либо убирать карту из карт игрока
        }
    }
}
