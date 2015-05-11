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

        // Loads all transactions into transactions dictionary at startup.
        public TransactionIO(StringSystem stringsystem, ref Dictionary<uint, Transaction> transactions)
        {
            try
            {
                this.stringsystem = stringsystem;

                string[] transactionList = File.ReadAllLines("..\\..\\Ressources\\transactions.log");
                foreach (var item in transactionList)
                {
                    string[] transaction = item.Split(';');
                    // Creates the transaction that matches it's type.
                    if (transaction[0].Equals("BuyTransaction"))
                    {
                        User user = stringsystem.GetUser(transaction[2]);
                        Product product = stringsystem.GetProduct(uint.Parse(transaction[3]));
                        BuyTransaction buyTransaction = new BuyTransaction(uint.Parse(transaction[1]), user, product, uint.Parse(transaction[4]), transaction[5]);
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
            catch (Exception)
            {
                throw new FileNotFoundException("Transaction log not found. New log file will be created on first transaction.");
            }
        }

        // Writes a transaction to the log when it is being executed.
        public void WriteTransactionToLog(Transaction transaction)
        {
            string s = transaction.ToString();
            File.AppendAllText("..\\..\\Ressources\\transactions.log", s + Environment.NewLine);

            // As the user's balance has also changed after a transaction, the user file is updated accordingly.
            // This is done here instead of in UserIO to avoid creating a new instance or passing UserIO as an argument.
            
            string[] userfile = File.ReadAllLines("..\\..\\Ressources\\user.csv");
            for (int i = 1; i < userfile.Length; i++)
            {
                userfile[i] = stringsystem.userIO.FormatUserForSave(stringsystem.users[(uint)i - 1]); // -1 as the user is shifted by one compared to line number due to header line.
            }

            File.WriteAllText("..\\..\\Ressources\\user.csv", string.Empty);
            File.WriteAllLines("..\\..\\Ressources\\user.csv", userfile);
        }
    }
}
