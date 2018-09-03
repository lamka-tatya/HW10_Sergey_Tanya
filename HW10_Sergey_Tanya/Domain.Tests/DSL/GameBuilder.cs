using System;
using System.Collections.Generic;
using Domain;
using Domain.Interfaces;
using Moq;

namespace Domain.Tests.DSL
{
    public class GameBuilder
    {
        private Mock<IBoard> _board = null;
        private Mock<IWipLimit> _wipLimit = null; 
        private Mock<ICoin> _coin = new Mock<ICoin>();
        private List<IPlayer> _players = new List<IPlayer>();

        public GameBuilder()
        {
        }

        internal GameBuilder WithSomePlayer()
        {
            _players.Add(new Player());
            return this;
        }

        internal GameBuilder WithOtherSomePlayer()
        {
            return WithSomePlayer();
        }

        internal GameBuilder And()
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

        internal GameBuilder WithReachedWipLimit()
        {
            _wipLimit = new Mock<IWipLimit>();
            _wipLimit.Setup(w => w.IsReached(It.IsAny<uint>())).Returns(true);
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
            var wipLimit = _wipLimit != null ? _wipLimit.Object : new WipLimit(10);
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