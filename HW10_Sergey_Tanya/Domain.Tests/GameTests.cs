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

        [Fact]
        public void GameCanNotMoveCardStatus_WhenItsStatusIsDone()
        {
            var card = Builder.CreateCard.Please();
            var game = Builder.CreateGame.WithSomePlayer().With(card).Please();

            for (int i = 0; i < 2; i++)
            {
                game.TryMoveCardNextStatus(card);
            }

            Assert.Throws<CardStatusException>(() => game.TryMoveCardNextStatus(card));
        }

        [Fact]
        public void GameCanNotMoveCardInTestStatus_WhenItsStatusIsInWork_AndWhenTestWipLimitIsReached()
        {
            var game = Builder.CreateGame.WithSomePlayer().WithWipLimit(1).Please();
            var inTestCard = game.WorkCards.First();
            game.TryMoveCardNextStatus(inTestCard);

            Assert.False(game.TryMoveCardNextStatus(Builder.CreateCard.In(Status.InWork).Please()));
        }

        [Fact]
        public void GameShouldCanMoveCardInDoneStatus_WhenWipLimitIsReached()
        {
            var cardInDone = Builder.CreateCard.Please();
            var game = Builder.CreateGame.WithSomePlayer().WithWipLimit(1).With(cardInDone).Please();
            game.TryMoveCardNextStatus(cardInDone);
            game.TryMoveCardNextStatus(cardInDone);
            var cardInTest = Builder.CreateCard.In(Status.Testing).Please();

            game.TryMoveCardNextStatus(cardInTest);

            Assert.Equal(Status.Done, cardInTest.Status);
        }
    }
}
