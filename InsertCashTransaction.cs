using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOP_Spring_2015
{
    public class InsertCashTransaction : Transaction
    {
        public InsertCashTransaction()
        {

        }

        public override void Execute()
        {
            base.Execute();
        }

        public override string ToString()
        {
            return "Insert Transaction:: ID: " + TransactionID + " Amount: " + Amount + " user: " + user + " Date: " + date;
        }
    }
}
