using Domain.Interfaces;
using Moq;
using System;

namespace Domain.Tests.DSL
{
    internal class BoardBuilder
    {
        private Mock<IWipLimit> _wipLimit;

        private uint _limit = 1;

        private Action<Board> _action;

        private Action<Board> _action2;

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

                    firstNewCard.TryMoveNextStatus();
                    firstNewCard.TryMoveNextStatus();
                };

            return this;
        }

        public BoardBuilder WithCardInDoneStatus()
        {
            _action2 = (Board board) =>
            {
                var firstNewCard = board.GiveNewCard();

                firstNewCard.TryMoveNextStatus();
                firstNewCard.TryMoveNextStatus();
                firstNewCard.TryMoveNextStatus();
            };

            return this;
        }

        public BoardBuilder And()
        {
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

            if (_action2 != null)
            {
                _action2(board);
            }

            return board;
        }
    }
}