using System;
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
            commandDictionary.Add(":adduser", s => this.stringsystem.AddUser(s[1], s[2], s[3], s));
        }

        // Since ParseCommand is responsible for all user input, it will also be responsible for handling all errors
        // related to input.
        public void ParseCommand(string command)
        {
            string[] s = command.Split(' ');
            try
            {
                if (s[0].StartsWith(":"))
                {
                    commandDictionary[s[0]].Invoke(s);
                }
                else
                {
                    User user;
                    user = stringsystem.GetUser(s[0]);

                    if (s.Length == 1)
                    {
                        cli.DisplayUserInfo(s[0]);
                    }
                    else if (s.Length == 2)
                    {
                        uint productID;
                        bool isNumber = uint.TryParse(s[1], out productID);
                        if(isNumber == true)
                        {
                            stringsystem.BuyProduct(user, productID);
                            cli.DisplayUserBuysProduct(productID);
                        }
                        else
                        {
                            ProductDoesNotExistException productDoesNotExistException = new ProductDoesNotExistException("The product does not exist");
                            productDoesNotExistException.Data["product"] = productID;
                            throw productDoesNotExistException;
                        }
                    }
                    else if (s.Length == 3)
                    {
                        uint productID;
                        uint numberOfProducts;

                        bool isIDNumber = uint.TryParse(s[2], out productID);
                        bool isValidNumber = uint.TryParse(s[1], out numberOfProducts);

                        if(isIDNumber == false)
                        {
                            ProductDoesNotExistException productDoesNotExistException = new ProductDoesNotExistException("The product does not exist");
                            productDoesNotExistException.Data["product"] = productID;
                            throw productDoesNotExistException;
                        }
                        else if(isValidNumber == false)
                        {
                            ArgumentOutOfRangeException argumentOutOfRangeExcaption = new ArgumentOutOfRangeException("The number of products requested can not be fulfilled as it is not valid.");
                            throw argumentOutOfRangeExcaption;
                        }
                        else
                        {
                            for (int i = 0; i < numberOfProducts; i++)
                            {
                                stringsystem.BuyProduct(user, productID);
                                cli.DisplayUserBuysProduct(productID);
                            }
                        }
                    }
                    else
                    {
                        throw new ArgumentException("Too many arguments");
                    }
                }
            }
            // User defined exceptions
            // Sending the exception to the CLI, as the userdefined exceptions can contain additional info that
            // has to bee shown to the user.
            catch(UserDoesNotExistException ex)
            {
                cli.DisplayUserNotFound(ex);
            }
            catch(ProductDoesNotExistException ex)
            {
                cli.DisplayProductNotFound(ex);
            }
            catch(ProductNotActiveException ex)
            {
                cli.DisplayProductNotActive(ex);
            }
            catch(InsufficientCreditsException ex)
            {
                cli.DisplayInsufficientCash(ex);
            }
            // Build-In Exceptions
            catch (KeyNotFoundException ex)
            {
                cli.DisplayAdminCommandNotFoundMessage(command); // <- this is the only place a dictionary should be able to get a valid key: AdminCommands.
            }
            catch (ArgumentNullException ex)
            {
                cli.DisplayGeneralError(ex.Message);
            }
            catch (Exception ex)
            {
                cli.DisplayGeneralError(ex.Message);
            }
            // No final, as the program returns to get new input here.
        }
    }
}
