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
            ProductID = productID;
            Name = name;
            Price = price;
            Active = active;
            CanBeBoughtOnCredit = credit;
        }

        protected Product()
        {

        }
    }
}
