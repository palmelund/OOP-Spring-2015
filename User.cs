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

        public uint Balance
        {
            get;
            protected set;
        }

        public User(uint userID, string firstName, string lastName, string username, string EMail)
        {
            UserID = userID;
            FirstName = CheckIfEmpty(firstName);
            LastName = CheckIfEmpty(lastName);
            Username = SetUsername(CheckIfEmpty(username));
            EMail = SetEmail(CheckIfEmpty(EMail));
            Balance = 0;
        }

        #region Set

        string CheckIfEmpty(string name)
        {
            if (string.IsNullOrEmpty(name))
            {
                throw new NotImplementedException(); // Tell user that name is empty
            }
            else
            {
                return name;
            }
        }

        string SetUsername(string username)
        {
            // http://stackoverflow.com/questions/12350801/check-string-for-invalid-characters-smartest-way
            Regex regex = new Regex("^[a-z0-9_]+$");

            if (string.IsNullOrEmpty(username))
            {
                throw new NotImplementedException(); // Tell user to input a username
            }
            else if (!regex.IsMatch(username))
            {
                throw new NotImplementedException(); // Tell user of incorrect symbols
            }
            else
            {
                return username;
            }
        }

        string SetEmail(string EMail)
        {
            string[] emailcomp = EMail.Split('@');
            Regex localRegex = new Regex("^[A-Za-z0-9/.-_]+$");
            Regex domainRegex = new Regex("^[A-Za-z0-9/.-]+$");
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
                    throw new NotImplementedException(); // Tell user EMail is invalid
                }
                else
                {
                    return EMail;
                }
            }
            else
            {
                throw new NotImplementedException(); // Tell user Email is invalid
            }
        }

        #endregion

        #region Balance

        public uint Salso()
        {
            return Balance;
        }

        public uint AddSaldo(uint saldo)
        {
            Balance += saldo;
            return Balance;
        }

        public uint SubtractSaldo(uint saldo)
        {
            Balance -= saldo;
            return Balance;
        }

        #endregion

        public override string ToString()
        {
            return FirstName + " " + LastName + " " + EMail;
        }

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

        public override int GetHashCode()
        {
            return this.UserID.GetHashCode();
        }

        public int CompareTo(object obj)
        {
            if (obj == null)
            {
                //return 1;
                throw new ArgumentNullException();
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
