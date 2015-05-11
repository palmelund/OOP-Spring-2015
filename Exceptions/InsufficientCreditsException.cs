using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOP_Spring_2015
{
    // This exception is thrown when the user tries to buy something with insufficient credits
    class InsufficientCreditsException : Exception
    {
        public InsufficientCreditsException()
        {

        }

        public InsufficientCreditsException(string message) : base(message)
        {

        }

        public InsufficientCreditsException(string message, Exception inner) : base(message, inner)
        {

        }
    }
}
