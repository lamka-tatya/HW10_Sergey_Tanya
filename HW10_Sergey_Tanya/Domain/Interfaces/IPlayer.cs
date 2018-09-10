using System;
using System.Collections.Generic;

namespace Domain.Interfaces
{
    internal interface IPlayer
    {
        Guid Id { get; }

        CoinResult TossCoin();
    }
}