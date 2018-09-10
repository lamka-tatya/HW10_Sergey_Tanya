using Domain.Interfaces;
using System;

namespace Domain
{
    internal class Player : IPlayer
    {
        public Guid Id { get; private set; }

        public Player()
        {
            Id = Guid.NewGuid();
        }

        public CoinResult TossCoin()
        {
            return new Coin().Toss();
        }
    }
}
