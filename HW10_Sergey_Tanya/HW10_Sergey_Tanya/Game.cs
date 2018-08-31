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
        private IList<Card> _cards = new List<Card>();
        private Board _board;
        private ICoin _coin;

        public Game(uint playersCount, Board board, ICoin coin)
        {
            _coin = coin; // todo
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
            foreach(var player in _players)
            {
                var coinResult = _coin.Toss();

                if (coinResult == CoinResult.Head)
                {
                    var notBlockedCard = player.AllCards.FirstOrDefault(x => !x.IsBlocked); // TODO добавить проверку на done, либо убирать карту из карт игрока

                    if (notBlockedCard != null)
                    {
                        notBlockedCard.Block();
                    }
                }
            }
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
