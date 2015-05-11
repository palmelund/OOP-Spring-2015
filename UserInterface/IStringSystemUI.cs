using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOP_Spring_2015
{
    public interface IStringSystemUI
    {
        void DisplayUserNotFound(string errorMessage, string username);
        void DisplayProductNotFound(string errorMessage, uint productID);
        void DisplayProductNotActive(string errorMessage, uint productID, string product);
        
        void DisplayUserInfo(string username);
        void DisplayLowBalance(User user);
        void DisplayTooManyArgumentsError(string arg);
        void DisplayAdminCommandNotFoundMessage(string arg);
        void DisplayUserBuysProduct(uint productID);
        void Close();
        void DisplayInsufficientCash(string user, string product);
        void DisplayCriticalError(string criticalErrorMessage); // <- This shouldn't be called unless something really bad happens.
        void DisplayGeneralError(string errorString);
        void DisplayArgumentException(string errorString);
        void DisplayArgumentNullException(string errorString);
    }
}
