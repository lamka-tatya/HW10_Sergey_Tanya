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
    public class CardTests
    {
        [Fact]
        public void CardsPlayerIdShouldBeEqualsPlayersId_WhenGameCreatedWithTheOnlyOnePlayer()
        {
            var player = Builder.CreatePlayer.Please();
            var game = Builder.CreateGame.With(player).Please();
            var card = game.CardsThat(Status.InWork).First();

            Assert.Equal(player.Id, card.PlayerId);
        }

        [Fact]
        public void CardShoudBeBlocked_WhenCoinResultIsHead()
        {
            var game = Builder.CreateGame.WithHeadCoin().WithSomePlayer().Please();

            game.PlayRound();

            Assert.True(game.CardsThat(Status.InWork).First().IsBlocked);
        }

        [Fact]
        public void CardShoudMoveNext_WhenCoinResultIsTails()
        {
            var cardMock = new Mock<ICard>();
            var game = Builder.CreateGame.WithSomePlayer().With(cardMock.Object).WithTailsCoin().Please();

            game.PlayRound();

            cardMock.Verify(c => c.MoveNextStatus(), Times.Exactly(2));
        }

        [Fact]
        public void CardCantMoveStatus_WhenItStatusIsDone()
        {
            var card = new Card();

            for (int i = 0; i < 3; i++)
            {
                card.MoveNextStatus();
            }

            Assert.Throws<Exception>(() => card.MoveNextStatus());
        }
    }
}