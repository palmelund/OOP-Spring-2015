using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOP_Spring_2015
{
    // Responsible for all output to the user.
    public class StringSystemCLI : IStringSystemUI
    {
        StringSystem stringsystem;

        // Adds the stringsystem, allowing all methods to use it's content.
        public StringSystemCLI(StringSystem stringsystem)
        {
            this.stringsystem = stringsystem;
        }

        // Run on program start, and will continue to do so until program is closed
        public void Start(StringSystemCommandParser parser)
        {
            string input;

            while(true) // <- no need to use break, Close() quits the application by other means.
            {
                WriteActiveProducts();
                Console.Write(">> ");
                input = Console.ReadLine();
                if(!string.IsNullOrEmpty(input)) // <- makes sure that nothing happens when the user just presses <enter>, otherwise it would complain about user not found.
                {
                    parser.ParseCommand(input);
                    Console.WriteLine("Press any key to clear screen and promt for new input:");
                    Console.ReadKey();
                }
                Console.Clear(); // <- in place so other users dont scroll through use history
            }
        }

        // Writes all active product formatted as seen in example picture.
        public void WriteActiveProducts()
        {
            Console.WriteLine(String.Format("|{0,5}|{1,-35}|{2,8}|", "ID", "Product", "Price")); // <- Currently have space for all products (including inactive) but may not suffice for future products
            WriteProductSeparationLine();

            List<Product> activeProducts = stringsystem.GetActiveProducts();

            foreach (var item in activeProducts)
            {
                WriteProductLine(item.ProductID);
            }

            WriteProductSeparationLine();
        }

        // Writes product info that will be displayed to the user in formatted manners.
        void WriteProductLine(uint id)
        {
            Console.WriteLine(String.Format("|{0,5}|{1,-35}|{2,8:N2}|", stringsystem.products[id].ProductID, stringsystem.products[id].Name, ((double)stringsystem.products[id].Price) / 100));
        }

        // Draws the separation line
        void WriteProductSeparationLine()
        {
            Console.WriteLine("|=====|===================================|========|");
        }

        //=== INTERFACE IMPLEMENTATION ===//
        // A

        public void DisplayUserNotFound(string errorMessage, string username)
        {
            Console.WriteLine("{0}: {1}", errorMessage, username);
        }

        public void DisplayProductNotFound(uint productID)
        {
            Console.WriteLine("The product is not found: {0}", productID);
        }

        public void DisplayProductNotActive(string errorMessage, uint productID, string product)
        {
            Console.WriteLine("{0}: {1} - {2}", errorMessage, productID, product);
        }

        // Displays user info
        // Text is formatted here instead of in User to give bigger freedom for formatting.
        public void DisplayUserInfo(string username)
        {
            foreach (var item in stringsystem.users)
            {
                if(item.Value.Username.Equals(username))
                {
                    User user = item.Value;
                    Console.WriteLine("Username: {0}\nIRL Name: {1} {2}\nBalance: {3:N2}",user.Username, user.FirstName, user.LastName, (double)user.Balance/100);

                    List<BuyTransaction> buyTransactions = stringsystem.GetBuyTransactionList(user.UserID, 10);

                    if(buyTransactions.Count > 0)
                    {
                        Console.WriteLine("\nLatest [" + buyTransactions.Count + "] transactions:");

                        Console.WriteLine("|{0,5}|{1,-35}|{2,8}|{3, -25}|", "ID", "Product", "Price", "Date");
                        DisplayUserInfoSeperationLine();
                        foreach (var t in buyTransactions)
                        {
                            Console.WriteLine("|{0,5}|{1,-35}|{2,8:N2}|{3, -25}|", t.TransactionID, t.product.Name, (double)t.Amount / 100, t.date);
                        }
                        DisplayUserInfoSeperationLine();
                    }
                    else
                    {
                        Console.WriteLine("No transactions yet... Why dont you buy something?");
                    }

                    if(user.CheckLowSaldo())
                    {
                        DisplayLowBalance(user);
                    }
                    return;
                }
            }
        }

        void DisplayUserInfoSeperationLine()
        {
            Console.WriteLine("|=====|===================================|========|=========================|");
        }

        public void DisplayLowBalance(User user)
        {
            Console.WriteLine("\nWARNING --- LOW BALANCE --- CURRENTLY: {0,5:N2}", ((double)user.Balance) / 100);
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

        public void Close()
        {
            System.Environment.Exit(0); // <- the program is closed with exit code 0;
                                        // No fear of data loss as everything is logged as it happens, and the user cant call
                                        // Close() while the system is doing so.
        }

        public void DisplayInsufficientCash(string user, string product)
        {
            Console.WriteLine("User [{0}] does not sufficient funds to complete transaction for product \"{1}\"", user, product);
        }

        public void DisplayInsufficientCashMultibuy(uint productID, uint totalprice, uint amount, uint affordableAmount)
        {
            Console.WriteLine("You can't afford to purchase \"{0}\" {1} times. The total price is {2:N2}, but you can only afford to buy the product {3} times.", stringsystem.GetProduct(productID).Name, amount, (double)totalprice/100, affordableAmount);
        }

        public void DisplayArgumentException(string errorstring)
        {
            Console.WriteLine("Argumenterror: " + errorstring);
        }

        public void DisplayGeneralError(string errorString)
        {
            Console.WriteLine("An error occoured: " + errorString);
        }

        public void DisplayArgumentNullException(string errorString)
        {
            Console.WriteLine("Null exception: " + errorString);
        }

        // Only used on system startup. Should not 
        public void DisplayCriticalError(string criticalErrorMessage)
        {
            Console.WriteLine("Critical Error: {0}", criticalErrorMessage);
            Console.WriteLine("Press <ESC> to stop the program or any other key to continue: ");
            ConsoleKeyInfo key = Console.ReadKey();
            if(key.Key.Equals(ConsoleKey.Escape))
            {
                Environment.Exit(1);
            }
            Console.Clear();
        }

        // Display man pages

        public void AdmDisplayQuit()
        {
            Console.WriteLine(":quit & :q");
            Console.WriteLine("Quit application");
            Console.WriteLine("Once called, the program will quit without further notice. As the program has already saved any transactions and users there might be, no data is lost in the proces.");
        }

        public void AdminDisplayActivate()
        {
            Console.WriteLine(":activate <productID>");
            Console.WriteLine("Activate product");
            Console.WriteLine("Once called, the program will activate the specified product (assuming that it exists), allowing the user to buy the product.");
            Console.WriteLine("*The program does not save the change on restart.");
        }

        public void AdminDisplayDeactivate()
        {
            Console.WriteLine(":deactivate <productID>");
            Console.WriteLine("Deactivate product");
            Console.WriteLine("Once called, the program will deactivate the specified product (assuming that it exists), preventing the user from buying it.");
            Console.WriteLine("*The program does not save the change on restart.");
        }

        public void AdminDisplayCreditsON()
        {
            Console.WriteLine(":crediton <productID>");
            Console.WriteLine("Allow credit");
            Console.WriteLine("Once called, the program will enable credit for the specified product (assuming that it exists), allowing the user to buy the product even if they dont have sufficient credits on their account.");
            Console.WriteLine("*The program does not save the change on restart.");
        }

        public void AdminDisplayCreditsOff()
        {
            Console.WriteLine(":creditoff <productID>");
            Console.WriteLine("Disallow credit");
            Console.WriteLine("Once called, the program will disable credit for the specified producy (assuming that it exists), disallowing the user from buying the product unless they have sufficient credits.");
            Console.WriteLine("*The program does not save the change on restart.");
        }

        public void AdminDisplayAddCredits()
        {
            Console.WriteLine(":addcredits <username> <amount>");
            Console.WriteLine("Add credits to user");
            Console.WriteLine("Once called, the program will add the specified amount to the user (assuming that the user exists)");
        }

        public void AdminDisplayAddUser()
        {
            Console.WriteLine(":adduser <username> <local>@<domain> <last name> <first name>[]");
            Console.WriteLine("Add user");
            Console.WriteLine("Once called, adds a new user to the system. The order of which the users info is added is: Username, Email, last name, all first names.");
            Console.WriteLine("Rules for naming:\nUsername: characters a-z 0-9 and '_'\nName: cant be empty\nEMail: for local, it can only contain a-z A-z 0-9 '.', '_' and '-', and for domain, it can unlo contain a-z A-Z 0-9 '-' and '.'. The domain is also not allowed to start with or end with '.' amd '-', and must contain at least one '.'");
        }

        public void AdminDisplaySetPrice()
        {
            Console.WriteLine(":setprice <productID> <price>");
            Console.WriteLine("Set price");
            Console.WriteLine("Once called, sets a new price for the specified product.");
            Console.WriteLine("*The program does not save the change on restart.");
        }

        public void AdminDisplayMan()
        {
            Console.WriteLine(":man <command>");
            Console.WriteLine("Manpages");
            Console.WriteLine("Displays relevant information about the specified command, and what parameters that it takes.");
        }
    }
}
