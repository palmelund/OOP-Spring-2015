using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOP_Spring_2015
{
    // This exception is thrown when the specified user doesn't exist.
    public class UserDoesNotExistException : Exception
    {
        public UserDoesNotExistException()
        {

        }

        public UserDoesNotExistException(string message) : base(message)
        {

        }

        public UserDoesNotExistException(string message, Exception inner)
            : base(message, inner)
        {

        }
    }
}
