using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOP_Spring_2015
{
    public interface IStringSystemUI
    {
        void DisplayUserNotFound(string username);
        void DisplayProductNotFound(uint id);
        void DisplayUserInfo(string username);
        void DisplayTooManyArgumentsError(string arg);
        void DisplayAdminCommandNotFoundMessage(string arg);
        void DisplayUserBuysProduct(uint productID);
        void DisplayUserBuysProduct(Transaction transaction, uint count);
        void Close();
        void DisplayInsufficientCash(User user);
        void DisplayGeneralError(string errorString);
    }
}
