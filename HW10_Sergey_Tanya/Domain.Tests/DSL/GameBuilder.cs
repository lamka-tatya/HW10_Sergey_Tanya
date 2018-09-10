using Domain.Interfaces;
using Moq;
using System.Collections.Generic;

namespace Domain.Tests.DSL
{
    internal class GameBuilder
    {
        private Mock<IWipLimit> _wipLimit = null;
        private uint _wipLimitInt = 10;
        private List<Mock<IPlayer>> _players = new List<Mock<IPlayer>>();
        private ICard _card = null;

        public GameBuilder()
        {
        }

        public GameBuilder WithSomePlayer()
        {
            _players.Add(new Mock<IPlayer>());
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

        public GameBuilder With(Mock<IPlayer> player)
        {
            _players.Add(player);
            return this;
        }

        public GameBuilder With(ICard card)
        {
            _card = card;
            
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
            foreach (var player in _players)
            {
                player.Setup(c => c.TossCoin()).Returns(CoinResult.Head);
            }           
            return this;
        }

        public GameBuilder WithTailsCoin()
        {
            foreach (var player in _players)
            {
                player.Setup(c => c.TossCoin()).Returns(CoinResult.Tails);
            }
            return this;
        }

        public Game Please()
        {
            var wipLimit = _wipLimit != null ? _wipLimit.Object : new WipLimit(_wipLimitInt);
            var game = new Game(wipLimit);

            if (_card != null)
            {
                var gameMock = new Mock<Game>(wipLimit);
                gameMock.Setup(x => x.GenerateNewCard()).Returns(_card);

                game = gameMock.Object;
            }

            foreach (var player in _players)
            {
                game.AddPlayer(player.Object);
            }

            return game;
        }
    }
}