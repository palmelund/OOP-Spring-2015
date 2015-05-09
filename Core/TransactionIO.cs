using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.IO;

namespace OOP_Spring_2015
{
    public class TransactionIO
    {
        StringSystem stringsystem;
        public TransactionIO(StringSystem stringsystem, ref Dictionary<uint, Transaction> transactions)
        {
            this.stringsystem = stringsystem;

            string[] transactionList = File.ReadAllLines("..\\..\\Ressources\\transactions.log");
            foreach (var item in transactionList)
            {
                string[] transaction = item.Split(';');
                if(transaction[0].Equals("BuyTransaction"))
                {
                    User user = stringsystem.GetUser(transaction[2]);
                    Product product = stringsystem.GetProduct(uint.Parse(transaction[3]));
                    BuyTransaction buyTransaction = new BuyTransaction(uint.Parse(transaction[1]),user, product, uint.Parse(transaction[4]), transaction[5]);
                    transactions.Add(buyTransaction.TransactionID, buyTransaction);
                }
                else if (transaction[0].Equals("InsertCashTransaction"))
                {
                    User user = stringsystem.GetUser(transaction[2]);
                    InsertCashTransaction insertCashTransaction = new InsertCashTransaction(uint.Parse(transaction[1]), user, uint.Parse(transaction[3]), transaction[4]);
                    transactions.Add(insertCashTransaction.TransactionID, insertCashTransaction);
                }
            }
        }

        public void WriteTransactionToLog(Transaction transaction)
        {
            try
            {
                if (transaction is BuyTransaction)
                {
                    BuyTransaction buyTransaction = transaction as BuyTransaction;
                    string s = "BuyTransaction;" + buyTransaction.TransactionID + ";" + buyTransaction.user.Username + ";" + buyTransaction.product.ProductID + ";" + buyTransaction.Amount + ";" + buyTransaction.date;
                    File.AppendAllText("..\\..\\Ressources\\transactions.log", s + Environment.NewLine);
                }
                else if (transaction is InsertCashTransaction)
                {
                    InsertCashTransaction insertcashtransaction = transaction as InsertCashTransaction;
                    string s = "InsertCashTransaction;" + insertcashtransaction.TransactionID + ";" + insertcashtransaction.user.Username + ";" + insertcashtransaction.Amount + ";" + insertcashtransaction.date;
                    File.AppendAllText("..\\..\\Ressources\\transactions.log", s + Environment.NewLine);
                }

                string[] userfile = File.ReadAllLines("..\\..\\Ressources\\user.csv");
                for (int i = 1; i < userfile.Length; i++)
                {
                    userfile[i] = stringsystem.userIO.FormatUserForSave(stringsystem.users[(uint)i-1]); // -1 as the user is shifted by one compared to line number
                }

                File.WriteAllText("..\\..\\Ressources\\user.csv", string.Empty);
                File.WriteAllLines("..\\..\\Ressources\\user.csv", userfile);
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex);
            }
        }
    }
}
