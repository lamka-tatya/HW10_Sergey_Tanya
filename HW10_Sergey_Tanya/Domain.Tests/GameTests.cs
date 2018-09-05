using Domain.Tests.DSL;
using System.Linq;
using Xunit;

namespace Domain.Tests
{
    public class GameTests
    {
        [Fact]
        public void GameShouldContainAsManyCardsInWorkAsPlayersCount_WhenCreated()
        {
            var game = Builder
                .CreateGame
                .WithSomePlayer()
                .And()
                .WithOtherSomePlayer()
                .Please();

            Assert.Equal(2, game.CardsCount(Status.InWork));
        }

        [Fact]
        public void GameWithoutBoard_CanNotBeCreated()
        {
            Assert.Throws<NullBoardException>(() => new Game(null, new Coin()));
        }


        [Fact]
        public void GameWithoutCoin_CanNotBeCreated()
        {
            Assert.Throws<NullCoinException>(() => new Game(new Board(new WipLimit(1)), null));
        }

        [Fact]
        public void GameShouldHaveTwoDoneCards_WhenCoinResultIsTails_AndTwoPlayersInGame_AndWipLimitIsOne()
        {
            var game = Builder
                    .CreateGame
                    .WithSomePlayer()
                    .And()
                    .WithOtherSomePlayer()
                    .And()
                    .WithTailsCoin()
                    .And()
                    .WithWipLimit(1)
                    .Please();

            game.PlayRound();
            game.PlayRound();
            game.PlayRound();

            Assert.Equal(2, game.CardsCount(Status.Done));
        }

        [Fact]
        public void GameShouldHaveTwoDoneCards_WhenCoinResultIsHead_AndTwoPlayersInGame_AndWipLimitIsOne()
        {
            var game = Builder
                    .CreateGame
                    .WithSomePlayer()
                    .And()
                    .WithOtherSomePlayer()
                    .And()
                    .WithHeadCoin()
                    .And()
                    .WithWipLimit(1)
                    .Please();

            game.PlayRound();
            game.PlayRound();
            game.PlayRound();

            Assert.Single(game.WorkCards);
        }

        [Fact]
        public void GameShouldHaveFourDoneCards_WhenCoinResultIsTails_AndTwoPlayersInGame_AndWipLimitIsTwo()
        {
            var game = Builder
                    .CreateGame
                    .WithSomePlayer()
                    .And()
                    .WithOtherSomePlayer()
                    .And()
                    .WithTailsCoin()
                    .And()
                    .WithWipLimit(2)
                    .Please();

            game.PlayRound();
            game.PlayRound();
            game.PlayRound();
            game.PlayRound();
            game.PlayRound();

            Assert.Equal(4, game.CardsCount(Status.Done));
        }

        [Fact]
        public void GameCouldNotAddEmptyPlayer()
        {
            Assert.Throws<NullPlayerException>(() => Builder.CreateGame.Please().AddPlayer(null));
        }
    }   

}
