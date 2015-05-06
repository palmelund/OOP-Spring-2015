using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace OOP_Spring_2015
{
    public class StringSystem
    {
        public Dictionary<uint, Transaction> transactions = new Dictionary<uint, Transaction>();
        public Dictionary<uint, Product> products = new Dictionary<uint, Product>();
        public Dictionary<uint, User> users = new Dictionary<uint, User>();

        public StringSystem()
        {
            ProductsReader productsReader = new ProductsReader();
            products = productsReader.GetProductDictionary();
        }

        public void BuyProduct(User user, Product product)
        {
            BuyTransaction transaction = new BuyTransaction((uint) transactions.Count, user, DateTime.Now, product);
            ExecuteTransaction(transaction);

        }

        public void AddCreditToAccount(User user, uint amount)
        {
            InsertCashTransaction transaction = new InsertCashTransaction((uint)transactions.Count, user, DateTime.Now, amount);
            ExecuteTransaction(transaction);
        }

        public void ExecuteTransaction(Transaction transaction)
        {
            try
            {
                transaction.Execute();
                transactions.Add(transaction.TransactionID, transaction);
            }
            catch (Exception)
            {
                //Implement
            }
        }

        public Product GetProduct(uint id)
        {
            bool contains = products.ContainsKey(id);

            if (contains == true)
            {
                return products[id];
            }
            else
            {
                throw new ProductDoesNotExistException("Product does not exist");
            }
        }
        public User GetUser(string username)
        {
            foreach (var item in users)
            {
                if(item.Value.Username.Equals(username))
                {
                    return item.Value;
                }
            }
            throw new UserDoesNotExistException();
        }

        public List<Transaction> GetTransactionList(User user, int numberOfTransactions)
        {
            List<Transaction> trans = new List<Transaction>();
            int itemsAdded = 0;

            foreach (var item in transactions)
            {
                if(itemsAdded == numberOfTransactions)
                {
                    break;
                }
                else
                {
                    if(item.Value.user.Equals(user))
                    {
                        trans.Add(item.Value);
                        itemsAdded++;
                    }
                }
            }

            return trans;
        }

        public List<Product> GetActiveProducts()
        {
            List<Product> productList = new List<Product>();

            foreach (var item in products)
            {
                if(item.Value.Active == true)
                {
                    productList.Add(item.Value);
                }
            }

            return productList;
        }
    }
}
