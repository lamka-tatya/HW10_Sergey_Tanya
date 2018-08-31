using Domain.Tests.DSL;
using Domain;
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
            var game = Builder.CreateGame.Please();
            var player = game.TakePlayers().First();
            var card = game.CardsThat(Status.InWork).First();

            Assert.Equal(player.Id, card.PlayerId);
        }

        [Fact]
        public void CardShoudBeBlocked_WhenCoinResultIsHead()
        {
            var game = Builder.CreateGame.WithHeadCoin().Please();

            game.PlayRound();

            Assert.True(game.CardsThat(Status.InWork).First().IsBlocked);
        }
    }
}