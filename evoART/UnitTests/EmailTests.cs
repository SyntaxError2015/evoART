using evoART.Emails;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace evoART.UnitTests
{
    [TestClass]
    public class EmailTests
    {
        [TestMethod]
        public void SendTestRegistrationEmail()
        {
            EmailSender.SendRegistrationEmail("TESTVALIDATIONTOKENASHJASHFASJFHASJFASF", "codobanclaudiu@gmail.com");
        }

        [TestMethod]
        public void SendTestPasswordResetEmail()
        {
            
        }
    }
}