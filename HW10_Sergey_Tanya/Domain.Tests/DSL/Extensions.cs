using Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Tests.DSL
{
    internal static class Extensions
    {
        public static void PlayThreeRounds(this IGame game)
        {
            PlayRounds(game, 3);
        }

        public static void PlayFiveRounds(this IGame game)
        {
            PlayRounds(game, 5);
        }

        public static void TryMoveCardNextStatusTwice(this IGame game, ICard card)
        {
            for (int i = 0; i< 2; i++)
            {
                game.TryMoveCardNextStatus(card);
            }
        }

        public static bool TryAddSomeCardInTest(this IGame game)
        {
            return game.TryMoveCardNextStatus(Builder.CreateCard.In(Status.InWork).Please());
        }

        private static void PlayRounds(IGame game, int count)
        {
            for (int i = 0; i < count; i++)
            {
                game.PlayRound();
            }
        }
    }
}
