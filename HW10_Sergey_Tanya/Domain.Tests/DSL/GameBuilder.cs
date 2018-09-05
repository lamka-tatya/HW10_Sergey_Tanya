using Domain.Interfaces;
using Moq;
using System.Collections.Generic;

namespace Domain.Tests.DSL
{
    internal class GameBuilder
    {
        private Mock<IBoard> _board = null;
        private Mock<IWipLimit> _wipLimit = null;
        private uint _wipLimitInt = 10;
        private Mock<ICoin> _coin = new Mock<ICoin>();
        private List<IPlayer> _players = new List<IPlayer>();

        public GameBuilder()
        {
        }

        public GameBuilder WithSomePlayer()
        {
            _players.Add(new Player());
            return this;
        }

        public GameBuilder WithOtherSomePlayer()
        {
            return WithSomePlayer();
        }

        public GameBuilder And()
        {
            return this;
        }

        public GameBuilder With(IPlayer player)
        {
            _players.Add(player);
            return this;
        }

        public GameBuilder With(ICard card)
        {
            _board = new Mock<IBoard>();
            _board.Setup(b => b.GiveNewCard()).Returns(card);

            return this;
        }

        public GameBuilder WithWipLimit(uint wipLimit)
        {
            _wipLimitInt = wipLimit;
            return this;
        }

        public GameBuilder WithReachedWipLimit()
        {
            _wipLimit = new Mock<IWipLimit>();
            _wipLimit.Setup(w => w.IsReached(It.IsAny<uint>())).Returns(true);
            return this;
        }

        public GameBuilder WithHeadCoin()
        {
            _coin.Setup(c => c.Toss()).Returns(CoinResult.Head);

            return this;
        }

        public GameBuilder WithTailsCoin()
        {
            _coin.Setup(c => c.Toss()).Returns(CoinResult.Tails);
            return this;
        }

        public Game Please()
        {
            var wipLimit = _wipLimit != null ? _wipLimit.Object : new WipLimit(_wipLimitInt);

            IBoard board = null;
            if (_board != null)
            {
                _board.Setup(x => x.WipLimit).Returns(wipLimit);
                board = _board.Object;
            }
            else
            {
                board = new Board(wipLimit);
            }

            var game = new Game(board, _coin.Object);

            foreach (var player in _players)
            {
                game.AddPlayer(player);
            }

            return game;
        }
    }
}