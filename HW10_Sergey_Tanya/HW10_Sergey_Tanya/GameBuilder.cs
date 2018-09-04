using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    public class GameBuilder
    {
        private Game _game = null;

        public GameBuilder CreateGame(uint wipLimit)
        {
            _game = new Game(new Board(new WipLimit(wipLimit)), new Coin());

            return this;
        }

        public GameBuilder WithPlayers(int playerCount)
        {
            for (int currentPlayer = 0; currentPlayer < playerCount; currentPlayer++)
            {
                _game.AddPlayer(new Player());
            }

            return this;
        }

        public GameBuilder PlayRounds(int roundsCount)
        {
            for (int roundNum = 0; roundNum < roundsCount; roundNum++)
            {
                _game.PlayRound();
            }

            return this;
        }

        public int DoneCardsCount()
        {
            return _game.CardsThat(Status.Done).Count();
        }
    }
}
