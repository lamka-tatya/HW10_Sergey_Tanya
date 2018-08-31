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
        private IList<Card> _cards = new List<Card>();
        private Board _board;

        public Game(uint playersCount, Board board)
        {
            _board = board ?? throw new BoardIsNullException();

            if (playersCount == 0)
            {
                throw new PlayersEmptyException();
            }

            for (int i = 0; i < playersCount; i++)
            {
                var player = new Player();
                _players.Add(player);

                var card = _board.GiveNewCard();
                player.Take(card);
                _cards.Add(card);
            }
        }

        public void PlayRound()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Card> CardsThat(Status inWork)
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
