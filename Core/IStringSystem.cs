using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOP_Spring_2015
{
    interface IStringSystem
    {
        void BuyProduct(User user, Product product);
        void AddCreditToAccount(User user, uint amount);
        void ExecuteTransaction(Transaction transaction);
        Product GetProduct(uint id);
        User GetUser(string username);
        List<Transaction> GetTransactionList(User user, int numberOfTransactions);
        List<Product> GetActiveProducts();
    }
}
