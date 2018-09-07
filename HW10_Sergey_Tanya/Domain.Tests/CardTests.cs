using Domain.Extensions;
using Domain.Tests.DSL;
using System;
using System.Linq;
using Xunit;

namespace Domain.Tests
{
    public class CardTests
    {
        [Fact]
        public void CardsPlayerIdShouldBeEqualsPlayersId_WhenGameCreatedWithTheOnlyOnePlayer()
        {
            var player = Builder.CreatePlayer.MockPlease();
            var game = Builder.CreateGame.With(player).Please();
            var card = game.WorkCards.First();

            Assert.Equal(player.Object.Id, card.PlayerId);
        }

        [Fact]
        public void CardShoudBeBlocked_WhenCoinResultIsHead()
        {
            var game = Builder.CreateGame.WithHeadCoin().WithSomePlayer().Please();

            game.PlayRound();

            Assert.True(game.WorkCards.First().IsBlocked);
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
                card.TryMoveNextStatus();
            }

            Assert.Throws<CardStatusException>(() => card.TryMoveNextStatus());
        }

        [Fact]
        public void CardShouldNotMoveInWorkStatus_WhenInWorkWipLimitIsReached()
        {
            var board = Builder.CreateBoard.WithWipLimit((uint)1).Please();
            var inWorkCard = board.GiveNewCard();

            inWorkCard.TryMoveNextStatus();
            var newCard = board.GiveNewCard();

            Assert.False(newCard.TryMoveNextStatus());
        }

        [Fact]
        public void CardShouldNotMoveInTestStatus_WhenTestWipLimitIsReached()
        {
            var board = Builder.CreateBoard.WithWipLimit((uint)1).WithCardInTestStatus().Please();
            var inWorkCard = board.GiveNewCard();

            inWorkCard.TryMoveNextStatus();

            Assert.False(inWorkCard.TryMoveNextStatus());
        }

        [Fact]
        public void CardShouldCanMoveCardInDoneStatus_WhenWipLimitIsReached()
        {
            var board = Builder.CreateBoard.WithWipLimit((uint)1)
                                           .And()
                                           .WithCardInDoneStatus()
                                           .And()
                                           .WithCardInTestStatus()
                                           .Please();
            var card = board.CardsThat(Status.Testing).First();

            card.TryMoveNextStatus();

            Assert.Equal(Status.Done, card.Status);
        }

        [Fact]
        public void CardShouldNotBeCreatedWithoutBoard()
        {
            Assert.Throws<NullBoardException>(() => new Card(null));
        }
    }
}