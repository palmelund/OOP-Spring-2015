using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using OOP_Spring_2015;

namespace OOP_Spring_2015.Test
{

    [TestFixture]
    class TestUser
    {
        [Test]
        void TestFirstName()
        {
            bool success1 = true;
            bool success2 = true;
            bool success3 = true;

            try
            {
                User user1 = new User(1, "frederik", "palmelund", "thepalmelund", "frederik.palmelund@gmail.com");
            }
            catch (Exception)
            {
                success1 = false;
            }

            try
            {
                User user2 = new User(1, null, "palmelund", "thepalmelund", "frederik.palmelund@gmail.com");
            }
            catch (Exception)
            {
                success2 = false;
            }

            try
            {
                User user3 = new User(1, "", "palmelund", "thepalmelund", "frederik.palmelund@gmail.com");
            }
            catch (Exception)
            {
                success3 = false;
            }

            Assert.AreEqual(true, success1);
            Assert.AreEqual(false, success2);
            Assert.AreEqual(false, success3);
        }
    }
}
