using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Xml.Linq;
using System.Xml.Resolvers;

namespace evoART.Emails
{
    public static class EmailSender
    {

        #region Fields and properties
        private static SmtpClient _emailClient = null;

        private static SmtpClient EmailClient
        {
            get
            {
                var xml = XDocument.Load(@"Emails\EmailSettings.xml");

                var credentials = xml.Root.Descendants("sender").Select(item => new
                {
                    username = item.Attribute("email").Value,
                    password = item.Attribute("password").Value
                });

                return _emailClient ?? (_emailClient = new SmtpClient
                {
                    EnableSsl = true,
                    Port = 587,
                    DeliveryMethod = SmtpDeliveryMethod.Network,
                    UseDefaultCredentials = false,
                    Host = "smtp.gmail.com",
                    Credentials = new NetworkCredential(credentials.ElementAt(0).username, credentials.ElementAt(0).password),
                });
            }
        }

        #endregion

        #region PublicMethods

        /// <summary>
        /// Send an email to an user in order to help him/her validate his account
        /// </summary>
        /// <param name="validationToken">The token that is needed for validation</param>
        /// <param name="emailTarget">The email address where to send this email</param>
        public static void SendRegistrationEmail(string validationToken, string emailTarget)
        {
            var email = GenerateRegistrationMessage(validationToken, emailTarget);

            EmailClient.Send(email);
        }

        /// <summary>
        /// Send an email to an user in order to help him/her reset his account password
        /// </summary>
        /// <param name="validationToken">The token that is needed for validation</param>
        /// <param name="emailTarget">The email address where to send this email</param>
        public static void SendPasswordResetEmail(string validationToken, string emailTarget)
        {
            var email = GeneratePasswordResetMessage(validationToken, emailTarget);

            EmailClient.Send(email);
        }

        #endregion

        #region Private methods

        private static MailMessage GenerateRegistrationMessage(string validationToken, string emailTarget)
        {
            return GenerateMailMessage("register", validationToken, emailTarget);
        }

        private static MailMessage GeneratePasswordResetMessage(string validationToken, string emailTarget)
        {
            return GenerateMailMessage("password", validationToken, emailTarget);
        }

        private static MailMessage GenerateMailMessage(string node, string innerBodyText, string emailTarget)
        {
            var xml = XDocument.Load(@"Emails\EmailSettings.xml");

            var query = xml.Descendants(node).Select(item => new
            {
                subject = item.Element("subject").Attribute("title").Value,
                bodyPart1 = item.Element("body1").Value,
                bodyPart2 = item.Element("body2").Value
            });

            var details = query.ElementAt(0);

            var sender = xml.Root.Descendants("sender").Attributes("email").ElementAt(0).Value;

            var body = details.bodyPart1 + "<br/><br/><b>" + innerBodyText + "</b><br/><br/>\n\n" + details.bodyPart2;

            var email = new MailMessage(sender, emailTarget)
            {
                Subject = details.subject,
                Body = body,
                IsBodyHtml = true
            };

            return email;
        }

        #endregion
    }
}