using System;


namespace Core.CustomException
{
    public class AmountProvidedException : Exception
    {
        public override string Message
        {
            get
            {
                return "The amount provided must be greater or equal to amount total!";
            }
        }
    }
}
