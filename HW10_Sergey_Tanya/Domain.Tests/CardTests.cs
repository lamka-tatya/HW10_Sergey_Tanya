using HW10_Sergey_Tanya;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Xunit;

namespace Domain.Tests
{
    public class CardTests
    {
        [Fact]
        public void CardsPlayerIdShouldBeEqualsPlayersId_WhenGameCreatedWithTheOnlyOnePlayer()
        {
            var board = new Board();
            var game = new Game(1, board);
            var player = game.TakePlayers().First();
            var card = game.CardsThat(Status.InWork).First();

            Assert.Equal(player.Id, card.PlayerId);
        }
    }
}