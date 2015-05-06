using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOP_Spring_2015
{
    class Program
    {
        public static void Main()
        {
            //IStringSystem stringSystem = new IStringSystem();
            //StringSystemCLI cli = new StringSystemCLI(stringSystem);
            //StringSystemCommandParser parser = new StringSystemCommandParser(cli, stringSystem);
            //cli.Start(parser);

            User user = new User(1, "frederik", "palmelund", "thepalmelund", "frederik.palmelund@gmail.com");
            try
            {
                User user2 = new User(2, null, "palmelund", "thepalmelund", "frederik.palmelund@gmail.com");
            }
            catch (Exception ex)
            {
                Console.WriteLine("First name");
            }

            try
            {
                User user2 = new User(2, "", "palmelund", "thepalmelund", "frederik.palmelund@gmail.com");
            }
            catch (Exception ex)
            {
                Console.WriteLine("First name empty");
            }

            try
            {
                User user2 = new User(2, "frederik", null, "thepalmelund", "frederik.palmelund@gmail.com");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Lastname");
            }

            try
            {
                User user2 = new User(2, "frederik", "palmelund", "thepalmelund9_2", "frederik.palmelund@gmail.com");
            }
            catch (Exception ex)
            {
                Console.WriteLine("username 1");
            }

            try
            {
                User user2 = new User(2, "frederik", null, "#thepalmelund", "frederik.palmelund@gmail.com");
            }
            catch (Exception ex)
            {
                Console.WriteLine("username 2");
            }

            try
            {
                User user2 = new User(2, "frederik", "palmelund", "thepalmelund", "frederik.palmelund@.gmail.com");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Email");
            }

            Console.ReadKey();
        }
    }
}
