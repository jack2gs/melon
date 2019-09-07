using Com.Melon.Core.Domain;
using System.Text.RegularExpressions;

namespace Com.Melon.IdentityAccess.Domain
{
    public class Email: ValueObject<Email>
    {
        private string _emailAddress;

        public string EmailAddress
        {
            get {
                return _emailAddress;
            }
            set {
                string email = value;
                Regex regex = new Regex(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$");
                Match match = regex.Match(email);
                SelfAssertArgumentTrue(match.Success, "Invalid email address");
                SelfAssertArgumentLength(email, 50, "The email should be less than 50 characters");

                _emailAddress = email;
            }
        }

        public Email(string emailAddress)
        {
            this.EmailAddress = emailAddress; 
        }

        private Email() { }
    }
}
