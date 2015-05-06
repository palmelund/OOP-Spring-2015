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

            Dictionary<uint, Product> products = new Dictionary<uint, Product>();
            ProductsReader productsReader = new ProductsReader();
            products = productsReader.GetProductDictionary();

            foreach (var item in products)
            {
                Console.WriteLine("Key: " + item.Key + " Value: " + item.Value);
            }

            Console.ReadKey();
        }
    }
}
