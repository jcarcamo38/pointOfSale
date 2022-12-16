using Core.Enum;
using Core.Utility.Extension;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.CustomException
{
    public class FormatAmountException : Exception
    {
        private decimal money { get; set; }
        private TypeOfMoney typeOfMoney { get; set; }

        public FormatAmountException(decimal money, TypeOfMoney typeOfMoney)
        {
            this.money = money;
            this.typeOfMoney = typeOfMoney;
        }

        public override string Message
        {
            get
            {
                var description = EnumExtensions.GetDescription<TypeOfMoney>(this.typeOfMoney);
                return $"The quantity: {this.money} is not {description}";
            }
        }
    }
}
