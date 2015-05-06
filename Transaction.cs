using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOP_Spring_2015
{
    public class Transaction
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

        public Transaction(uint id, User user, DateTime date, uint amount)
        {
            TransactionID = id;
            this.user = SetUser(user);
            this.date = date;
            Amount = amount;
        }

        public Transaction()
        {

        }

        protected User SetUser(User user)
        {
            if(user == null)
            {
                throw new ArgumentNullException("User not found");
            }
            else
            {
                return user;
            }
        }

        public override string ToString()
        {
            return "ID: " + TransactionID + " Amount: " + Amount + " Date: " + date;
        }

        public virtual void Execute()
        {

        }
    }
}
