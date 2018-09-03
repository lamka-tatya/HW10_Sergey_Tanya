using Domain.Interfaces;
using System;

namespace Domain
{
    public class Coin : ICoin
    {
        private Random rnd = new Random();
       
        public CoinResult Toss()
        {
            var res = rnd.Next(1, 10);
            return res > 5 ? CoinResult.Head : CoinResult.Tails;
        }
    }
}
