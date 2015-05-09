using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OOP_Spring_2015;
using NUnit.Framework;

namespace TestLib
{
    [TestFixture]
    class TestUser
    {
        #region Firstname

        [Test]
        public void UserFirstNameNormal()
        {
            bool success = true;

            try
            {
                User user = new User(1, "frederik", "palmelund", "thepalmelund", "frederik.palmelund@gmail.com");
            }
            catch (Exception)
            {
                success = false;
            }

            Assert.AreEqual(true, success);
        }

        [Test]
        public void UserFirstNameNull()
        {
            bool success = true;

            try
            {
                User user = new User(1, null, "palmelund", "thepalmelund", "frederik.palmelund@gmail.com");
            }
            catch (Exception)
            {
                success = false;
            }

            Assert.AreEqual(false, success);
        }

        [Test]
        public void UserFirstNameEmpty()
        {
            bool success = true;

            try
            {
                User user = new User(1, "", "palmelund", "thepalmelund", "frederik.palmelund@gmail.com");
            }
            catch (Exception)
            {
                success = false;
            }

            Assert.AreEqual(false, success);

        }

        #endregion
        #region LastName

        [Test]
        public void UserLastNameNormal()
        {
            bool success = true;

            try
            {
                User user = new User(1, "frederik", "palmelund", "thepalmelund", "frederik.palmelund@gmail.com");
            }
            catch
            {
                success = false;
            }

            Assert.AreEqual(true, success);
        }

        [Test]
        public void UserLastNameNull()
        {
            bool success = true;

            try
            {
                User user = new User(1, "frederik", null, "thepalmelund", "frederik.palmelund@gmail.com");
            }
            catch
            {
                success = false;
            }

            Assert.AreEqual(false, success);
        }

        [Test]
        public void UserLastNameEmpty()
        {
            bool success = true;

            try
            {
                User user = new User(1, "frederik", "", "thepalmelund", "frederik.palmelund@gmail.com");
            }
            catch
            {
                success = false;
            }

            Assert.AreEqual(false, success);
        }

        #endregion
        #region Username

        [Test]
        public void UserUsernameNormal()
        {
            bool success = true;

            try
            {
                User user = new User(1, "frederik", "palmelund", "thepalmelund", "frederik.palmelund@gmail.com");
            }
            catch (Exception)
            {
                success = false;
            }

            Assert.AreEqual(true, success);
        }

        #endregion
        #region Email
        [Test]
        public void UserEMailNormal()
        {
            bool success = true;

            try
            {
                User user = new User(1, "frederik", "palmelund", "thepalmelund", "frederik.palmelund@gmail.com");
            }
            catch (Exception)
            {
                success = false;
            }

            Assert.AreEqual(true, success);
        }

        [Test]
        public void UserEMail2At()
        {
            bool success = true;

            try
            {
                User user = new User(1, "frederik", "palmelund", "thepalmelund", "frederik.palmelund@gmail@me.com");
            }
            catch (Exception)
            {
                success = false;
            }

            Assert.AreEqual(false, success);
        }

        [Test]
        public void UserEMailStartDot()
        {
            bool success = true;

            try
            {
                User user = new User(1, "frederik", "palmelund", "thepalmelund", "frederik.palmelund@.gmail.com");
            }
            catch (Exception)
            {
                success = false;
            }

            Assert.AreEqual(false, success);
        }

        [Test]
        public void UserEMailIlligalCharacter()
        {
            bool success = true;

            try
            {
                User user = new User(1, "frederik", "palmelund", "thepalmelund", "frederik.palmelund@.gm#¤%&/a#il.com");
            }
            catch (Exception)
            {
                success = false;
            }

            Assert.AreEqual(false, success);
        }

        [Test]
        public void UserEMailLocal()
        {
            bool success = true;

            try
            {
                User user = new User(1, "frederik", "palmelund", "thepalmelund", "fred$erik.palmelund@gmail.com");
            }
            catch (Exception)
            {
                success = false;
            }

            Assert.AreEqual(false, success);
        }

        [Test]
        public void UserEMailSlash()
        {
            bool success = true;

            try
            {
                User user = new User(1, "frederik", "palmelund", "thepalmelund", "frederik.palmelund@gma/il.com");
            }
            catch (Exception)
            {
                success = false;
            }

            Assert.AreEqual(false, success);
        }

        #endregion
    }
}
