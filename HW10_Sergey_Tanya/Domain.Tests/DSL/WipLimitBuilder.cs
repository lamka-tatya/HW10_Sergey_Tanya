namespace Domain.Tests.DSL
{
    internal class WipLimitBuilder
    {
        private uint _limit = 1;

        public WipLimitBuilder WithLimit(uint limit)
        {
            _limit = limit;

            return this;
        }

        public IWipLimit Please()
        {
            return new WipLimit(_limit);
        }
    }
}