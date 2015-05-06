﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOP_Spring_2015
{
    class BuyTransaction : Transaction
    {
        public Product product
        {
            get;
            protected set;
        }

        new public int Amount
        {
            get;
            protected set;
        }

        public BuyTransaction(uint id, User user, DateTime date,Product product, int amount)
        {
            TransactionID = id;
            this.user = SetUser(user);
            this.date = SetDate(date);
            this.product = product;
            this.Amount = amount;
        }

        public override void Execute()
        {
            if(user.Balance < Amount && product.CanBeBoughtOnCredit == false)
            {
                throw new InsufficientCreditsException("Insufficient funds");
            }
            user.SubtractFromBalance(Amount);
        }

        public override string ToString()
        {
            return "Purchase:: ID: " + TransactionID + " user: " + user + " amount: " + Amount + " date: " + date;
        }
    }
}
