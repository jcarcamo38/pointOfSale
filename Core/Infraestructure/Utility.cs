using Core.CustomException;
using Core.Enum;
using Core.Model;
using Core.Utility.Extension;
using Core.Wrapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Core.Utility
{
    public static class Utility
    {        
        /// <summary>
        /// Transform the response in yes or no through the params of type string
        /// </summary>
        /// <param name="response">yes or no of type string</param>
        /// <returns>True if is yes or false</returns>
        public static bool getResponse(string response)
        {
            return String.Equals(response, "y", StringComparison.CurrentCultureIgnoreCase) ? true : false;
        }   
        
        public static List<decimal> splitMoney(string response)
        {            
            var result = new List<decimal>();
            var quantities = response.Split(",");
            decimal q = 0;

            foreach(var quantity in quantities)
            {
                var isSuccess = Decimal.TryParse(quantity, out q);
                if (!isSuccess)
                    throw new ConvertQuantityException(quantity);

                result.Add(Convert.ToDecimal(quantity));
            }            

            return result;
        }

        /// <summary>
        /// Create the structure string of the receipt of the ticket
        /// </summary>
        /// <param name="ticket">Has all the information of the ticket</param>
        /// <returns>String with the structure string</returns>
        public static string PrintReceipt(Ticket ticket)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("\n");
            sb.AppendLine("-----------RECEIPT----------");
            sb.AppendLine("----------------------------");
            sb.AppendLine($"Number of items: {ticket.Items.Count}");
            sb.AppendLine($"Total amount: { ticket.AmountToPay}");
            sb.AppendLine("----------------------------");
            sb.AppendLine($"Total amount provided: { ticket.AmountProvided.Total }");
            sb.AppendLine($"Total amount due: { ticket.AmountDue.Total }");
            return sb.ToString();
        }

        /// <summary>
        /// Get the language of the app.config using the configuration wrapper
        /// </summary>
        /// <param name="config">Configuration of type IConfig</param>
        /// <returns>Language of type enum</returns>
        private static Language GetLanguage(IConfig config)
        {            
            return config.language == EnumExtensions.GetAttributeDescription<Language>(Language.US) ? 
                    Language.US : Language.MX;                                    
        }

        /// <summary>
        /// Get the denominations by language
        /// </summary>
        /// <param name="config">Configuration of type IConfig</param>
        /// <returns>List of denominations</returns>
        public static List<decimal> GetDenominations(IConfig config)
        {
            var denominations = new List<decimal>();
            var denominationsResult = new string[]{ };
            var language = GetLanguage(config);

            if (language == Language.US)
                denominationsResult = config.denominationsUS;
            else
                denominationsResult = config.denominationsMX;

            denominationsResult.ToList().ForEach(x => denominations.Add(Convert.ToDecimal(x)));
            return denominations;
        }

    }
}
