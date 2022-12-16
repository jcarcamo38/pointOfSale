using Core.Enum;
using Core.Model.Abstract;

namespace Core.Model
{
    public class Bill : Generic
    {
        public Bill(decimal money)
        {            
            this.ValidateTypeOfMoney(money, TypeOfMoney.BILL);
            this.Amount = money;
        }
    }
}
