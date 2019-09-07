using Com.Melon.Core.Domain;

namespace Com.Melon.IdentityAccess.Domain
{
    public class Password: ValueObject<Password>
    {
        private string _password;

        public string PasswordString
        {
            get
            {
                return _password;
            }

            private set
            {
                string password = value;
                int maxLength = 20;
                int minLength = 6;
                SelfAssertArgumentLength(password, minLength, maxLength, $"The length should be between {minLength} and {maxLength}.");
                _password = password;
            }
        }

        public Password(string password)
        {
            PasswordString = password;
        }

        private Password() { }
    }
}
