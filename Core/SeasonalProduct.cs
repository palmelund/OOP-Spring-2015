using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

// This class is not used as there is no build-in support for seasonal produtcs in the current implementation.

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

        // Constructor for seasonal products
        public SeasonalProduct(uint productID, string name, uint price, bool active, DateTime seasonStartDate, DateTime seasonEndDate)
        {
            ProductID = productID;
            Name = name;
            Price = price;
            Active = active;
            SeasonStartDate = SetStartDate(seasonStartDate);
            SeasonEndDate = SetEndDate(seasonEndDate);
        }

        DateTime SetStartDate(DateTime date)
        {
            if(date.Equals(null))
            {
                Active = true;
            }
            return date;
        }

        DateTime SetEndDate(DateTime date)
        {
            if(date.Equals(null))
            {
                Active = true;
            }
            return date;
        }
    }
}
