using Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    internal class Board : IBoard
    {
        private IList<ICard> _cards;
        private readonly IWipLimit _wipLimit;

        public IWipLimit WipLimit => _wipLimit;

        public Board(IWipLimit wipLimit)
        {
            _cards = new List<ICard>();
            _wipLimit = wipLimit;
        }

        public ICard GiveNewCard()
        {
            var card = new Card(this);
            _cards.Add(card);
            return card;
        }

        public IEnumerable<ICard> CardsThat(Status status)
        {
            return _cards.Where(x => x.Status == status);
        }

        public bool WipLimitIsReached(Status status)
        {
            if (status == Status.Done)
            {
                return false;
            }

            return WipLimit.IsReached((uint)CardsThat(status).Count());
        }
    }
}
