using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

namespace OOP_Spring_2015
{
    public class User : IComparable
    {
        
        public uint UserID
        {
            get;
            protected set;
        }

        public string FirstName
        {
            get;
            protected set;
        }

        public string LastName
        {
            get;
            protected set;
        }

        public string Username
        {
            get;
            protected set;
        }

        public string EMail
        {
            get;
            protected set;
        }

        public int Balance
        {
            get;
            protected set;
        }

        // Constructor for creating a user. It is expected the username and id has been checked for dublicate entry before initialization.
        public User(uint userID, string firstName, string lastName, string username, string EMail)
        {
            UserID = userID;
            FirstName = CheckIfEmpty(firstName);
            LastName = CheckIfEmpty(lastName);
            Username = SetUsername(CheckIfEmpty(username));
            this.EMail = SetEmail(CheckIfEmpty(EMail));
            Balance = 0;

        }

        // Ensures a empty name isn't saved.
        string CheckIfEmpty(string name)
        {
            if (string.IsNullOrEmpty(name))
            {
                throw new ArgumentNullException("Empty name"); // Tell user that name is empty
            }
            else
            {
                return name;
            }
        }

        // Checks that the user has used legal characters for his/her username.
        string SetUsername(string username)
        {
            Regex regex = new Regex("^[a-z0-9_]+$");

            if (string.IsNullOrEmpty(username))
            {
                throw new ArgumentNullException("No username"); // Tell user to input a username
            }
            else if (!regex.IsMatch(username))
            {
                throw new ArgumentException("Invalid Characters"); // Tell user of incorrect symbols
            }
            else
            {
                return username;
            }
        }

        // Checks that the user has used legal characters for his/her email
        string SetEmail(string EMail)
        {
            string[] emailcomp = EMail.Split('@');
            Regex localRegex = new Regex("^[A-Za-z0-9._-]+$");
            Regex domainRegex = new Regex("^[A-Za-z0-9.-]+$");
            if (emailcomp.Length == 2 && !string.IsNullOrEmpty(emailcomp[0]) && !string.IsNullOrEmpty(emailcomp[1]))
            {
                if (!localRegex.IsMatch(emailcomp[0])
                    || !domainRegex.IsMatch(emailcomp[1])
                    || emailcomp[1].StartsWith(".")
                    || emailcomp[1].StartsWith("-")
                    || emailcomp[1].EndsWith(".")
                    || emailcomp[1].EndsWith("-")
                    || !emailcomp[1].Contains("."))
                {
                    throw new ArgumentException("Invalid EMail. See \":man adduser\" for more info"); // Tell user EMail is invalid
                }
                else
                {
                    return EMail;
                }
            }
            else
            {
                throw new ArgumentException("Invalid EMail, must follow <local@domain>"); // Tell user Email is invalid
            }
        }

        // Returns the user's balance, thought the balance can also accessed directly through the getter.
        public int Saldo()
        {
            return Balance;
        }

        // Adds the specified number the the users balance. Used for InsertCashTransactions
        public void AddToBalance(int amount)
        {
            Balance += amount;
        }

        // Subtracts the specified number from the users balance. A positive number is expected for subtraction.
        public void SubtractFromBalance(int amount)
        {
            Balance -= amount;
        }

        // Returns true if the user has low saldo and needs to be warned about it.
        const int WarningBalance = 5000;
        
        public bool CheckLowSaldo()
        {
            if (Balance < WarningBalance)
            {
                return true;
            }
            else
            {
                return false; 
            }
        }

        public override string ToString()
        {
            return FirstName + " " + LastName + " " + EMail;
        }
        
        // Used for comparing two users to each other, by comparing their userID
        public override bool Equals(object obj)
        {
            if(obj == null)
            {
                return false;
            }

            User user = (obj as User);
            if (user == null)
            {
                return false;
            }
            else
            {
                return UserID.Equals(user.UserID);
            }
        }

        // Gets a hashcode for the user by returning the hascode for the users id.
        public override int GetHashCode()
        {
            return this.UserID.GetHashCode();
        }

        // Compares two users, and tells who has the bigger userID
        public int CompareTo(object obj)
        {
            if (obj == null)
            {
                //return 1;
                throw new ArgumentNullException("Failed comparing user to null");
            }

            User user = obj as User;
            if (user != null)
            {
                return (int)(UserID - user.UserID);
            }
            else
            {
                throw new ArgumentException("Object is not a user");
            }
        }
    }

}
