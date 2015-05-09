using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOP_Spring_2015
{
    public interface IStringSystemUI
    {
        // Because it's the cli's job to display the exception to the user
        void DisplayUserNotFound(Exception ex);
        void DisplayProductNotFound(Exception ex);
        
        void DisplayUserInfo(string username);
        void DisplayTooManyArgumentsError(string arg);
        void DisplayAdminCommandNotFoundMessage(string arg);
        void DisplayUserBuysProduct(uint productID);
        void DisplayUserBuysProduct(Transaction transaction, uint count);
        void Close();
        void DisplayInsufficientCash(Exception ex);
        void DisplayGeneralError(string errorString);
    }
}
