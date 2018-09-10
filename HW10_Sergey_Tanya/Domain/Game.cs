﻿using Domain.Extensions;
using Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Domain
{
    internal class Game : IGame
    {
        private IList<IPlayer> _players = new List<IPlayer>();
        private IList<ICard> _cards;

        public IWipLimit WipLimit { get; }

        public IEnumerable<ICard> DoneCards => this.CardsThat(Status.Done);

        public IEnumerable<ICard> WorkCards => this.CardsThat(Status.InWork);

        public Game(IWipLimit wipLimit)
        {
            _cards = new List<ICard>();
            WipLimit = wipLimit;
        }

        public bool WipLimitIsReached(Status status)
        {
            if (status == Status.Done)
            {
                return false;
            }

            return WipLimit.IsReached((uint)CardsThat(status).Count());
        }

        public void AddPlayer(IPlayer player)
        {
            if (player == null)
            {
                throw new NullPlayerException();
            }

            this.TryTakeNewCard(player.Id);

            _players.Add(player);
        }

        public void PlayRound()
        {
            foreach (var player in _players)
            {
                var coinResult = player.TossCoin();
                var playerId = player.Id;

                if (coinResult == CoinResult.Head)
                {
                    BlockCard(playerId);
                    TryTakeNewCard(playerId);
                }
                else
                {
                    var card = TakeCardReadyForAction(playerId);

                    if (TryMoveCardNextStatus(card) ||
                        TryTakeNewCard(playerId) ||
                        TryUnblockCard(playerId))
                    {
                        return;
                    }
                    else
                    {
                        HelpOtherPlayer();
                    }
                }
            }
        }

        public int CardsCount(Status status)
        {
            return this.CardsThat(status).Count();
        }

        public void HelpOtherPlayer()
        {
            foreach (var status in new[] { Status.Testing, Status.InWork })
            {
                var cardToMove = this.CardsThat(status).FirstOrDefault(x => !x.IsBlocked);
                var cardToUnBlock = this.CardsThat(status).FirstOrDefault(x => x.IsBlocked);

                if (cardToMove != null && this.TryMoveCardNextStatus(cardToMove))
                {
                    return;
                }

                if (cardToUnBlock != null)
                {
                    cardToUnBlock.UnBlock();
                }
            }
        }

        public virtual bool TryTakeNewCard(Guid playerId)
        {
            var card = new Card();
            var result = this.TryMoveCardNextStatus(card);

            if (result)
            {
                card.AssignTo(playerId);
                _cards.Add(card);
            }

            return result;
        }

        public virtual bool TryMoveCardNextStatus(ICard card)
        {
            if (card == null)
            {
                return false;
            }
       
            if (card.Status == Status.Done)
            {
                throw new CardStatusException();
            }

            if (!this.WipLimitIsReached(card.Status.Next()))
            {
                card.Status = card.Status.Next();
                return true;
            }

            return false;
        }

        


        public virtual bool TryUnblockCard(Guid playerId)
        {
            var card = TakeBlockedCard(playerId);
            var result = card != null;

            if (result)
            {
                card.UnBlock();
            }

            return result;
        }


        public virtual void BlockCard(Guid playerId)
        {
            var card = TakeCardReadyForAction(playerId);

            if (card != null)
            {
                card.Block();
            }
        }

        private IEnumerable<ICard> CardsThat(Status status)
        {
            return _cards.Where(x => x.Status == status);
        }


        private ICard TakeCardReadyForAction(Guid playerId)
        {
            return _cards.FirstOrDefault(x => x.PlayerId == playerId && x.Status != Status.Done && !x.IsBlocked);
        }

        private ICard TakeBlockedCard(Guid playerId)
        {
            return _cards.FirstOrDefault(x => x.PlayerId == playerId && x.Status != Status.Done && x.IsBlocked);
        }
    }
}
