using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Text.RegularExpressions;

namespace OOP_Spring_2015
{
    class ProductsReader
    {
        Dictionary<uint, Product> products = new Dictionary<uint,Product>();

        public ProductsReader()
        {
            string[] productsString = File.ReadAllLines("..\\..\\Ressources\\products.csv", Encoding.UTF7);
            for (int i = 1; i < productsString.Length; i++) // <- start at 1 as first line isn't for sale.
            {
                // http://www.dotnetperls.com/remove-html-tags
                productsString[i] = Regex.Replace(productsString[i], "<.*?>", string.Empty);
                productsString[i] = productsString[i].Replace("\"", "");
                string[] split = productsString[i].Split(';');
                Product product = new Product(uint.Parse(split[0]), split[1], uint.Parse(split[2]), split[3].Equals("1") ? true : false);

                products.Add(product.ProductID, product);
            }
        }

        public Dictionary<uint, Product> GetProductDictionary()
        {
            return products;
        }
    }
}
