using Core.Enum;
using Core.Model.Abstract;

namespace Core.Model
{
    public class Coin : Generic
    {
        public Coin(decimal money)
        {
            this.ValidateTypeOfMoney(money, TypeOfMoney.COIN);
            this.Amount = money;
        }
    }
}
