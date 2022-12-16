namespace Core.Model
{
    public class Item
    {
        public decimal Price { get; set; }

        public Item(decimal price)
        {
            this.Price = price;
        }
        
    }
}
