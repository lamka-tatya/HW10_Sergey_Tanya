using Domain.Interfaces;
using System.Linq;
using Xunit;

namespace Domain.Tests.DSL
{
    internal static class AssertThat
    {
        public static void PlayerTookCardInWork(IGame game, IPlayer player, ICard card)
        {
            Assert.Equal(player.Id, card.PlayerId);
            Assert.Equal(Status.InWork, card.Status);
            Assert.Contains(card, game.WorkCards);
        }

        public static void PlayerHaveTwoCardsInWork(IGame game, IPlayer player)
        {
            var cards = game.WorkCards.ToArray();

            Assert.Equal(2, cards.Count());
            Assert.Equal(player.Id, cards[0].PlayerId);
            Assert.Equal(player.Id, cards[1].PlayerId);
            Assert.Equal(Status.InWork, cards[0].Status);
            Assert.Equal(Status.InWork, cards[1].Status);
        }
    }
}
