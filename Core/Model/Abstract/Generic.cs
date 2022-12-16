using Core.CustomException;
using Core.Enum;
using System.Text.RegularExpressions;

namespace Core.Model.Abstract
{
    public abstract class Generic
    {
        public decimal Amount { get; set; }
        private readonly string PATTERN_BILL = @"^\d{0,3}(\.[0]{0,2})?$";
        private readonly string PATTERN_COIN = @"^[01]\.\d{2}$";

        public void ValidateTypeOfMoney(decimal  money, TypeOfMoney typeOfMoney)
        {
            if(money > 0)
            {
                switch (typeOfMoney)
                {
                    case TypeOfMoney.BILL:
                        {
                            var regex = new Regex(PATTERN_BILL);
                            if (!regex.IsMatch(money.ToString()))
                                throw new FormatAmountException(money, TypeOfMoney.BILL);

                            break;
                        }
                    case TypeOfMoney.COIN:
                        {
                            var regex = new Regex(PATTERN_COIN);
                            if (!regex.IsMatch(money.ToString()))
                                throw new FormatAmountException(money, TypeOfMoney.COIN);

                            break;
                        }
                    default: break;
                }
            }            
        }
    }
}
