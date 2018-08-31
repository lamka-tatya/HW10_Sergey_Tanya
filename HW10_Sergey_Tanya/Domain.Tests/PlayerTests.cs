using Domain.Tests.DSL;
using HW10_Sergey_Tanya;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Xunit;

namespace Domain.Tests
{
    public class PlayerTests
    {
        [Fact]
        public void PlayerShouldTakeInWorkOnlyCardsInNewStatus()
        {
            var player = new Player();
            var card = new Card();
            card.MoveNextStatus();

            Assert.Throws<CardStatusIsNotNewException>(() => player.Take(card));
        }

        [Fact]
        public void PlayerShouldHaveOneCard_WhenGameStart()
        {
            var game = Builder.CreateGame.Please();
            var player = game.TakePlayers().First();

            Assert.Single(player.AllCards);
        }
    }
}