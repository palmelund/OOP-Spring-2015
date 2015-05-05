using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOP_Spring_2015
{
    class Transaction
    {
        public uint TransactionID
        {
            get;
            protected set;
        }

        public User user
        {
            get;
            protected set;
        }

        public DateTime date
        {
            get;
            protected set;
        }

        public uint Amount
        {
            get;
            protected set;
        }

        public Transaction()
        {

        }

        public override string ToString()
        {
            return "ID: " + TransactionID + " Amount: " + Amount + " Date: " + date;
        }

        public abstract void Execute()
        {

        }
    }
}
