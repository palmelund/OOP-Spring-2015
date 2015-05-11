using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOP_Spring_2015
{
    public class InsertCashTransaction : Transaction
    {
        public InsertCashTransaction(uint id, User user, uint amount)
        {
            TransactionID = id;
            this.user = SetUser(user);
            this.date = SetDate(DateTime.Now);
            this.Amount = amount;
        }

        public InsertCashTransaction(uint id, User user, uint amount, string date)
        {
            TransactionID = id;
            this.user = user;
            this.date = SetDateFromString(date);
            this.Amount = amount;
        }

        public InsertCashTransaction()
        {

        }

        public override void Execute()
        {
            user.AddToBalance((int)Amount);
         }

        public override string ToString()
        {
            return "InsertCashTransaction;" + TransactionID + ";" + user.Username + ";" + Amount + ";" + date;
        }
    }
}
