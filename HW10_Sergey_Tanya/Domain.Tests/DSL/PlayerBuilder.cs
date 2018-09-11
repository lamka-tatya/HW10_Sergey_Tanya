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