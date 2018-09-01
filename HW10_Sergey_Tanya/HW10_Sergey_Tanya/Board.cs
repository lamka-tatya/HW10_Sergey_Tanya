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
        private IList<ICard> _cards;
        private IWipLimit _wipLimit;

        public Board(IWipLimit wipLimit)
        {
            _cards = new List<ICard>();
            _wipLimit = wipLimit;
        }

        public ICard GiveNewCard()
        {
            if (!_wipLimit.IsReached((uint)CardsThat(Status.InWork).Count()))
            {
                var card = new Card();
                _cards.Add(card);
                return card;
            }
            return null;
        }

        public IEnumerable<ICard> CardsThat(Status status)
        {
            return _cards.Where(x => x.Status == status);
        }
    }
}
