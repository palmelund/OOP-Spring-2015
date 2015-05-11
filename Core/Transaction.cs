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

        // Constructor for default transaction.
        public Transaction(uint id, User user, DateTime date, uint amount)
        {
            TransactionID = id;
            this.user = SetUser(user);
            this.date = SetDate(date);
            Amount = amount;
        }

        // Specifies what the transaction does in the derived class.
        public Transaction()
        {

        }

        // Sets the user that made the transaction
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

        // Sets the time for the transaction, and ensures that it isn't null.
        protected DateTime SetDate(DateTime date)
        {
            if (date == null)
            {
                throw new ArgumentNullException("Date not found");
            }
            else
            {
                return date;
            }
        }

        // When reading date from a file, parses the string date to a DateTime date.
        protected DateTime SetDateFromString(string date)
        {
            DateTime dt = Convert.ToDateTime(date);
            return dt;
        }

        // Converts the transaction to a write-friendly string.
        public override string ToString()
        {
            return TransactionID + ";" + Amount + ";" + date;
        }

        // Used by the derived classes to tell what should happen upon executing the transaction.
        public virtual void Execute()
        {
        }
    }
}
