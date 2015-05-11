using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOP_Spring_2015
{
    // Interface for the methods used in StringSystem
    interface IStringSystem
    {
        void BuyProduct(User user, uint productID);
        void AddCreditToAccount(User user, uint amount);
        void ExecuteTransaction(Transaction transaction);
        Product GetProduct(uint id);
        User GetUser(string username);
        List<Transaction> GetTransactionList(uint UserID);
        List<Product> GetActiveProducts();
    }
}
