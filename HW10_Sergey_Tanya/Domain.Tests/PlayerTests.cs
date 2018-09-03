using Domain.Tests.DSL;
using Moq;
using Xunit;

namespace Domain.Tests
{
    public class PlayerTests
    {
        [Fact]
        public void PlayerShouldTakeOneCard_WhenGameStart()
        {
            var playerMock = Builder.CreatePlayer.MockPlease();

            var game = Builder.CreateGame.With(playerMock.Object).Please();

            playerMock.Verify(p => p.TryTakeNewCard(), Times.Once);
        }

        [Fact]
        public void PlayerShouldTryTakeOneMoreCard_WhenCoinResultIsTails_AndHasNoCardsToMove()
        {
            var playerMock = Builder.CreatePlayer.WithBlockedCards().MockPlease();
            var game = Builder.CreateGame.With(playerMock.Object).WithTailsCoin().Please();

            game.PlayRound();

            playerMock.Verify(p => p.TryTakeNewCard(), Times.Exactly(2));
        }

        [Fact]
        public void PlayerShouldNotTakeNewCard_WhenInWorkWipLimitIsReached()
        {
            var player = Builder.CreatePlayer.Please();
            var game = Builder.CreateGame.With(player).And().WithReachedWipLimit().Please();

            var newCardResult = player.TryTakeNewCard();

            Assert.False(newCardResult);
        }

        [Fact]
        public void PlayerShouldTryUnblockCard_WhenCoinResultIsTails_WhenWhenWipLimitIsReached_AndHasNoCardsToMoveNextStatus()
        {
            var playerMock = Builder.CreatePlayer.WithBlockedCards().MockPlease();
            var game = Builder.CreateGame
                .With(playerMock.Object)
                .And()
                .WithTailsCoin()
                .And()
                .WithReachedWipLimit().Please();

            game.PlayRound();

            playerMock.Verify(p => p.TryUnblockCard(), Times.Once);
        }
    }
}