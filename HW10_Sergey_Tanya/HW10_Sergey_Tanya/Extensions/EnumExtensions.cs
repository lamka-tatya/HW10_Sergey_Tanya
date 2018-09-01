using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Extensions
{
    public static class EnumExtensions
    {
        public static Status Next(this Status currentStatus)
        {
            return currentStatus + 1;
        }
    }
}
