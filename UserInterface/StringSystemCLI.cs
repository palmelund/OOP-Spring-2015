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

        // Run on program start, and will continue to do so until program is closed
        public void Start(StringSystemCommandParser parser)
        {
            string input;

            while(true) // <- no need to use break, Close() quits the application by other means.
            {
                WriteActiveProducts();
                Console.Write(">> ");
                input = Console.ReadLine();
                parser.ParseCommand(input);
                Console.WriteLine("Press any key to clear screen and promt for new input:");
                Console.ReadKey();
                Console.Clear(); // <- in place so other users dont scroll through use history
            }
        }

        // Writes all active product formatted as seen in example picture.
        public void WriteActiveProducts()
        {
            Console.WriteLine(String.Format("|{0,5}|{1,-35}|{2,8}|", "ID", "Product", "Price")); // <- Currently have space for all products (including inactive) but may not suffice for future products
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

        // Displays user info
        // Text is formatted here instead of in User to give bigger freedom for formatting.
        public void DisplayUserInfo(string username)
        {
            foreach (var item in stringsystem.users)
            {
                if(item.Value.Username.Equals(username))
                {
                    User user = item.Value;
                    Console.WriteLine("Username: " + user.Username + "\nIRL Name: " + user.FirstName  + user.LastName + "\nBalance: " + user.Balance);

                    List<BuyTransaction> buyTransactions = stringsystem.GetBuyTransactionList(user.UserID, 10);

                    if(buyTransactions.Count > 0)
                    {
                        Console.WriteLine("\nLatest [" + buyTransactions.Count + "] transactions:");

                        Console.WriteLine("|{0,5}|{1,-35}|{2,8}|{3, -25}|", "ID", "Product", "Price", "Date");
                        Console.WriteLine("|=====|===================================|========|=========================|");
                        foreach (var t in buyTransactions)
                        {
                            Console.WriteLine("|{0,5}|{1,-35}|{2,8:N2}|{3, -25}|", t.TransactionID, t.product.Name, (double)t.Amount / 100, t.date);
                        }
                        Console.WriteLine("|=====|===================================|========|=========================|");
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

        public void DisplayUserBuysProduct(Transaction transaction, uint count)
        {
                Console.WriteLine("Completed " + count + " transactions:\n" + transaction);
        }

        public void Close()
        {
            System.Environment.Exit(0); // <- the program is closed with exit code 0;
                                        // No fear of data loss as everything is logged as it happens, and the user cant call
                                        // Close() while the system is doing so.
        }

        public void DisplayInsufficientCash(Exception ex)
        {
            Console.WriteLine("User [{0}] does not sufficient funds to complete transaction for product \"{1}\"", ex.Data["user"], ex.Data["product"]);
        }

        public void DisplayGeneralError(string errorString)
        {
            Console.WriteLine("An error occoured: " + errorString);
        }

        public void DisplayCriticalError(Exception ex)
        {
            Console.WriteLine("Critical Error: {0}", ex.Message);
            Console.WriteLine("Press <ESC> to stop the program or any other key to continue: ");
            ConsoleKeyInfo key = Console.ReadKey();
            if(key.Key.Equals(ConsoleKey.Escape))
            {
                Environment.Exit(1);
            }
            Console.Clear();
        }

        // Display manpages

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
            Console.WriteLine("Once called, the program will disable credit for the specified producy (assuming that it exits), disallowing the user from buying the product unless they have sufficient credits.");
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
            Console.WriteLine("Rules for naming:\nUsername: characters a-z 0-9 and '_'\nName: cant be empty\nEMail: for local, it can only contain a-z A-z 0-9 '.', '_' and '-', and for domain, it can unlo contain a-z A-Z 0-9 '-' and '.'");
        }

        public void AdminDisplayMan()
        {
            Console.WriteLine(":man <command>");
            Console.WriteLine("Manpages");
            Console.WriteLine("Displays relevant information about the specified command, and what parameters that it takes.");
        }
    }
}
