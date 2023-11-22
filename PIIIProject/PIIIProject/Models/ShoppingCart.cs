using System.Collections.Generic;
using System;
using System.Text;
using System.Linq;
using System.Globalization;

namespace PIIIProject.Models
{
    public class ShoppingCart
    {
        private const double EMPTY = 0.00;
        private const double MIN_TOTAL = 1.00;
        private const double MIN_MONEY = 0.01;
        private const double MIN_CARD_TOTAL = 5.00;
        private const int MIN_QUANTITY = 0;

        private const double QUARTER = 0.25;
        private const double DIME = 0.1;
        private const double NICKEL = 0.05;
        private const double PENNY = 0.01;

       

        private double _totalCost;
        private double _amountReceived;
        private double _moneyBack;
        private string _paymentType;
        private List<Item> _items;

        private double[] bills_ = { 5.00, 10.00, 20.00, 50.00, 100.00 };


        public ShoppingCart(List<Item> items_, string paymentType_)
        {
            _items = items_;
            PayType = paymentType_;
        }

        private double TotalCost
        {
            get { return _totalCost; }

            set
            {
                if (value < MIN_TOTAL)
                    throw new System.ArgumentException("Error, total cost cannot be less than 1.00$");

                _totalCost = value;
            }
        }

        private double AmountReceived
        {
            get { return _amountReceived; }

            set
            {
                if (bills_.Contains(value) == false)
                    throw new System.ArgumentException("Error, please enter a valid bill");

                _amountReceived = value;
            }
        }

        private double MoneyBack
        {
            get { return _moneyBack; }

            set
            {
                /*if (value < MIN_MONEY)
                    throw new System.ArgumentException("Error, cannot return a negative value"); */

                _moneyBack = value;
            }
        }

        public string PayType
        {
            get { return _paymentType; }

            set
            {

                if (string.IsNullOrEmpty(value))
                    throw new System.ArgumentException("Error, please select a valid Payment Type");

                _paymentType = value;
            }
        }

        public string PayWithDebitCredit(string card)
        {
            if (card != "Debit/Credit")
                throw new System.ArgumentException("Error, this form of payment only accepts debit/credit");

            foreach (Item item in _items)
            {
                TotalCost += (item.Price * item.Quantity);
            }

            if (TotalCost < MIN_CARD_TOTAL)
                throw new System.ArgumentException("Error, Total Cost must be atleast 5$");

            return "Payment Succesful";

        }


        public string PayWithCash(string cash, double amount_ )
        {
            if (cash != "Cash")
                throw new System.ArgumentException("Error, this form of payment only accepts cash");

            foreach (Item item in _items)
            {
                TotalCost += (item.Price * item.Quantity);
            }

            AmountReceived = amount_;

            if (AmountReceived < TotalCost)
                throw new System.ArgumentException("Error, amount received is less than your puchase total");

            return "Payment Succesful";

        }

        public string PrintReceipt()
        {
            if (PayType == "Debit/Credit")
            {
                StringBuilder cardReceipt = new StringBuilder();

                foreach(Item item in _items)
                {
                    if (item.Quantity > MIN_QUANTITY)
                    cardReceipt.AppendLine(item.ToString());
                }

                cardReceipt.AppendLine(" ");
                cardReceipt.AppendLine("Payment Type: Debit/Credit");
                cardReceipt.AppendLine($"Total Cost: {TotalCost.ToString("C", CultureInfo.CurrentCulture)}");
                cardReceipt.AppendLine("Thank you for using SnackBoost Vending Machine!");

                return cardReceipt.ToString();

            }

            else
            {
                StringBuilder cashReceipt = new StringBuilder();

                foreach (Item item in _items)
                {
                    if(item.Quantity > MIN_QUANTITY)
                    cashReceipt.AppendLine(item.ToString());
                }

                #region CaculateMoneyBack

                double change = (AmountReceived - TotalCost);
                MoneyBack = (AmountReceived - TotalCost);

                // Calculate the number of dollars to be returned
                int dollars = (int)MoneyBack;
                MoneyBack -= dollars;

                // Calculate the number of quarters to be returned
                int quarters = (int)(MoneyBack / QUARTER);
                MoneyBack -= quarters * QUARTER;

                // Calculate the number of dimes to be returned
                int dimes = (int)(MoneyBack / DIME);
                MoneyBack -= dimes * DIME;

                // Calculate the number of nickels to be returned
                int nickels = (int)(MoneyBack / NICKEL);
                MoneyBack -= nickels * NICKEL;

                // Calculate the number of pennies to be returned
                int pennies = (int)(MoneyBack / PENNY);

                #endregion

                cashReceipt.AppendLine(" ");
                cashReceipt.AppendLine("Payment Type: Cash");
                cashReceipt.AppendLine($"Total Cost: {TotalCost.ToString("C", CultureInfo.CurrentCulture)}");
                cashReceipt.AppendLine($"Amount Received: {AmountReceived.ToString("C", CultureInfo.CurrentCulture)}");
                cashReceipt.AppendLine($"Change {change.ToString("C", CultureInfo.CurrentCulture)}");
                cashReceipt.AppendLine("Breakdown:");
                cashReceipt.AppendLine(" ");
                cashReceipt.AppendLine($"Dollars: {dollars}");
                cashReceipt.AppendLine($"Quarters: {quarters}");
                cashReceipt.AppendLine($"Dimes {dimes}");
                cashReceipt.AppendLine($"Nickels: {nickels}");
                cashReceipt.AppendLine($"Pennies: {pennies}");
                cashReceipt.AppendLine("Thank you for using SnackBoost Vending Machine!");

                return cashReceipt.ToString();
            }


        }
    }
}
