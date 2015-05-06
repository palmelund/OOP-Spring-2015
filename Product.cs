using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOP_Spring_2015
{
    class Product
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

        public Product(uint productID, string name, uint price, bool active, bool credit)
        {
            ProductID = SetProductID(productID);
            Name = SetProductName(name);
            Price = price;
            Active = active;
            CanBeBoughtOnCredit = credit;
        }

        protected Product()
        {

        }

        uint SetProductID(uint productID)
        {
            if(productID == 0)
            {
                throw new NotImplementedException();
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
                throw new NotImplementedException();
            }
            else
            {
                return name;
            }
        }

        public void SetNewPrice(uint price)
        {
            Price = price;
        }

        public void SetActive(bool active)
        {
            Active = active;    // Should be deactivated if false
        }

        public void SetCanBeBoughtOnCredit(bool credit)
        {
            CanBeBoughtOnCredit = credit;
        }
    }
}
