﻿using Domain.Extensions;
using Domain.Tests.DSL;
using System.Linq;
using Xunit;

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
        public void ShouldNotMoveInWorkStatus_WhenInWorkWipLimitIsReached()
        {
            var board = Builder.CreateBoard.WithWipLimit((uint)1).Please();

            var inWorkCard = board.GiveNewCard();
            inWorkCard.MoveNextStatus();

            var newCard = board.GiveNewCard();

            Assert.False(newCard.MoveNextStatus());
        }


        [Fact]
        public void ShouldNotMoveInTestStatus_WhenTestWipLimitIsReached()
        {
            var board = Builder.CreateBoard.WithWipLimit((uint)1).WithCardInTestStatus().Please();
            var inWorkCard = board.GiveNewCard();

            inWorkCard.MoveNextStatus();

            Assert.False(inWorkCard.MoveNextStatus());
        }
    }
}