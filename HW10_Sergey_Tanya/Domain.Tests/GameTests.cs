using Domain.Tests.DSL;
using Moq;
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

        [Fact]
        public void GameShouldGiveOneCardToPlayer_WhenGameStart()
        {
            var player = Builder.CreatePlayer.MockPlease();
            var card = Builder.CreateCard.Please();
            var game = Builder.CreateGame.With(player).And().With(card).Please();

            AssertThat.PlayerTookCardInWork(game, player.Object, card);
        }

        [Fact]
        public void GameShouldGiveOneMoreCardToPlayer_WhenCoinResultIsTails_AndHasNoCardsToMove()
        {
            var player = Builder.CreatePlayer.MockPlease();
            var game = Builder.CreateGame.With(player).WithTailsCoin().Please();
            game.WorkCards.First().Block();

            game.PlayRound();

            AssertThat.PlayerHaveTwoCardsInWork(game, player.Object);
        }

        [Fact]
        public void GameShouldGiveOneMoreCardToPlayer_WhenCoinResultIsHead()
        {
            var player = Builder.CreatePlayer.MockPlease();
            var game = Builder.CreateGame.With(player).And().WithHeadCoin().Please();

            game.PlayRound();

            AssertThat.PlayerHaveTwoCardsInWork(game, player.Object);
        }

        [Fact]
        public void GameShouldNotGiveNewCard_WhenInWorkWipLimitIsReached()
        {
            var game = Builder.CreateGame.WithReachedWipLimit().Please();

            var newCardResult = game.TryTakeNewCard(Builder.CreatePlayer.MockPlease().Object.Id);

            Assert.False(newCardResult);
        }

        [Fact]
        public void GameShouldUnblockCard_WhenCoinResultIsTails_AndWhenWhenWipLimitIsReached_AndHasNoCardsToMoveNextStatus()
        {
            var game = Builder.CreateGame.WithSomePlayer().And().WithTailsCoin().And().WithWipLimit(1).Please();
            var cardToBlock = game.WorkCards.First();
            cardToBlock.Block();

            game.PlayRound();

            Assert.False(cardToBlock.IsBlocked);
        }

        [Fact]
        public void GameShouldHelpOtherPlayer_WhenCoinResultIsTails_AndWhenWhenWipLimitIsReached_AndHasNoCardsToMoveNextStatus_AndHasNoCardsToUnlock()
        {
            var gameMock = Builder.CreateGame
                .WithSomePlayer()
                .And()
                .WithTailsCoin()
                .And()
                .WithReachedWipLimit().MockPlease();

            gameMock.Object.PlayRound();

            gameMock.Verify(p => p.HelpOtherPlayer(), Times.Once);
        }

        [Fact]
        public void GameShouldMoveTestCardInDone_WhenHelpOtherPlayer()
        {
            var game = Builder.CreateGame.WithSomePlayer().And().WithTailsCoin().Please();
            var cardInTest = game.WorkCards.First();
            game.TryMoveCardNextStatus(cardInTest);

            game.HelpOtherPlayer();

            Assert.Equal(Status.Done, cardInTest.Status);
        }

        [Fact]
        public void GameShouldUnblockBlockedCard_WhenHelpOtherPlayer_AndNoCardsInTest()
        {
            var game = Builder.CreateGame.WithSomePlayer().And().Please();
            var cardToBlock = game.WorkCards.First();
            cardToBlock.Block();

            game.HelpOtherPlayer();

            Assert.False(cardToBlock.IsBlocked);
        }

        [Fact]
        public void GameShouldBlockCard_WhenCoinResultIsHead()
        {
            var gameMock = Builder.CreateGame.WithSomePlayer().And().WithHeadCoin().MockPlease();

            gameMock.Object.PlayRound();

            gameMock.Verify(p => p.BlockCard(It.IsAny<Guid>()), Times.Once);
        }
    }
}
