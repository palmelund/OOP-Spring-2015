using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOP_Spring_2015
{
    public interface IStringSystemUI
    {
        //TODO: ADD NEW METHODS TO INTERFACE
        // Because it's the cli's job to display the exception to the user
        void DisplayUserNotFound(Exception ex);
        void DisplayProductNotFound(Exception ex);
        void DisplayProductNotActive(Exception ex);
        
        void DisplayUserInfo(string username);
        void DisplayLowBalance(User user);
        void DisplayTooManyArgumentsError(string arg);
        void DisplayAdminCommandNotFoundMessage(string arg);
        void DisplayUserBuysProduct(uint productID);
        void Close();
        void DisplayInsufficientCash(Exception ex);
        void DisplayCriticalError(Exception ex); // <- This shouldn't be called
        void DisplayGeneralError(string errorString);
    }
}
