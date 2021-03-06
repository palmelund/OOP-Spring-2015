﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOP_Spring_2015
{
    public class StringSystemCommandParser
    {
        StringSystemCLI cli;
        StringSystem stringsystem;

        public Dictionary<string, Action<string[]>> commandDictionary = new Dictionary<string, Action<string[]>>();

        // Adds all admin commands when initiated
        public StringSystemCommandParser(StringSystemCLI cli, StringSystem stringsystem)
        {
            this.cli = cli;
            this.stringsystem = stringsystem;

            // Add admin command to commandDictionary

            // Quits the program
            commandDictionary.Add(":q", s => cli.Close());
            commandDictionary.Add(":quit", s => cli.Close());

            // Activate or deactivate a product - :activate <productID>
            // Example - :activate 1
            commandDictionary.Add(":activate", s => this.stringsystem.products[uint.Parse(s[1])].SetActive(true));
            commandDictionary.Add(":deactivate", s => this.stringsystem.products[uint.Parse(s[1])].SetActive(false));

            // Activate ir deactivate credit for product, allowing user to overdraw on their account - :crediton <productID>
            // Example - :crediton 1
            commandDictionary.Add(":crediton", s => this.stringsystem.products[uint.Parse(s[1])].SetCanBeBoughtOnCredit(true));
            commandDictionary.Add(":creditoff", s => this.stringsystem.products[uint.Parse(s[1])].SetCanBeBoughtOnCredit(false));
            
            // Add credits to existing account. Added credits are in øre (1/100 kr) - :addcredits <username> <amount>
            // Example - :addcredits user 10000
            commandDictionary.Add(":addcredits", s => this.stringsystem.AddCreditToAccount(this.stringsystem.GetUser(s[1]), uint.Parse(s[2])));
            
            // Add new user to the system. Username is added last, but will include all first names and middle names - :adduser <username> <email> <lastname> <firstname(s)>
            commandDictionary.Add(":adduser", s => this.stringsystem.AddUser(DoesUserExist(s[1]), s[2], s[3], s));

            // Sets a new price for a product - :setprice <productID> <new price>
            // Example - :setprice 1 100
            commandDictionary.Add(":setprice", s => stringsystem.products[uint.Parse(s[1])].SetNewPrice(uint.Parse(s[2])));

            // Display use information about specified command - :man <command>
            // Example - :man q
            commandDictionary.Add(":man", s => DisplayManPages(s[1]));
        }

        // Since ParseCommand is responsible for all user input, it will also be responsible for handling all errors
        // related to input.
        public void ParseCommand(string command)
        {
            string[] s = command.Split(' ');
            try
            {
                // If the input starts with ":" it is treated as an admin command, otherwise it will be read as a user.
                if (s[0].StartsWith(":"))
                {
                    commandDictionary[s[0]].Invoke(s);
                }
                else
                {
                    User user;
                    user = stringsystem.GetUser(s[0]);

                    // If length 1, it will just print info about the user.
                    if (s.Length == 1)
                    {
                        cli.DisplayUserInfo(s[0]);
                    }
                    // If length 2, it will búy a product for the user, assuming the product exists.
                    else if (s.Length == 2)
                    {
                        uint productID;
                        bool isNumber = uint.TryParse(s[1], out productID);
                        if(isNumber == true)
                        {
                            stringsystem.BuyProduct(user, productID);
                            cli.DisplayUserBuysProduct(productID);
                            if (user.CheckLowSaldo())
                            {
                                cli.DisplayLowBalance(user);
                            }
                        }
                        else
                        {
                            cli.DisplayProductNotFound(productID);
                        }
                    }
                    // If length 3, it will buy a number of products for the user, assuming the number is legal and the product exists.
                    else if (s.Length == 3)
                    {
                        uint productID;
                        uint numberOfProducts;

                        bool isIDNumber = uint.TryParse(s[2], out productID);
                        bool isValidNumber = uint.TryParse(s[1], out numberOfProducts);
                        stringsystem.GetProduct(productID); // <- this is here to check if product exists, and throw a ProductNotFoundException, as it would otherwise result in a KeyNotFoundException, that is only expected to happen when using an invalid AdminCommand.

                        if(isIDNumber == false)
                        {
                            cli.DisplayProductNotFound(productID);
                        }
                        else if(isValidNumber == false || numberOfProducts < 1)
                        {
                            cli.DisplayArgumentException("Cant complete transaction as the specified number isn't allowed: " + numberOfProducts);
                        }
                        else
                        {
                            // Ensure that the user can afford the purchase, otherwise let the user know why they cant afford it, and how many they can.
                            uint totalprice = numberOfProducts * stringsystem.products[productID].Price;

                            if(totalprice <= user.Balance)
                            {
                                for (int i = 0; i < numberOfProducts; i++)
                                {
                                    stringsystem.BuyProduct(user, productID);
                                    cli.DisplayUserBuysProduct(productID);
                                    if (user.CheckLowSaldo())
                                    {
                                        cli.DisplayLowBalance(user);
                                    }
                                }
                            }
                            else
                            {
                                uint affordableAmount = 0;
                                uint affordabletotalprice = 0;
                                uint pieceprice = stringsystem.products[productID].Price;
                                while(affordabletotalprice < user.Balance - pieceprice)
                                {
                                    affordabletotalprice += pieceprice;
                                    affordableAmount++;
                                }

                                cli.DisplayInsufficientCashMultibuy(productID, totalprice, numberOfProducts, affordableAmount);
                            }
                        }
                    }
                    // Should the user add more arguments, the system throws an exception, in stead of guessing what the user wants.
                    else
                    {
                        cli.DisplayArgumentException("Too many arguments!");
                    }
                }
            }
            // Some exception cleanup has happened, so it is not guaranteed that all exceptions will catch anything, but are left alone to be safe.

            // User defined exceptions
            catch(UserDoesNotExistException ex)
            {
                cli.DisplayUserNotFound(ex.Message, ex.Data["user"].ToString());
            }
            catch(ProductDoesNotExistException ex)
            {
                cli.DisplayProductNotFound(uint.Parse(ex.Data["product"].ToString()));
            }
            catch(ProductNotActiveException ex)
            {
                cli.DisplayProductNotActive(ex.Message, uint.Parse(ex.Data["id"].ToString()), ex.Data["product"].ToString());
            }
            catch(InsufficientCreditsException ex)
            {
                cli.DisplayInsufficientCash(ex.Data["user"].ToString(), ex.Data["product"].ToString());
            }
            // Build-In Exceptions
            catch (KeyNotFoundException)
            {
                cli.DisplayAdminCommandNotFoundMessage(command);
            }
            catch (ArgumentNullException ex)
            {
                cli.DisplayArgumentNullException(ex.Message);
            }
            catch (ArgumentException ex)
            {
                cli.DisplayArgumentException(ex.Message);
            }
            catch (Exception ex)
            {
                cli.DisplayGeneralError(ex.Message);
            }
        }

        // Switch statement for man pages, deciding what message to show for the user.
        void DisplayManPages(string command)
        {
            switch(command)
            {
                case "q":
                case "quit":
                    cli.AdmDisplayQuit();
                    break;
                case "activate":
                    cli.AdminDisplayActivate();
                    break;
                case "deactivate":
                    cli.AdminDisplayDeactivate();
                    break;
                case "crediton":
                    cli.AdminDisplayCreditsON();
                    break;
                case "creditoff":
                    cli.AdminDisplayCreditsOff();
                    break;
                case "addcredits":
                    cli.AdminDisplayAddCredits();
                    break;
                case "adduser":
                    cli.AdminDisplayAddUser();
                    break;
                case "setprice":
                    cli.AdminDisplaySetPrice();
                    break;
                case "man":
                    cli.AdminDisplayMan();
                    break;
                default:
                    cli.DisplayArgumentException("Command not found: " + command);
                    break;
            }
        }

        // Checks if the user exists when creating a new user, avoiding dublicate usernames.
        string DoesUserExist(string username)
        {
            try
            {
                stringsystem.GetUser(username);
            }
            catch (Exception)   // <- if an exception is thrown, it means that the username doesnøt exist.
            {
                return username;
            }
            throw new ArgumentException("Username already taken");
        }
    }
}
