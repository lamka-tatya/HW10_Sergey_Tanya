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
        public void CardShoudBeInTesting_WhenCoinResultIsTails()
        {
            var card = new Card();
            var game = Builder.CreateGame.WithSomePlayer().With(card).WithTailsCoin().Please();
 
            game.PlayRound();

            Assert.Equal(Status.Testing, card.Status);
        }

        [Fact]
        public void CardCanNotMoveStatus_WhenItsStatusIsDone()
        {
            var card = new Card();
            var game = Builder.CreateGame.WithSomePlayer().With(card).Please();

            for (int i = 0; i < 2; i++)
            {
                game.TryMoveCardNextStatus(card);
            }

            Assert.Throws<CardStatusException>(() => game.TryMoveCardNextStatus(card));
        }

        [Fact]
        public void ShouldNotMoveInWorkStatus_WhenInWorkWipLimitIsReached()
        {
            var card = new Card();
            var game = Builder.CreateGame.WithSomePlayer().With(card).WithReachedWipLimit().Please();
            
            Assert.Equal(Status.New, card.Status);
        }

        //[Fact]
        //public void ShouldNotMoveInTestStatus_WhenTestWipLimitIsReached()
        //{
        //    var board = Builder.CreateBoard.WithWipLimit((uint)1).WithCardInTestStatus().Please();
        //    var inWorkCard = board.GiveNewCard();

        //    inWorkCard.TryMoveNextStatus();

        //    Assert.False(inWorkCard.TryMoveNextStatus());
        //}

        //[Fact]
        //public void ShouldCanMoveCardInDoneStatus_WhenWipLimitIsReached()
        //{
        //    var board = Builder.CreateBoard.WithWipLimit((uint)1)
        //                                   .And()
        //                                   .WithCardInDoneStatus()
        //                                   .And()
        //                                   .WithCardInTestStatus()
        //                                   .Please();
        //    var card = board.CardsThat(Status.Testing).First();

        //    card.TryMoveNextStatus();

        //    Assert.Equal(Status.Done, card.Status);
        //}
    }
}