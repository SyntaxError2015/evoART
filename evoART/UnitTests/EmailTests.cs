using evoART.Emails;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace evoART.UnitTests
{
    //[TestClass]
    public class EmailTests
    {
        [TestMethod]
        public void SendTestRegistrationEmail()
        {
            EmailSender.SendRegistrationEmail("TESTVALIDATIONTOKENASHJASHFASJFHASJFASF", "codobanclaudiu@live.com");
        }

        [TestMethod]
        public void SendTestPasswordResetEmail()
        {
            EmailSender.SendPasswordResetEmail("ASHFASHFAS", "codobanclaudiu@live.com");
        }
    }
}