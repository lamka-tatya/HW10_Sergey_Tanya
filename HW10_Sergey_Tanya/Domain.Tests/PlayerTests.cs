using Domain.Tests.DSL;
using Domain;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Xunit;
using Moq;
using Domain.Interfaces;

namespace Domain.Tests
{
    public class PlayerTests
    {
        [Fact]
        public void PlayerShouldHaveOneCard_WhenGameStart()
        {
            var player = Builder.CreatePlayer.Please();
            var game = Builder.CreateGame.With(player).Please();

            Assert.Single(player.AllCards);
        }

        [Fact]
        public void PlayerShouldTakeOneMoreCard_WhenCoinResultIsTails_AndHasNoCardsToMove()
        {
            var playerMock = Builder.CreatePlayer.WithBlockedCards().MockPlease();
            var game = Builder.CreateGame.With(playerMock.Object).WithTailsCoin().Please();

            game.PlayRound();

            playerMock.Verify(p => p.TakeNewCard(), Times.Exactly(2));
        }
    }
}