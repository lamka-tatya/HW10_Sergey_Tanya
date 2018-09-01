using Domain.Tests.DSL;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Xunit;

namespace Domain.Tests
{
    public class BoardTests
    {
        [Fact]
        public void BoardShouldReturnNull_WhenWipLimitIsReached()
        {
            var board = Builder.CreateBoard.WithReachedWipLimit().Please();

            var newCard = board.GiveNewCard();

            Assert.Null(newCard);
        }
    }
}