using Domain.Interfaces;
using Moq;
using System.Collections.Generic;

namespace Domain.Tests.DSL
{
    internal class PlayerBuilder
    {
        private Mock<IPlayer> _player = null;

        public PlayerBuilder()
        {
        }

        //public PlayerBuilder WithBlockedCards()
        //{
        //    var blockedCard = new Mock<ICard>();
        //    _player = new Mock<Player>();

        //    blockedCard.Setup(c => c.IsBlocked).Returns(true);
        //    blockedCard.Setup(c => c.Status).Returns(Status.InWork);
        //    blockedCard.Setup(c => c.PlayerId).Returns(_player.Object.Id);

        //    _player.Setup(p => p.AllCards).Returns(new List<ICard>() { blockedCard.Object });

        //    return this;
        //}

        public Mock<IPlayer> MockPlease()
        {
            return _player ?? new Mock<IPlayer>();
        }

        public IPlayer Please()
        {
            if(_player != null)
            {
                return _player.Object;
            }

            return new Player();
        }
    }
}