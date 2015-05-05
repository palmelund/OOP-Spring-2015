using System;
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

        public uint Amount
        {
            get;
            protected set;
        }

        public BuyTransaction()
        {

        }

        public override string ToString()
        {
            return "Purchase:: ID: " + TransactionID + " user: " + user + " amount: " + Amount + " date: " + date;
        }

        public override void Execute()
        {
            throw new NotImplementedException();
        }
    }
}
