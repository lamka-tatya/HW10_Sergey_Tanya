using System;
using Domain.Interfaces;
using Moq;

namespace Domain.Tests.DSL
{
    public class BoardBuilder
    {
        private Mock<IWipLimit> _wipLimit;

        private uint _limit = 1;

        public BoardBuilder WithReachedWipLimit()
        {
            _wipLimit = new Mock<IWipLimit>();
            _wipLimit.Setup(w => w.IsReached(It.IsAny<uint>())).Returns(true);

            return this;
        }

        public BoardBuilder WithWipLimit(uint limit)
        {
            _limit = limit;

            return this;
        }

        public IBoard Please()
        {
            return new Board(_wipLimit != null ? _wipLimit.Object : new WipLimit(_limit));
        }
    }
}