using Domain.Tests.DSL;
using System.Linq;
using Xunit;

namespace Domain.Tests
{
    public class WipLimitTests
    {
        [Fact]
        public void WipLimitShouldBeNotReachedForZero_WhenLimitSetToZero()
        {
            var wipLimit = Builder.CreateWipLimit.WithLimit(0).Please();

            Assert.False(wipLimit.IsReached(0));
        }

        [Fact]
        public void WipLimitShouldBeNotReachedForTen_WhenLimitSetToZero()
        {
            var wipLimit = Builder.CreateWipLimit.WithLimit(0).Please();

            Assert.False(wipLimit.IsReached(0));
        }

        [Fact]
        public void WipLimitShouldBeNotReachedForHundred_WhenLimitSetToZero()
        {
            var wipLimit = Builder.CreateWipLimit.WithLimit(0).Please();

            Assert.False(wipLimit.IsReached(0));
        }

        [Fact]
        public void WipLimitShouldBeNotReachedForOne_WhenLimitSetToTwo()
        {
            var wipLimit = Builder.CreateWipLimit.WithLimit(2).Please();

            Assert.False(wipLimit.IsReached(1));
        }

        [Fact]
        public void WipLimitShouldBeReachedForTwo_WhenLimitSetToTwo()
        {
            var wipLimit = Builder.CreateWipLimit.WithLimit(2).Please();

            Assert.True(wipLimit.IsReached(3));
        }

        [Fact]
        public void WipLimitShouldBeReachedForThree_WhenLimitSetToTwo()
        {
            var wipLimit = Builder.CreateWipLimit.WithLimit(2).Please();

            Assert.True(wipLimit.IsReached(3));
        }
    }
}