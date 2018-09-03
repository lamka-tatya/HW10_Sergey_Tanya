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

        public virtual bool TryTakeNewCard()
        {
            var card = _game.GiveNewCard();
            var result = card != null && card.TryMoveNextStatus();
            if (result)
            {
                _allCards.Add(card);
                card.AssignTo(this);
            }
            return result;                
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
                var card = TakeCardReadyForAction();

                // todo возможно порядок обработки стоит поменять
                if(card != null && card.TryMoveNextStatus())
                {
                    return;
                }
                else
                {
                    TryTakeNewCard();
                }
            }
        }

        private ICard TakeCardReadyForAction()
        {
            return AllCards.FirstOrDefault(x => !x.IsBlocked); // TODO добавить проверку на done, либо убирать карту из карт игрока
        }
    }
}
