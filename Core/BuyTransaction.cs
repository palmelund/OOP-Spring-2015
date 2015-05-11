using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOP_Spring_2015
{
    public class BuyTransaction : Transaction
    {
        public Product product
        {
            get;
            protected set;
        }

        new public uint Amount
        {
            get;
            protected set;
        }

        // Constructor used for creating a new transaction
        public BuyTransaction(uint id, User user, Product product)
        {
            TransactionID = id;
            this.user = SetUser(user);
            date = SetDate(DateTime.Now);
            this.product = product;
            Amount = product.Price;
        }

        // Constructor used for creating an existing transaction from log file.
        public BuyTransaction(uint id, User user, Product product, uint amount, string date)
        {
            TransactionID = id;
            this.user = SetUser(user);
            this.date = SetDateFromString(date);
            this.product = product;
            Amount = amount;
        }

        public BuyTransaction()
        {

        }

        // Exetutes the transaction if the user has sufficient credits, otherwise an exception is thrown.
        public override void Execute()
        {
            if(user.Balance < Amount && product.CanBeBoughtOnCredit == false)
            {
                InsufficientCreditsException insufficientCreditsException = new InsufficientCreditsException("Insufficient credits to complete transaction");
                insufficientCreditsException.Data["user"] = user.Username;
                insufficientCreditsException.Data["product"] = product.Name;
                throw insufficientCreditsException;
            }
            else
            {
                user.SubtractFromBalance((int)Amount);
            }
        }

        public override string ToString()
        {
            return "BuyTransaction;" + TransactionID + ";" + user.Username + ";" + product.ProductID + ";" + Amount + ";" + date;
        }
    }
}
