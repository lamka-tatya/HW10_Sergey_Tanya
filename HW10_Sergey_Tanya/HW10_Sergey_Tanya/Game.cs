using Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

            _players.Add(player); // todo проверить на null
        }

        public ICard GiveNewCard()
        {
            var card = _board.GiveNewCard();
            return card;
        }

        public void PlayRound()
        {
            foreach (var player in _players)
            {
                player.Toss(_coin);
            }
        }

        public IEnumerable<IPlayer> TakePlayers()
        {
            return _players;
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

                if (cardToMove != null && cardToMove.TryMoveNextStatus())
                {
                    return;
                }
                else if (cardToUnBlock != null)
                {
                    cardToUnBlock.UnBlock();
                    return;
                }
            }
        }
    }
}
