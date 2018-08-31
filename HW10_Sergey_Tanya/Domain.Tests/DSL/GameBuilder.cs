using System;
using Domain;

namespace Domain.Tests.DSL
{
    public class GameBuilder
    {
        private Board _board;
        private uint _playersCount;

        public GameBuilder()
        {

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
            return new Game(_playersCount == 0 ? 1 : _playersCount, _board ?? new Board());
        }

        internal GameBuilder WithHeadCoin()
        {
            return this;
        }
    }
}