using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOP_Spring_2015
{
    class Program
    {
        public static void Main()
        {
            Console.OutputEncoding = Encoding.Unicode;
            StringSystem stringsystem = new StringSystem();
            StringSystemCLI cli = new StringSystemCLI(stringsystem);
            StringSystemCommandParser parser = new StringSystemCommandParser(cli, stringsystem);
            cli.Start(parser);

            User user = new User((uint)stringsystem.users.Count, "Frederik", "Palmelund", "thepalmelund", "frederik.palmelund@gmail.com");
            stringsystem.users.Add(user.UserID, user);

            //Console.WriteLine(user.Balance);

            //parser.ParseCommand(":addcredits thepalmelund 10000");

            //Console.WriteLine(user.Balance);

            //parser.ParseCommand(":activate 1");

            //cli.WriteActiveProducts();

            //parser.ParseCommand(":deactivate 1");

            //cli.WriteActiveProducts();

            parser.ParseCommand(":addcredits thepalmelund 4900");

            //stringsystem.BuyProduct(user, 11);
            //stringsystem.BuyProduct(user, 13);
            //stringsystem.BuyProduct(user, 14);
            //stringsystem.BuyProduct(user, 15);
            //stringsystem.BuyProduct(user, 16);
            //stringsystem.BuyProduct(user, 11);
            //stringsystem.BuyProduct(user, 13);
            //stringsystem.BuyProduct(user, 14);
            //stringsystem.BuyProduct(user, 15);
            //stringsystem.BuyProduct(user, 16);
            //stringsystem.BuyProduct(user, 11);
            //stringsystem.BuyProduct(user, 13);
            //stringsystem.BuyProduct(user, 14);
            //stringsystem.BuyProduct(user, 15);
            //stringsystem.BuyProduct(user, 16);

            parser.ParseCommand("thepalmelund 3 11");

            //parser.ParseCommand("thepalmelund 13");

            parser.ParseCommand("thepalmelund");



            Console.ReadKey();
        }
    }
}
