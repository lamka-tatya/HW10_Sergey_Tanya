using Domain.Interfaces;
using Domain.Tests.DSL;
using Moq;
using Xunit;

namespace Domain.Tests
{
    public class PlayerTests
    {
        //[Fact]
        //public void PlayerShouldTakeOneCard_WhenGameStart()
        //{
        //    var playerMock = Builder.CreatePlayer.MockPlease();

        //    var game = Builder.CreateGame.With(playerMock.Object).Please();

        //    playerMock.Verify(p => p.TryTakeNewCard(), Times.Once);
        //}

        //[Fact]
        //public void PlayerShouldTryTakeOneMoreCard_WhenCoinResultIsTails_AndHasNoCardsToMove()
        //{
        //    var playerMock = Builder.CreatePlayer.WithBlockedCards().MockPlease();
        //    var game = Builder.CreateGame.With(playerMock.Object).WithTailsCoin().Please();

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