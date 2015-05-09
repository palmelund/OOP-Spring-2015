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

        public Dictionary<string, Action<string[]>> com = new Dictionary<string, Action<string[]>>();

        public StringSystemCommandParser(StringSystemCLI cli, StringSystem stringsystem)
        {
            this.cli = cli;
            this.stringsystem = stringsystem;
            com.Add(":q", s => cli.Close());
            com.Add(":quit", s => cli.Close());
            com.Add(":activate", s => this.stringsystem.products[uint.Parse(s[1])].SetActive(true));
            com.Add(":deactivate", s => this.stringsystem.products[uint.Parse(s[1])].SetActive(false));
            com.Add(":crediton", s => this.stringsystem.products[uint.Parse(s[1])].SetCanBeBoughtOnCredit(true));
            com.Add(":creditoff", s => this.stringsystem.products[uint.Parse(s[1])].SetCanBeBoughtOnCredit(false));
            com.Add(":addcredits", s => this.stringsystem.AddCreditToAccount(this.stringsystem.GetUser(s[1]), uint.Parse(s[2])));
            com.Add(":adduser", s => this.stringsystem.AddUser(s[1], s[2], s[3], s));
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
                    com[s[0]].Invoke(s);
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

                        if (isIDNumber == false || isValidNumber == false)
                        {
                            throw new ArgumentException("Argument is not a number or negative");
                        }

                        for (int i = 0; i < numberOfProducts; i++)
                        {
                            stringsystem.BuyProduct(user, productID);
                            cli.DisplayUserBuysProduct(productID);
                        }
                    }
                    else
                    {
                        throw new ArgumentException("Too many arguments");
                    }
                }
            }
            // User defined exceptions
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
            //catch (KeyNotFoundException ex)
            //{
            //    cli.DisplayGeneralError("Admin key not legal");
            //}
            catch (Exception ex)
            {
                cli.DisplayGeneralError(ex.Message);
            }
        }
    }
}
