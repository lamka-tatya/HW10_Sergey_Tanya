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

        //[Fact]
        //public void PlayerShouldTryTakeNewCard_WhenCoinResultIsHead()
        //{
        //    var playerMock = Builder.CreatePlayer.MockPlease();
        //    var game = Builder.CreateGame.With(playerMock.Object)
        //                                 .And()
        //                                 .WithHeadCoin().Please();

        //    game.PlayRound();

        //    playerMock.Verify(p => p.TryTakeNewCard(), Times.Exactly(2));
        //}

        //[Fact]
        //public void PlayerShouldNotTakeNewCard_WhenInWorkWipLimitIsReached()
        //{
        //    var player = Builder.CreatePlayer.Please();
        //    var game = Builder.CreateGame.With(player).And().WithReachedWipLimit().Please();

        //    var newCardResult = player.TryTakeNewCard();

        //    Assert.False(newCardResult);
        //}

        //[Fact]
        //public void PlayerShouldTryUnblockCard_WhenCoinResultIsTails_AndWhenWhenWipLimitIsReached_AndHasNoCardsToMoveNextStatus()
        //{
        //    var playerMock = Builder.CreatePlayer.WithBlockedCards().MockPlease();
        //    var game = Builder.CreateGame
        //        .With(playerMock.Object)
        //        .And()
        //        .WithTailsCoin()
        //        .And()
        //        .WithReachedWipLimit().Please();

        //    game.PlayRound();

        //    playerMock.Verify(p => p.TryUnblockCard(), Times.Once);
        //}

        //[Fact]
        //public void PlayerShouldHelpOtherPlayer_WhenCoinResultIsTails_AndWhenWhenWipLimitIsReached_AndHasNoCardsToMoveNextStatus_AndHasNoCardsToUnlock()
        //{
        //    var playerMock = Builder.CreatePlayer.MockPlease();
        //    var game = Builder.CreateGame
        //        .With(playerMock.Object)
        //        .And()
        //        .WithTailsCoin()
        //        .And()
        //        .WithReachedWipLimit().Please();

        //    game.PlayRound();

        //    playerMock.Verify(p => p.HelpOtherPlayer(), Times.Once);
        //}

        //[Fact]
        //public void PlayerShouldTryBlockCard_WhenCoinResultIsHead()
        //{
        //    var playerMock = Builder.CreatePlayer.MockPlease();
        //    var game = Builder.CreateGame.With(playerMock.Object)
        //                                 .And()
        //                                 .WithHeadCoin().Please();

        //    game.PlayRound();

        //    playerMock.Verify(p => p.BlockCard(), Times.Once);
        //}

        //[Fact]
        //public void PlayerShouldTryMoveCardNextStatus_WhenCoinResultIsTails()
        //{
        //    var playerMock = Builder.CreatePlayer.MockPlease();
        //    var game = Builder.CreateGame.With(playerMock.Object).And().WithTailsCoin().Please();

        //    game.PlayRound();

        //    playerMock.Verify(p => p.TryMoveCardNextStatus(It.IsAny<ICard>()), Times.Once);
        //}
    }
}
