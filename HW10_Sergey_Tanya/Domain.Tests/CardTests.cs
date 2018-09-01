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
using Domain.Extensions;

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
            var card = new Card(Builder.CreateBoard.Please());
            var game = Builder.CreateGame.WithSomePlayer().With(card).WithTailsCoin().Please();
            var prevStatus = card.Status;

            game.PlayRound();

            Assert.Equal(prevStatus.Next(), card.Status);
        }

        [Fact]
        public void CardCanNotMoveStatus_WhenItsStatusIsDone()
        {
            var card = Builder.CreateBoard.Please().GiveNewCard();

            for (int i = 0; i < 3; i++)
            {
                card.MoveNextStatus();
            }

            Assert.Throws<CardStatusException>(() => card.MoveNextStatus());
        }

        [Fact]
        public void ShouldNotMoveInWorkStatus_WhenWipLimitInWorkIsReached()
        {
            var board = Builder.CreateBoard.WithWipLimit((uint)1).Please();

            var firstNewCard = board.GiveNewCard();
            firstNewCard.MoveNextStatus();

            var secondNewCard = board.GiveNewCard();

            Assert.False(secondNewCard.MoveNextStatus());
        }


        [Fact]
        public void ShouldNotMoveTestStatus_WhenWipLimitInWorkIsReached()
        {
            var board = Builder.CreateBoard.WithWipLimit((uint)1).WithCardInTestStatus().Please();
            var secondNewCard = board.GiveNewCard();

            secondNewCard.MoveNextStatus();

            Assert.False(secondNewCard.MoveNextStatus());
        }
    }
}