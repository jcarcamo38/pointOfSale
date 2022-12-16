using Core.Model.Abstract;
using Core.Wrapper;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Core.Model
{
    public class AmountDue : Amount
    {
        private List<decimal> denominations = new List<decimal>();
        private IConfig _config;

        public AmountDue(decimal amount, IConfig config)
        {
            this._config = config;
            this.Bills = new List<Bill>();
            this.Coins = new List<Coin>();
            this.Total = amount;

            if(this.Total != 0)
            {
                GetDenominations();
                CalculateAmountDue();
            }            
        }     

        /// <summary>
        /// Main functionality to calculate the amount due
        /// </summary>
        private void CalculateAmountDue()
        {
            var totalAux = this.Total;
            foreach (var denomination in denominations.AsEnumerable().Reverse())
            {
                if (totalAux == 0)
                    break;

                var result = totalAux / denomination;
                var roundResult = Math.Truncate(result);                

                if (roundResult > 0)
                {
                    if (denomination >= 1)//Bill
                    {
                        for(var x = 1; x <= roundResult; x++)
                        {
                            this.Bills.Add(new Bill(denomination));
                            totalAux -= denomination;
                        }                        
                    }
                    else //Coin
                    {
                        for (var x = 1; x <= roundResult; x++)
                        {
                            this.Coins.Add(new Coin(denomination));
                            totalAux -= denomination;
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Get the denominations previously to calculate the amount
        /// </summary>
        public void GetDenominations()
        {            
            denominations = Utility.Utility.GetDenominations(this._config);               
        }        
    }
}
