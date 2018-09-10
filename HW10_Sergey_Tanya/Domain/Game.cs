using Domain.Extensions;
using Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Domain
{
    internal class Game : IGame
    {
        private IList<IPlayer> _players = new List<IPlayer>();
        private ICoin _coin;
        private IList<ICard> _cards;

        public IWipLimit WipLimit { get; }


        public IEnumerable<ICard> DoneCards => this.CardsThat(Status.Done);

        public IEnumerable<ICard> WorkCards => this.CardsThat(Status.InWork);

        public Game(IWipLimit wipLimit, ICoin coin)
        {
            _coin = coin ?? throw new NullCoinException();
            _cards = new List<ICard>();
            WipLimit = wipLimit;
        }

        public ICard GiveNewCard()
        {
            var card = new Card();
            _cards.Add(card);
            return card;
        }

        public IEnumerable<ICard> CardsThat(Status status)
        {
            return _cards.Where(x => x.Status == status);
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

            player.JoinGame(this);
            player.TryTakeNewCard();

            _players.Add(player);
        }

        public void PlayRound()
        {
            foreach (var player in _players)
            {
                player.Toss(_coin);
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

                if (cardToMove != null && GetPlayerById(cardToMove.PlayerId).TryMoveCardNextStatus(cardToMove))
                {
                    return;
                }

                if (cardToUnBlock != null)
                {
                    cardToUnBlock.UnBlock();
                }
            }
        }

        public bool TryMoveNextStatus(ICard card)
        {
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



        private IPlayer GetPlayerById(Guid playerId)
        {
            return _players.FirstOrDefault(p => p.Id == playerId);
        }
    }
}
