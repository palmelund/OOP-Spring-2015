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
            Console.OutputEncoding = Encoding.Unicode;  // <- To allow terminal to display special characters such as æøå, that is used for products
            
            // Start-up
            StringSystem stringsystem = new StringSystem();
            StringSystemCLI cli = new StringSystemCLI(stringsystem);
            StringSystemCommandParser parser = new StringSystemCommandParser(cli, stringsystem);
            cli.Start(parser);
        }
    }
}
