using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOP_Spring_2015
{
    class SeasonalProduct : Product
    {
        public DateTime SeasonStartDate
        {
            get;
            protected set;
        }

        public DateTime SeasonEndDate
        {
            get;
            protected set;
        }

        public SeasonalProduct(uint productID, string name, uint price, bool active, bool credit, DateTime seasonStartDate, DateTime seasonEndDate)
        {
            ProductID = productID;
            Name = name;
            Price = price;
            Active = active;
            CanBeBoughtOnCredit = credit;
            SeasonStartDate = seasonStartDate;
            SeasonEndDate = seasonEndDate;
        }
    }
}
