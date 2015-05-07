using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace OOP_Spring_2015
{
    public class StringSystem : IStringSystem
    {
        public Dictionary<uint, Transaction> transactions = new Dictionary<uint, Transaction>();
        public Dictionary<uint, Product> products = new Dictionary<uint, Product>();
        public Dictionary<uint, User> users = new Dictionary<uint, User>();

        public StringSystem()
        {
            ProductsReader productsReader = new ProductsReader();
            products = productsReader.GetProductDictionary();

            User user = new User((uint)users.Count, "Frederik", "Palmelund", "thepalmelund", "frederik.palmelund@gmail.com");
            users.Add(user.UserID, user);
        }

        public void BuyProduct(User user, Product product)
        {
            BuyTransaction transaction = new BuyTransaction((uint) transactions.Count, user, product);
            ExecuteTransaction(transaction);
        }

        public void BuyProduct(User user, uint productID)
        {
            Product product = GetProduct(productID);

            BuyTransaction transaction = new BuyTransaction((uint)transactions.Count, user, product);
            ExecuteTransaction(transaction);
        }

        public void AddCreditToAccount(User user, uint amount)
        {
            InsertCashTransaction transaction = new InsertCashTransaction((uint)transactions.Count, user, amount);
            ExecuteTransaction(transaction);
        }

        public void ExecuteTransaction(Transaction transaction)
        {
            transaction.Execute();
            transactions.Add(transaction.TransactionID, transaction);
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

        public List<Transaction> GetTransactionList(uint userID)
        {
            List<Transaction> trans = new List<Transaction>();
            int itemsAdded = 0;

            foreach (var item in transactions)
            {
                if (item.Value.user.UserID == userID)
                {
                    trans.Add(item.Value);
                    itemsAdded++;
                }
            }

            return trans;
        }

        public List<BuyTransaction> GetBuyTransactionList(uint userID, int numberOfTransactions)
        {
            List<BuyTransaction> trans = new List<BuyTransaction>();
            int itemsAdded = 0;

            List<Transaction> transactionList = GetTransactionList(userID);

            transactionList.Reverse();

            foreach (Transaction item in transactionList)
            {
                if (item is BuyTransaction)
                {
                    trans.Add(item as BuyTransaction);
                    itemsAdded++;
                }
                if (itemsAdded == numberOfTransactions)
                {
                    break;
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
