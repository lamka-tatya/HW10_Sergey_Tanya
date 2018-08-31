using System;
using Domain;
using Domain.Interfaces;
using Moq;

namespace Domain.Tests.DSL
{
    public class GameBuilder
    {
        private Board _board;
        private uint _playersCount;
        private Mock<ICoin> coin;

        public GameBuilder()
        {
            coin = new Mock<ICoin>();
        }

        public GameBuilder With(Board board)
        {
            _board = board;
            return this;
        }

        public GameBuilder WithPlayers(uint count)
        {
            _playersCount = count;
            return this;
        }

        public Game Please()
        {
            return new Game(
                _playersCount == 0 ? 1 : _playersCount,
                _board ?? new Board(),
                coin.Object);
        }

        internal GameBuilder WithHeadCoin()
        {
            coin.Setup(c => c.Toss()).Returns(CoinResult.Head);

            return this;
        }
    }
}