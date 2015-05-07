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
            Console.OutputEncoding = System.Text.Encoding.Unicode;
            StringSystem stringsystem = new StringSystem();

            StringSystemCLI cli = new StringSystemCLI(stringsystem);

            StringSystemCommandParser parser = new StringSystemCommandParser(cli, stringsystem);

            User user = new User((uint) stringsystem.users.Count, "Frederik", "Palmelund", "thepalmelund", "frederik.palmelund@gmail.com");
            stringsystem.users.Add((uint)stringsystem.users.Count,user);

            Console.WriteLine(user.Balance);

            parser.ParseCommand(":addcredits thepalmelund 10000");

            Console.WriteLine(user.Balance);

            //parser.ParseCommand(":activate 1");

            //cli.WriteActiveProducts();

            //parser.ParseCommand(":deactivate 1");

            //cli.WriteActiveProducts();

            Console.ReadKey();
        }
    }
}
