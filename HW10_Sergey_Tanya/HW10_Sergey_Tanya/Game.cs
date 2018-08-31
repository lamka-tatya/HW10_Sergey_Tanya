using Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    public class Game
    {
        private IList<Player> _players = new List<Player>();
        private IList<ICard> _cards = new List<ICard>();
        private IBoard _board;
        private ICoin _coin;

        public Game(IBoard board, ICoin coin)
        {
            _coin = coin ?? throw new NullCoinException(); 
            _board = board ?? throw new NullBoardException();
        }

        public void AddPlayer(Player player)
        {
            var card = _board.GiveNewCard();
            player.Take(card);
            _cards.Add(card);

            _players.Add(player); // todo проверить на null
        }

        public void PlayRound()
        {
            foreach(var player in _players)
            {
                player.Toss(_coin);
            }
        }

        public IEnumerable<ICard> CardsThat(Status inWork)
        {
            return _cards.Where(x => x.Status == inWork);
        }

        public IEnumerable<Player> TakePlayers()
        {
            return _players;
        }

        public int CardsCount(Status status)
        {
            return CardsThat(status).Count();
        }
    }
}
