using Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Domain
{
    internal class Game : IGame
    {
        private IList<IPlayer> _players = new List<IPlayer>();
        private IBoard _board;
        private ICoin _coin;

        public IEnumerable<ICard> DoneCards => _board.CardsThat(Status.Done);

        public IEnumerable<ICard> WorkCards => _board.CardsThat(Status.InWork);

        public Game(IBoard board, ICoin coin)
        {
            _coin = coin ?? throw new NullCoinException();
            _board = board ?? throw new NullBoardException();
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

        public ICard GiveNewCard()
        {
            return _board.GiveNewCard();
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
            return _board.CardsThat(status).Count();
        }

        public void HelpOtherPlayer()
        {
            foreach (var status in new[] { Status.Testing, Status.InWork })
            {
                var cardToMove = _board.CardsThat(status).FirstOrDefault(x => !x.IsBlocked);
                var cardToUnBlock = _board.CardsThat(status).FirstOrDefault(x => x.IsBlocked);

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

        private IPlayer GetPlayerById(Guid playerId)
        {
            return _players.FirstOrDefault(p => p.Id == playerId);
        }
    }
}
