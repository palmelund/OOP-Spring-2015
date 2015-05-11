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
        public TransactionIO transactionIO;
        public UserIO userIO;

        /*
         * REMEMBER:
         * Load order:
         * USER
         * PRODUCT
         * TRANSACTION
         */

        public StringSystem()
        {
            try
            {
                userIO = new UserIO(ref users);

                ProductsReader productsReader = new ProductsReader();
                products = productsReader.GetProductDictionary();

                transactionIO = new TransactionIO(this, ref transactions);
            }
            catch (Exception ex) // <- If this catches anything, the program shouldn't be run as something went wrong with loading the files, and may not work properly.
            {
                StringSystemCLI cli = new StringSystemCLI(this);
                cli.DisplayCriticalError(ex);
            }
        }

        public void BuyProduct(User user, Product product)
        {
            BuyTransaction transaction = new BuyTransaction((uint) transactions.Count, user, product);
            ExecuteTransaction(transaction);
        }

        public void BuyProduct(User user, uint productID)
        {
            Product product = GetProduct(productID);

            if(product.Active == false)
            {
                ProductNotActiveException productNotActiveException = new ProductNotActiveException("The product is currently not active");
                productNotActiveException.Data["product"] = product.Name;
                productNotActiveException.Data["id"] = product.ProductID;
                throw productNotActiveException;
            }

            BuyTransaction transaction = new BuyTransaction((uint)transactions.Count, user, product);
            ExecuteTransaction(transaction);
        }

        public void AddUser(string username, string email, string lastname, string[] firstname)
        {
            string fn = string.Empty;

            for(int i = 4; i < firstname.Length; i++)
            {
                fn += firstname[i] + " ";
            }

            User user = new User((uint)users.Count, fn, lastname, username, email);
            users.Add(user.UserID, user);

            userIO.AddUserToFile(user);
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

            if(transaction is BuyTransaction)
            {
                transactionIO.WriteTransactionToLog(transaction as BuyTransaction);
            }
            else if (transaction is InsertCashTransaction)
            {
                transactionIO.WriteTransactionToLog(transaction as InsertCashTransaction);
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
                ProductDoesNotExistException productDoesNotExistException = new ProductDoesNotExistException("Product does not exist");
                productDoesNotExistException.Data["product"] = id;
                throw productDoesNotExistException;
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
            UserDoesNotExistException userDoesNotExistException = new UserDoesNotExistException("The user does not exist");
            userDoesNotExistException.Data["user"] = username;
            throw userDoesNotExistException;
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
