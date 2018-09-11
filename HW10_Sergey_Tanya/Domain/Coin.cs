using Domain.Interfaces;
using System;

namespace Domain
{
    internal class Coin : ICoin
    {
        private static Random rnd = new Random();

        public CoinResult Toss()
        {
            var res = rnd.Next(1, 10);
            return res > 5 ? CoinResult.Head : CoinResult.Tails;
        }
    }
}
