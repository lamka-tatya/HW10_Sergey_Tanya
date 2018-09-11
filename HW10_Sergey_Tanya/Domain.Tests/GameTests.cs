using Domain.Tests.DSL;
using System;
using System.Linq;
using Xunit;

namespace Domain.Tests
{
    public class GameTests
    {
        [Fact]
        public void GameShouldContainAsManyCardsInWorkAsPlayersCount_WhenCreatedWithNoWipLimit()
        {
            var game = Builder
                .CreateGame
                .WithSomePlayer()
                .And()
                .WithOtherSomePlayer()
                .And()
                .WithWipLimit(0)
                .Please();

            Assert.Equal(2, game.CardsCount(Status.InWork));
        }

        [Fact]
        public void GameShouldHaveTwoDoneCardsAfterThreeRounds_WhenCoinResultIsTails_AndTwoPlayersInGame_AndWipLimitIsOne()
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

            game.PlayThreeRounds();

            Assert.Equal(2, game.CardsCount(Status.Done));
        }

        [Fact]
        public void GameShouldHaveOneCardInWorkAfterThreeRounds_WhenCoinResultIsHead_AndTwoPlayersInGame_AndWipLimitIsOne()
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

            game.PlayThreeRounds();

            Assert.Single(game.WorkCards);
        }

        [Fact]
        public void GameShouldHaveFourDoneCardsAfterFiveRounds_WhenCoinResultIsTails_AndTwoPlayersInGame_AndWipLimitIsTwo()
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

            game.PlayFiveRounds();

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

            game.TryMoveCardNextStatusTwice(card);

            Assert.Throws<CardStatusException>(() => game.TryMoveCardNextStatus(card));
        }

        [Fact]
        public void GameCanNotMoveCardInTestStatus_WhenItsStatusIsInWork_AndWhenTestWipLimitIsReached()
        {
            var game = Builder.CreateGame.WithSomePlayer().WithWipLimit(1).Please();
            var inTestCard = game.WorkCards.First();
            game.TryMoveCardNextStatus(inTestCard);

            Assert.False(game.TryAddSomeCardInTest());
        }

        [Fact]
        public void GameShouldCanMoveCardInDoneStatus_WhenWipLimitIsReached()
        {
            var cardInDone = Builder.CreateCard.Please();
            var game = Builder.CreateGame.WithSomePlayer().WithWipLimit(1).With(cardInDone).Please();
            game.TryMoveCardNextStatusTwice(cardInDone);
            var cardInTest = Builder.CreateCard.In(Status.Testing).Please();

            game.TryMoveCardNextStatus(cardInTest);

            Assert.Equal(Status.Done, cardInTest.Status);
        }
    }
}
