using System.Collections.Generic;

namespace Core.Model.Abstract
{
    public abstract class Amount
    {
        public List<Bill> Bills { get; set; }
        public List<Coin> Coins { get; set; }
        public decimal Total { get; set; }
    }
}
