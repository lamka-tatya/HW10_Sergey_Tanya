using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HW10_Sergey_Tanya
{
    public class Game
    {
        private IList<Player> _players = new List<Player>();
        private IList<Card> _cards = new List<Card>();
        private Board _board;

        public Game(int playersCount, Board board)
        {
            _board = board ?? throw new BoardIsNullException();

            for (int i = 0; i < playersCount; i++)
            {
                var player = new Player();
                _players.Add(player);

                var newCard = _board.GiveNewCard();
                newCard.MoveNextStatus();
                _cards.Add(newCard);
            }
        }

        public int CardsCount(Status status)
        {
            return _cards.Count(c => c.Status == status);
        }
    }
}
