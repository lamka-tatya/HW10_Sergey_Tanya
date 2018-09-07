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
        private List<Mock<Player>> _players = new List<Mock<Player>>();
        private CoinResult? _coinResult = CoinResult.Head;

        public GameBuilder()
        {
        }

        public GameBuilder WithSomePlayer()
        {
            _players.Add(new Mock<Player>());
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

        public GameBuilder With(Mock<Player> player)
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
            _coinResult = CoinResult.Head;

            return this;
        }

        public GameBuilder WithTailsCoin()
        {
            _coinResult = CoinResult.Tails;
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

            var game = new Game(board);

            foreach (var player in _players)
            {
                if (_coinResult.HasValue)
                {
                    player.Setup(p => p.TossCoin()).Returns(_coinResult.Value);
                }

                game.AddPlayer(player.Object);
            }

            return game;
        }
    }
}