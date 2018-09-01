using Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    public class Board : IBoard
    {
        private IList<ICard> _cards = new List<ICard>();

        public ICard GiveNewCard()
        {
            var card = new Card();
            _cards.Add(card);
            return card;
        }

        public IEnumerable<ICard> CardsThat(Status status)
        {
            return _cards.Where(x => x.Status == status);
        }
    }
}
