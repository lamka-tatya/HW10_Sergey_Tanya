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
        private Mock<ICoin> _coin;
        private Mock<Card> _card;

        public GameBuilder()
        {
            _coin = new Mock<ICoin>();
        }

        public GameBuilder With(Board board)
        {
            _board = board;
            return this;
        }

        public GameBuilder With(Mock<Card> card)
        {
            _card = card;
            return this;
        }

        public GameBuilder WithPlayers(uint count)
        {
            _playersCount = count;
            return this;
        }

        internal GameBuilder WithHeadCoin()
        {
            _coin.Setup(c => c.Toss()).Returns(CoinResult.Head);

            return this;
        }

        internal GameBuilder WithTailsCoin()
        {
            _coin.Setup(c => c.Toss()).Returns(CoinResult.Tails);
            return this;
        }

        public Game Please()
        {
            return new Game(
                _playersCount == 0 ? 1 : _playersCount,
                _board ?? new Board(),
                _coin.Object);
        }

    }
}