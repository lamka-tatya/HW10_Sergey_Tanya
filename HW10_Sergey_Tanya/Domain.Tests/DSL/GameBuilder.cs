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
        private bool _reachedWipLimit = false;

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
            _reachedWipLimit = true;

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

        public Mock<Game> MockPlease()
        {
            var wipLimit = _wipLimit != null ? _wipLimit.Object : Builder.CreateWipLimit.WithLimit(_wipLimitInt).Please();
            Mock<Game> gameMock = new Mock<Game>(wipLimit);

            if (_card != null)
            {
                gameMock.Setup(x => x.GenerateNewCard()).Returns(_card);
            }

            if (_reachedWipLimit)
            {
                gameMock.Setup(x => x.WipLimitIsReached(It.IsAny<Status>())).Returns(true);
            }

            foreach (var player in _players)
            {
                gameMock.Object.AddPlayer(player.Object);
            }

            return gameMock;
        }

        public Game Please()
        {
            var wipLimit = _wipLimit != null ? _wipLimit.Object : Builder.CreateWipLimit.WithLimit(_wipLimitInt).Please();
            var game = new Game(wipLimit);
            Mock<Game> gameMock = null;

            if (_card != null)
            {
                gameMock = new Mock<Game>(wipLimit);
                gameMock.Setup(x => x.GenerateNewCard()).Returns(_card);
            }

            if (_reachedWipLimit)
            {
                gameMock = gameMock ?? new Mock<Game>(It.IsAny<WipLimit>());
                gameMock.Setup(x => x.WipLimitIsReached(It.IsAny<Status>())).Returns(true);
            }

            if (gameMock != null)
            {
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