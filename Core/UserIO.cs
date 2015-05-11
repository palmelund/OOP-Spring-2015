using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace OOP_Spring_2015
{
    public class UserIO
    {
        // Loads all existing users into a dictionary on startup.
        public UserIO(ref Dictionary<uint, User> users)
        {
            try
            {
                string[] userlist = File.ReadAllLines("../../Ressources/user.csv");
                int stringlength = userlist.Length;

                for (int i = 1; i < stringlength; i++)
                {
                    string[] split = userlist[i].Split(';');
                    User user = new User(uint.Parse(split[0]), split[2], split[3], split[1], split[4]);
                    user.AddToBalance(int.Parse(split[5]));

                    users.Add(user.UserID, user);
                }
            }
            catch (Exception)
            {
                throw new FileNotFoundException("The user file could not be found.");
            }
        }

        // Adds a newly created user to the userfile. It is expected that the userfile still exists if an exception wasn't thrown on startup.
        public void AddUserToFile(User user)
        {
            string savefile = FormatUserForSave(user);
            File.AppendAllText("../../Ressources/user.csv", savefile + Environment.NewLine);
        }

        // A more complete tostring version for the user containing everything that has to be saved to the file.
        public string FormatUserForSave(User user)
        {
            return user.UserID + ";" + user.Username + ";" + user.FirstName + ";" + user.LastName + ";" + user.EMail + ";" + user.Balance;
        }
    }
}
