using System.Collections.Generic;

namespace Core.Model
{
    public class Ticket
    {   
        public List<Item> Items { get; set; }        
        public AmountProvided AmountProvided { get; set; }
        public AmountDue AmountDue { get; set; }
        public decimal AmountToPay { get; set; }

        public Ticket(List<Item> items)
        {
            this.Items = items;
            this.getTotal();
        }        

        private void getTotal()
        {
            if (this.Items.Count > 0)
                this.Items.ForEach(x => this.AmountToPay += x.Price);
        }        

    }
}
