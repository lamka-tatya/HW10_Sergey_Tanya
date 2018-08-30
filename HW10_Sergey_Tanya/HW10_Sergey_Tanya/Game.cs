using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HW10_Sergey_Tanya
{
    public class Game
    {
        private List<Player> _players;

        public Game(int playersCount)
        {
            for (int i = 0; i < playersCount; i++)
            {
                _players.Add(new Player());
            }
        }

        public int CardsCount(Status status)
        {
            throw new NotImplementedException();
        }
    }
}
