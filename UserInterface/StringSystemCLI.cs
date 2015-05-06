using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOP_Spring_2015
{
    class StringSystemCLI
    {
        StringSystem stringsystem;

        public StringSystemCLI(StringSystem stringsystem)
        {
            this.stringsystem = stringsystem;
            Start(); // <- Dont call here
        }

        public void Start()
        {
            WriteActiveProducts();
        }

        void WriteActiveProducts()
        {
            Console.WriteLine(String.Format("|{0,5}|{1,-35}|{2,8}|", "ID", "Product", "Price"));
            Console.WriteLine("|=====|===================================|========|");

            foreach (var item in stringsystem.products)
            {
                if(item.Value.Active == true)
                {
                    Console.WriteLine(WriteProductLine(item.Key));
                }
            }
            Console.WriteLine("|=====|===================================|========|");
        }

        string WriteProductLine(uint id)
        {
            return String.Format("|{0,5}|{1,-35}|{2,8:N2}|", stringsystem.products[id].ProductID, stringsystem.products[id].Name, ((double) stringsystem.products[id].Price)/100);
        }
    }
}
