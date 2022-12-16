using System;

namespace Core.CustomException
{
    public class ConvertQuantityException : Exception
    {
        private string quantity { get; set; }
        public ConvertQuantityException(string quantity)
        {
            this.quantity = quantity;
        }

        public override string Message
        {
            get
            {
                return $"Error converting the quantity: {this.quantity} to decimal!";
            }
        }
    }
}
