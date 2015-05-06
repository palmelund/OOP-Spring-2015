using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOP_Spring_2015
{
    public class StringSystem
    {
        List<Transaction> transactionList = new List<Transaction>();
        List<Product> productList = new List<Product>();
        List<User> userList = new List<User>();

        public StringSystem()
        {

        }

        public void BuyProduct(User user, Product product)
        {
            BuyTransaction transaction = new BuyTransaction((uint) transactionList.Count, user, DateTime.Now, product);
            ExecuteTransaction(transaction);

        }

        public void AddCreditToAccount(User user, uint amount)
        {
            InsertCashTransaction transaction = new InsertCashTransaction((uint)transactionList.Count, user, DateTime.Now, amount);
            ExecuteTransaction(transaction);
        }

        public void ExecuteTransaction(Transaction transaction)
        {
            try
            {
                transaction.Execute();
                transactionList.Add(transaction);
            }
            catch (Exception)
            {
                //Implement
            }
        }

        public Product GetProduct(uint id)
        {
            foreach (var item in productList)
            {
                if(item.ProductID == id)
                {
                    return item;
                }
            }
            throw new ProductDoesNotExistException("Product does not exist");
        }

        public User GetUser(string username)
        {
            foreach (var item in userList)
            {
                if(item.Username.Equals(username))
                {
                    return item;
                }
            }
            throw new UserDoesNotExistException();
        }

        public void GetTransactionList()
        {

        }

        public void GetActiveProducts()
        {

        }


    }
}
