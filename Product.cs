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

        public Product(uint productID, string name, uint price, bool active/*, bool credit*/)
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

        public uint SetNewPrice(uint price)
        {
            Price = price;
            return Price;
        }

        public bool SetActive(bool active)
        {
            Active = active;
            return Active;
        }

        public bool SetCanBeBoughtOnCredit(bool credit)
        {
            CanBeBoughtOnCredit = credit;
            return CanBeBoughtOnCredit;
        }
    }
}
