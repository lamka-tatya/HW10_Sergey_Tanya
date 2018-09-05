namespace Domain
{
    internal class WipLimit : IWipLimit
    {
        private readonly uint _limit;

        public WipLimit(uint limit)
        {
            _limit = limit;
        }

        public bool IsReached(uint count)
        {
            return _limit != 0 && _limit == count;
        }
    }
}