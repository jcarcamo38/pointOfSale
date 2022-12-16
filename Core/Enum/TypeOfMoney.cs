using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace Core.Enum
{
    public enum TypeOfMoney
    {
        [DescriptionAttribute("Bill")]
        BILL = 1,
        [DescriptionAttribute("Coin")]
        COIN = 2
    }
}
