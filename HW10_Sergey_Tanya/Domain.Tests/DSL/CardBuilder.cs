using Domain.Interfaces;

namespace Domain.Tests.DSL
{
    internal class CardBuilder
    {
        private ICard _card = new Card();

        public CardBuilder In(Status status)
        {
            while (status != _card.Status)
            {
                _card.NextStatus();
            }
            
            return this;
        }

        public ICard Please()
        {
            return _card;
        }
    }
}