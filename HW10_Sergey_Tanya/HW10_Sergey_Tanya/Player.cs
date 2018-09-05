using Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Domain
{
    internal class Player : IPlayer
    {
        private readonly IList<ICard> _allCards = new List<ICard>();

        private IGame _game = null;

        public Guid Id { get; private set; }

        public virtual IEnumerable<ICard> AllCards => _allCards;

        public Player()
        {
            Id = Guid.NewGuid();
        }

        public void JoinGame(IGame game)
        {
            _game = game;
        }

        public virtual bool TryTakeNewCard()
        {
            var card = _game.GiveNewCard();
            var result = TryMoveCardNextStatus(card);
            if (result)
            {
                _allCards.Add(card);
                card.AssignTo(this);
            }
            return result;                
        }

        public virtual bool TryUnblockCard()
        {
            var card = TakeBlockedCard();
            var result = card != null;

            if (result)
            {
                card.UnBlock();
            }

            return result;
        }

        public void Toss(ICoin coin)
        {
            var coinResult = coin.Toss();

            if (coinResult == CoinResult.Head)
            {
                BlockCard();
                TryTakeNewCard();
            }
            else
            {
                var card = TakeCardReadyForAction();

                if(TryMoveCardNextStatus(card) ||
                    TryTakeNewCard() ||
                    TryUnblockCard())
                {
                    return;
                }
                else
                {
                    HelpOtherPlayer();
                }
            }
        }

        public virtual void HelpOtherPlayer()
        {
            _game.HelpOtherPlayer();
        }

        public virtual void BlockCard()
        {
            var card = TakeCardReadyForAction();

            if (card != null)
            {
                card.Block();
            }
        }

        public virtual bool TryMoveCardNextStatus(ICard card)
        {
            var result = card != null && card.TryMoveNextStatus();

            if (result && (card.Status == Status.Done))
            {
                _allCards.Remove(card);
            }
            return result;
        }

        private ICard TakeCardReadyForAction()
        {
            return AllCards.FirstOrDefault(x => !x.IsBlocked);
        }

        private ICard TakeBlockedCard()
        {
            return AllCards.FirstOrDefault(x => x.IsBlocked);
        }
    }
}
