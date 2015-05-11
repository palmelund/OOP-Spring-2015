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
        // Opens the products folder, and loads all product into a products dictionary.
        public ProductsReader(ref Dictionary<uint, Product> products)
        {
            try
            {
                string[] productsString = File.ReadAllLines("../../Ressources/products.csv", Encoding.UTF7);
                int stringLength = productsString.Length;
                for (int i = 1; i < stringLength; i++) // <- start at 1 as first line isn't for sale.
                {
                    productsString[i] = Regex.Replace(productsString[i], "<.*?>", string.Empty);
                    productsString[i] = productsString[i].Replace("\"", "");
                    string[] split = productsString[i].Split(';');
                    Product product = new Product(uint.Parse(split[0]), split[1], uint.Parse(split[2]), split[3].Equals("1") ? true : false);

                    products.Add(product.ProductID, product);
                }
            }
            catch (Exception)
            {
                throw new FileNotFoundException("Product file not found");
            }
        }
    }
}
