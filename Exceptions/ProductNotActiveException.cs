using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOP_Spring_2015
{
    // This exception is thrown when the user tries to buy a product that is not active.
    class ProductNotActiveException : Exception
    {
         public ProductNotActiveException()
        {

        }

        public ProductNotActiveException(string message) : base(message)
        {

        }

        public ProductNotActiveException(string message, Exception inner)
            : base(message, inner)
        {

        }
    }
}
