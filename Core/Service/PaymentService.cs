using Core.CustomException;
using Core.Model;
using Core.Wrapper;

namespace Core.Service
{
    /// <summary>
    /// Class PaymentService, the main purpose of this functionallity is validate the amount received by the user
    /// and calculate the change due to relate it to the object AmountDue.
    /// </summary>
    public class PaymentService
    {
        private readonly IConfig _config;          

        public PaymentService(IConfig config)
        {
            this._config = config;
        }       

        /// <summary>
        /// Calculate the amount due of the ticket
        /// </summary>
        /// <param name="ticket">Has all the information to process the amount due</param>
        public void CalculateAmountDue(Ticket ticket)
        {
            var subtractResult = ticket.AmountProvided.Total - ticket.AmountToPay;            
            ticket.AmountDue = new AmountDue(subtractResult, this._config);            
        }

        /// <summary>
        /// Validate the amount before do the calculation of the amount due
        /// </summary>
        /// <param name="ticket">Has the total and the amount to pay to validate</param>
        /// <returns>True if the validation is success</returns>
        public bool IsValidAmount(Ticket ticket)
        {
            if (ticket.AmountProvided.Total < ticket.AmountToPay)
                throw new AmountProvidedException();

            return true;
        }    
    }
}
