using HW10_Sergey_Tanya;
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
            var board = new Board();
            var game = new Game(10, board);

            Assert.Equal(10, game.CardsCount(Status.InWork));
        }
    }

}
