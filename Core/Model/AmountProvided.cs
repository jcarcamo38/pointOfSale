using Core.Enum;
using Core.Model.Abstract;
using System.Collections.Generic;

namespace Core.Model
{
    public class AmountProvided : Amount
    {                
        public AmountProvided(List<Bill> bills, List<Coin> coins)
        {
            this.Bills = bills;
            this.Coins = coins;
            SumAmountProvided();
        }        

        private void SumAmountProvided()
        {
            if (this.Bills.Count > 0)
                Bills.ForEach(x => this.Total += x.Amount);

            if (Coins.Count > 0)
                Coins.ForEach(x => this.Total += x.Amount);
        }

    }
}
