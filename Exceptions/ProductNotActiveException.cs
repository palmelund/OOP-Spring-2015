using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOP_Spring_2015
{
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
