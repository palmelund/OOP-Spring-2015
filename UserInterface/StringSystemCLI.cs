using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOP_Spring_2015
{
    public class StringSystemCLI : IStringSystemUI
    {
        StringSystem stringsystem;

        public StringSystemCLI(StringSystem stringsystem)
        {
            this.stringsystem = stringsystem;
        }

        public void Start(StringSystemCommandParser parser)
        {
            string input;

            while(true)
            {
                WriteActiveProducts();
                Console.Write(">> ");
                input = Console.ReadLine();
                parser.ParseCommand(input);
                Console.WriteLine("Press any key to clear screen and promt for new input:");
                Console.ReadKey();
                Console.Clear();
            }
        }

        public void WriteActiveProducts()
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

        public void DisplayUserNotFound(Exception ex)
        {
            Console.WriteLine("{0}: {1}", ex.Message, ex.Data["user"]);
        }

        public void DisplayProductNotFound(Exception ex)
        {
            Console.WriteLine("{0}: {1}", ex.Message, ex.Data["product"]);
        }

        public void DisplayProductNotActive(Exception ex)
        {
            Console.WriteLine("{0}: {1} - {2}", ex.Message, ex.Data["id"], ex.Data["product"]);
        }

        public void DisplayUserInfo(string username)
        {
            foreach (var item in stringsystem.users)
            {
                if(item.Value.Username.Equals(username))
                {
                    User user = item.Value;
                    Console.WriteLine("Username: " + user.Username + "\nIRL Name: " + user.FirstName  + user.LastName + "\nBalance: " + user.Balance);

                    List<BuyTransaction> buyTransactions = stringsystem.GetBuyTransactionList(user.UserID, 10);

                    Console.WriteLine("\nLatest [" + buyTransactions.Count + "] transactions:");

                    foreach (var t in buyTransactions)
                    {
                        Console.WriteLine(t);
                    }

                    if(user.Balance < 5000)
                    {
                        Console.WriteLine("\n!!!WARNING!!! LOW BALANCE !!! CURRENTLY: {0,5:N2} !!!", ((double)user.Balance)/100);
                    }
                    return;
                }
            }
        }

        public void DisplayTooManyArgumentsError(string arg)
        {
            Console.Write("Too many arguments! The program supports a maximum total of three (3): " + arg);

        }

        public void DisplayAdminCommandNotFoundMessage(string arg)
        {
            Console.WriteLine("The AdminCommandLine [{0}] could not be found.", arg);
        }

        public void DisplayUserBuysProduct(uint productID)
        {
            Console.WriteLine("Bought product [{0}]:\n{1}", productID, stringsystem.GetProduct(productID));
        }

        public void DisplayUserBuysProduct(Transaction transaction, uint count)
        {
                Console.WriteLine("Completed " + count + " transactions:\n" + transaction);
        }

        public void Close()
        {
            System.Environment.Exit(0);
        }

        public void DisplayInsufficientCash(Exception ex)
        {
            Console.WriteLine("User [{0}] does not sufficient funds to complete transaction for product \"{1}\"", ex.Data["user"], ex.Data["product"]);
        }

        public void DisplayGeneralError(string errorString)
        {
            Console.WriteLine("An error occoured: " + errorString);
        }
    }
}
