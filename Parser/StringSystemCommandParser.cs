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
            //com.Add(":active", () => );
            //com[":q"].Invoke();
        }

        public void ParseCommand(string command)
        {
            if(command.StartsWith(":"))
            {
                string[] s = command.Split(' ');
                com[s[0]].Invoke(s);
            }
            else
            {
                string[] s = command.Split(' ');
            }

        }

        //void Command(string command, Action action)
        //{
        //    com.Add(":q", () => cli.Close());
        //}

        //void Command(string command, Action action)
        //{
        //    string[] s = command.Split(' ');
        //    com.Add(s[0], () => stringsystem.products[s[1]].SetActive(true));
        //}
    }
}
