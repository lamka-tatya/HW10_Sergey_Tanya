using Domain.Interfaces;
using Domain.Tests.DSL;
using Moq;
using System.Linq;
using Xunit;

namespace Domain.Tests
{
    public class PlayerTests
    {
        [Fact]
        public void PlayerShouldTakeOneCard_WhenGameStart()
        {
            var playerMock = Builder.CreatePlayer.MockPlease();

            var game = Builder.CreateGame.With(playerMock).Please();

            Assert.Single(playerMock.Object.AllCards);
        }

        [Fact]
        public void PlayerShouldTryTakeOneMoreCard_WhenCoinResultIsTails_AndHasNoCardsToMove()
        {
            var playerMock = Builder.CreatePlayer.WithBlockedCards().MockPlease();
            var game = Builder.CreateGame.With(playerMock).WithTailsCoin().Please();
            var prevCardsCount = playerMock.Object.AllCards.Count();

            game.PlayRound();

            Assert.Equal(++prevCardsCount, playerMock.Object.AllCards.Count());
        }

        [Fact]
        public void PlayerShouldNotTakeNewCard_WhenInWorkWipLimitIsReached()
        {
            var player = Builder.CreatePlayer.MockPlease();
            var game = Builder.CreateGame.With(player).And().WithReachedWipLimit().Please();

            var newCardResult = player.Object.TryTakeNewCard();

            Assert.False(newCardResult);
        }

        [Fact]
        public void PlayerShouldTryUnblockCard_WhenCoinResultIsTails_AndWhenWhenWipLimitIsReached_AndHasNoCardsToMoveNextStatus()
        {
            var playerMock = Builder.CreatePlayer.WithBlockedCards().MockPlease();
            var game = Builder.CreateGame
                .With(playerMock)
                .And()
                .WithTailsCoin()
                .And()
                .WithReachedWipLimit().Please();

            game.PlayRound();

            playerMock.Verify(p => p.TryUnblockCard(), Times.Once);
        }

        [Fact]
        public void PlayerShouldHelpOtherPlayer_WhenCoinResultIsTails_AndWhenWhenWipLimitIsReached_AndHasNoCardsToMoveNextStatus_AndHasNoCardsToUnlock()
        {
            var playerMock = Builder.CreatePlayer.MockPlease();
            var game = Builder.CreateGame
                .With(playerMock)
                .And()
                .WithTailsCoin()
                .And()
                .WithReachedWipLimit().Please();

            game.PlayRound();

            playerMock.Verify(p => p.HelpOtherPlayer(), Times.Once);
        }

        [Fact]
        public void PlayerShouldTryTakeNewCard_WhenCoinResultIsHead()
        {
            var playerMock = Builder.CreatePlayer.MockPlease();
            var game = Builder.CreateGame.With(playerMock)
                                         .And()
                                         .WithHeadCoin().Please();
            var prevCardsCount = playerMock.Object.AllCards.Count();

            game.PlayRound();

            Assert.Equal(++prevCardsCount, playerMock.Object.AllCards.Count());
        }
    }
}