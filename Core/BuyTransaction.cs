﻿using System;
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

        public BuyTransaction(uint id, User user, Product product)
        {
            TransactionID = id;
            this.user = SetUser(user);
            date = SetDate(DateTime.Now);
            this.product = product;
            Amount = product.Price;
        }

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
