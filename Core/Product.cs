using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOP_Spring_2015
{
    public class Product
    {

        public uint ProductID
        {
            get;
            protected set;
        }

        public string Name
        {
            get;
            protected set;
        }

        public uint Price
        {
            get;
            protected set;
        }

        public bool Active
        {
            get;
            protected set;
        }

        public bool CanBeBoughtOnCredit
        {
            get;
            protected set;
        }

        // Constructor for loading a product into the system.
        public Product(uint productID, string name, uint price, bool active)
        {
            ProductID = SetProductID(productID);
            Name = SetProductName(name);
            Price = price;
            Active = active;
            CanBeBoughtOnCredit = false;
        }

        protected Product()
        {

        }

        // Sets the product id if it isn't 0
        uint SetProductID(uint productID)
        {
            if(productID == 0)
            {
                throw new ArgumentNullException("Product can not have ID 0");
            }
            else
            {
                return productID;
            }
        }

        // Set's the product name if it isn't null or empty.
        string SetProductName(string name)
        {
            if(string.IsNullOrEmpty(name))
            {
                throw new ArgumentNullException("Product does not have a name");
            }
            else
            {
                return name;
            }
        }

        // Sets the new price for the product
        public uint SetNewPrice(uint price)
        {
            Price = price;
            return Price;
        }

        // Sets if the user can buy the product. If true, it is for sale.
        public void SetActive(bool active)
        {
            Active = active;
        }

        // Sets if the user can buy the product with insufficient credits.
        public bool SetCanBeBoughtOnCredit(bool credit)
        {
            CanBeBoughtOnCredit = credit;
            return CanBeBoughtOnCredit;
        }

        public override string ToString()
        {
            return "ID: " + ProductID + " Name: " + Name + " Price: " + Price;
        }
    }
}
