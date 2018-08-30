using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HW10_Sergey_Tanya
{
    public class CardStatusIsNotNewException : Exception
    {
        public override string Message => "Статус карточки должен быть новым";
    }
}
