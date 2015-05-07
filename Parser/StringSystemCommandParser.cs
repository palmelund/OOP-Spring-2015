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
            com.Add(":activate", s => stringsystem.products[uint.Parse(s[1])].SetActive(true));
            com.Add(":deactivate", s => stringsystem.products[uint.Parse(s[1])].SetActive(false));
            com.Add(":crediton", s => stringsystem.products[uint.Parse(s[1])].SetCanBeBoughtOnCredit(true));
            com.Add(":creditoff", s => stringsystem.products[uint.Parse(s[1])].SetCanBeBoughtOnCredit(false));
            com.Add(":addcredits", s => stringsystem.users[(stringsystem.GetUser(s[1])).UserID].AddToBalance(int.Parse(s[2])));

        }

        public void ParseCommand(string command)
        {
            string[] s = command.Split(' ');
         
            if(s[0].StartsWith(":"))
            {
                com[s[0]].Invoke(s);
            }
            else
            {
                User user;
                user = stringsystem.GetUser(s[0]);

                if(s.Length == 1)
                {
                    cli.DisplayUserInfo(s[0]);
                }
                else if (s.Length == 2)
                {
                    uint productID;
                    bool isNumber = uint.TryParse(s[1], out productID);
                    stringsystem.BuyProduct(user, productID);
                    cli.DisplayUserBuysProduct(productID);
                }
                else if (s.Length == 3)
                {
                    uint productID;
                    int numberOfProducts;

                    bool isIDNumber = uint.TryParse(s[2], out productID);
                    bool isValidNumber = int.TryParse(s[1], out numberOfProducts);

                    if(isIDNumber == false ||isValidNumber == false)
                    {
                        throw new ArgumentException("Argument is NaN");
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
    }
}
