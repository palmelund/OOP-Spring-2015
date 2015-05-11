using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOP_Spring_2015
{
    // This exception is thrown when the user tries to buy a product that doesn't excist.
    class ProductDoesNotExistException : Exception
    {
        public ProductDoesNotExistException()
        {

        }

        public ProductDoesNotExistException(string message) : base(message)
        {

        }

        public ProductDoesNotExistException(string message, Exception inner)
            : base(message, inner)
        {

        }
    }
}
