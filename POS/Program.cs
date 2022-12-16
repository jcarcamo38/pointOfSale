using Core.CustomException;
using Core.Model;
using Core.Service;
using Core.Utility;
using Core.Wrapper;
using System;
using System.Collections.Generic;

namespace POS
{
    class Program
    {
        static void Main(string[] args)
        {            
            Console.WriteLine("**** POINT OF SALE **** \n");                        
            try 
            {
                var ticket = AddItems();                                 
                AddPayment(ticket);
                PrintReceipt(ticket);

                Console.ReadLine();
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }            
            finally
            {
                Console.ReadLine();
            }
        }     
        
        /// <summary>
        /// Add new items and create a new ticket.
        /// </summary>
        /// <returns>New Ticket</returns>
        static Ticket AddItems()
        {
            var isEnterPrice = true;
            var items = new List<Item>();

            while (isEnterPrice)
            {
                Console.WriteLine($"Enter price of item({items.Count + 1}): ");
                var price = Convert.ToDecimal(Console.ReadLine());
                items.Add(new Item(price));

                Console.WriteLine("Add new item [y/n]: ");
                isEnterPrice = Utility.getResponse(Console.ReadLine());
                Console.WriteLine("\n");
            }

            return new Ticket(items);
        }

        /// <summary>
        /// Add new payment related ticket
        /// </summary>
        /// <param name="ticket">Ticket to relate the new payment</param>
        static void AddPayment(Ticket ticket)
        {                        
            var bills = new List<Bill>();
            var coins = new List<Coin>();

            try
            {                    
                bills = AddBills();                                        
                coins = AddCoins();
                GenerateAmountProvided(ticket, bills, coins);
                GetAmountChange(ticket);                  
            }            
            catch (AmountProvidedException ape)
            {
                Console.WriteLine(ape.Message);
                Console.Clear();
                AddPayment(ticket);
            }
        }

        /// <summary>
        /// Create the bills that we received from the user
        /// </summary>
        /// <returns>List of bills</returns>
        static List<Bill> AddBills()
        {
            var bills = new List<Bill>();
            try
            {
                Console.WriteLine("\n");
                Console.WriteLine("1: Bill(s) | Please separate using comma, example (1.00, 2.00, 5.00 ...). Use 0 to set N/A: ");
                var responseBills = Utility.splitMoney(Console.ReadLine());

                if(responseBills.Count > 0)                
                    responseBills.ForEach(b => bills.Add(new Bill(b)));                
            }
            catch (ConvertQuantityException cqe)
            {
                ResetBills(cqe.Message);
            }
            catch(FormatAmountException fae)
            {
                ResetBills(fae.Message);
            }
            return bills;
        }        

        /// <summary>
        /// Create the coins that we received from the user
        /// </summary>
        /// <returns>List of coins</returns>
        static List<Coin> AddCoins()
        {
            var coins = new List<Coin>();

            try
            {
                Console.WriteLine("\n");
                Console.WriteLine("2: Coin(s) | Please separate using comma, example (.01, .05, .10 ...). Use 0 to set N/A: ");
                var responseCoins = Utility.splitMoney(Console.ReadLine());

                if (responseCoins.Count > 0)
                    responseCoins.ForEach(c => coins.Add(new Coin(c)));
            }
            catch (ConvertQuantityException cqe)
            {

                ResetCoins(cqe.Message);
            }
            catch(FormatAmountException fae)
            {
                ResetCoins(fae.Message);
            }

            return coins;
        }

        /// <summary>
        /// Generate object AmountProvides of the amount provided by the user
        /// </summary>
        /// <param name="ticket">Ticket to relate the data with the bills and coins</param>
        /// <param name="bills">Bills to relate to the AmountProvided object</param>
        /// <param name="coins">Coins to relate to the AmountProvided object</param>
        static void GenerateAmountProvided(Ticket ticket, List<Bill> bills, List<Coin> coins)
        {
            ticket.AmountProvided = new AmountProvided(bills, coins);
        }

        /// <summary>
        /// The main purpose of this method is obtain the change due of the user
        /// </summary>
        /// <param name="ticket">Ticket where we're going to take the data to process the change due</param>        
        static void GetAmountChange(Ticket ticket)
        {
            var paymentService = new PaymentService(new Config());
            try
            {
                if (paymentService.IsValidAmount(ticket))
                {
                    paymentService.CalculateAmountDue(ticket);
                }                
            }
            catch(AmountProvidedException ape)
            {
                ResetPayment(ticket, ape.Message);
            }
        }        

        /// <summary>
        /// Reset payment's UI
        /// </summary>
        /// <param name="ticket">Ticket to continue working with the actual</param>
        /// <param name="message">Message of the previous error</param>
        static void ResetPayment(Ticket ticket, string message)
        {
            Console.WriteLine(message);
            Console.ReadLine();
            Console.Clear();
            AddPayment(ticket);
        }

        /// <summary>
        /// Reset Bill's UI
        /// </summary>
        /// <param name="message">Message of the previous error</param>
        static void ResetBills(string message)
        {
            Console.WriteLine(message);
            Console.ReadLine();
            Console.Clear();
            AddBills();
        }

        /// <summary>
        /// Reset Coin's UI
        /// </summary>
        /// <param name="message">Message of the previous error</param>
        static void ResetCoins(string message)
        {
            Console.WriteLine(message);
            Console.ReadLine();
            Console.Clear();
            AddCoins();
        }

        /// <summary>
        /// Print the last step of the process with the change due and other information
        /// </summary>
        /// <param name="ticket">Has all the information</param>
        static void PrintReceipt(Ticket ticket)
        {
            var printResult = Utility.PrintReceipt(ticket);            
            Console.WriteLine(printResult);
        }
    }
}
