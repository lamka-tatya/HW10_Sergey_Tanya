namespace Domain
{
    public static class GameBuilder
    {
        public static GameCreator CreateGame(uint wipLimit) => new GameCreator(wipLimit);
    }

    public class GameCreator
    {
        private IGame _game = null;

        public GameCreator(uint wipLimit)
        {
            _game = new Game(new WipLimit(wipLimit));
        }

        public GameCreator WithPlayers(int playerCount)
        {
            for (int currentPlayer = 0; currentPlayer < playerCount; currentPlayer++)
            {
                _game.AddPlayer(new Player());
            }

            return this;
        }

        public GameCreator PlayRounds(int roundsCount)
        {
            for (int roundNum = 0; roundNum < roundsCount; roundNum++)
            {
                _game.PlayRound();
            }

            return this;
        }

        public int DoneCardsCount()
        {
            return _game.CardsCount(Status.Done);
        }
    }
}
