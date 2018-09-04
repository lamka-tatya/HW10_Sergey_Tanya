﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Interfaces;

namespace Domain
{
    internal class Player : IPlayer
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

                // todo возможно порядок обработки стоит поменять
                if((card != null && card.TryMoveNextStatus()) ||
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

        private ICard TakeCardReadyForAction()
        {
            return AllCards.FirstOrDefault(x => x.Status != Status.Done && !x.IsBlocked); // TODO добавить проверку на done, либо убирать карту из карт игрока
        }

        private ICard TakeBlockedCard()
        {
            return AllCards.FirstOrDefault(x => x.Status != Status.Done && x.IsBlocked); // TODO добавить проверку на done, либо убирать карту из карт игрока
        }

        public virtual void BlockCard()
        {
            var card = TakeCardReadyForAction();

            if (card != null)
            {
                card.Block();
            }
        }
    }
}
