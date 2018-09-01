using System;
using Domain.Interfaces;
using Moq;

namespace Domain.Tests.DSL
{
    public class BoardBuilder
    {
        private Mock<IWipLimit> _wipLimit;

        private uint _limit = 1;

        private Action<Board> _action;

        public BoardBuilder WithReachedWipLimit()
        {
            _wipLimit = new Mock<IWipLimit>();
            _wipLimit.Setup(w => w.IsReached(It.IsAny<uint>())).Returns(true);

            return this;
        }

        public BoardBuilder WithCardInTestStatus()
        {
            _action = (Board board) =>
                {
                    var firstNewCard = board.GiveNewCard();

                    firstNewCard.MoveNextStatus();
                    firstNewCard.MoveNextStatus();
                };

            return this;
        }

        public BoardBuilder WithWipLimit(uint limit)
        {
            _limit = limit;

            return this;
        }

        public IBoard Please()
        {
            var board = new Board(_wipLimit != null ? _wipLimit.Object : new WipLimit(_limit));

            if(_action != null)
            {
                _action(board);
            }

            return board;
        }
    }
}