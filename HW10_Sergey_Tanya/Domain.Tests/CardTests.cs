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
            var card = Builder.CreateCard.Please();
            var game = Builder.CreateGame.WithSomePlayer().With(card).WithTailsCoin().Please();

            game.PlayRound();

            Assert.Equal(Status.Testing, card.Status);
        }


        [Fact]
        public void ShouldNotBeInWorkStatus_WhenInWorkWipLimitIsReached()
        {
            var card = Builder.CreateCard.Please();
            var game = Builder.CreateGame.WithSomePlayer().With(card).WithReachedWipLimit().Please();

            Assert.Equal(Status.New, card.Status);
        }
    }
}