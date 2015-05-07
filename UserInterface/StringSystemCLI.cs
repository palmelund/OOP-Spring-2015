using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOP_Spring_2015
{
    class StringSystemCLI : IStringSystemUI
    {
        StringSystem stringsystem;

        public StringSystemCLI(StringSystem stringsystem)
        {
            this.stringsystem = stringsystem;
            User user = new User((uint)stringsystem.users.Count, "frederik", "palmelund", "thepalmelund", "frederik.palmelund@gmail.com");
            stringsystem.users.Add(user.UserID, user);
            Start(); // <- Dont call here
            user.AddToBalance(2000);
            Console.WriteLine(user.Balance);
            stringsystem.BuyProduct(user, stringsystem.products[11]);
            Console.WriteLine(user.Balance);
            stringsystem.BuyProduct(user, stringsystem.products[11]);
            Console.WriteLine(user.Balance);


            foreach (var item in stringsystem.transactions)
            {
                Console.WriteLine(item.Value);
            }

            DisplayUserInfo("thepalmelund");

            //Close();
        }

        public void Start()
        {
            WriteActiveProducts();
        }

        void WriteActiveProducts()
        {
            Console.WriteLine(String.Format("|{0,5}|{1,-35}|{2,8}|", "ID", "Product", "Price"));
            WriteProductSeparationLine();

            foreach (var item in stringsystem.products)
            {
                if (item.Value.Active == true)
                {
                    WriteProductLine(item.Key);
                }
            }

            WriteProductSeparationLine();
        }

        void WriteProductLine(uint id)
        {
            Console.WriteLine(String.Format("|{0,5}|{1,-35}|{2,8:N2}|", stringsystem.products[id].ProductID, stringsystem.products[id].Name, ((double)stringsystem.products[id].Price) / 100));
        }

        void WriteProductSeparationLine()
        {
            Console.WriteLine("|=====|===================================|========|");
        }

        //=== INTERFACE IMPLEMENTATION ===//

        public void DisplayUserNotFound(string username)
        {
            Console.WriteLine("User: [" + username + "] not found.");
        }

        public void DisplayProductNotFound(uint id)
        {
            Console.WriteLine("The product [" + id + "] does not exist, or is not avalable.");
        }

        public void DisplayUserInfo(string username)
        {
            foreach (var item in stringsystem.users)
            {
                if(item.Value.Username.Equals(username))
                {
                    User user = item.Value;
                    Console.WriteLine(user);
                    return;
                }
            }
            DisplayUserNotFound(username);
        }

        public void DisplayTooManyArgumentsError(string arg)
        {
            Console.Write("Could not understand command input: " + arg);

        }

        public void DisplayAdminCommandNotFoundMessage(string arg)
        {
            Console.WriteLine("The AdminCommandLine [{0}] could not be found.", arg);
        }

        public void DisplayUserBuysProduct(BuyTransaction transaction)
        {
            Console.WriteLine("Completed Transaction:\n" + transaction);
        }

        public void DisplayUserBuysProduct(Transaction transaction, uint count)
        {
            for (int i = 0; i < count; i++)
            {
                Console.WriteLine("Completed Transaction:\n" + transaction);
            }
        }

        public void Close()
        {
            System.Environment.Exit(0);
        }

        public void DisplayInsufficientCash(User user)
        {
            Console.WriteLine("User [{0}] does not sufficient funds to complete transaction.", user.Username);
        }

        public void DisplayGeneralError(string errorString)
        {
            Console.WriteLine("An error occoured: " + errorString);
        }
    }
}
