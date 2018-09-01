using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Domain
{
    public class WipLimit : IWipLimit
    {
        private readonly uint _limit;

        public WipLimit(uint limit)
        {
            _limit = limit;// TODO null exc
        }

        public bool IsReached(uint count)
        {
            return _limit == count;
        }
    }
}