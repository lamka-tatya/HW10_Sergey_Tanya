using Domain.Tests.DSL;
using Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Domain.Tests
{
    public class GameTests
    {
        [Fact]
        public void GameShouldContainAsManyCardsInWorkAsPlayersCount_WhenCreated()
        {
            var game = Builder
                .CreateGame
                .WithSomePlayer()
                .And()
                .WithOtherSomePlayer()
                .Please();

            Assert.Equal(2, game.CardsCount(Status.InWork));
        }

        [Fact]
        public void GameWithoutBoard_CanNotBeCreated()
        {
            Assert.Throws<NullBoardException>(() => new Game(null, new Coin()));
        }


        [Fact]
        public void GameWithoutCoin_CanNotBeCreated()
        {
            Assert.Throws<NullCoinException>(() => new Game(new Board(new WipLimit(1)), null));
        }
    }
    

}
