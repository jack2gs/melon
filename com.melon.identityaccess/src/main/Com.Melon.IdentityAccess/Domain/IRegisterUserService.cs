using System;
using System.Collections.Generic;
using System.Text;

namespace Com.Melon.IdentityAccess.Domain
{
    public interface IRegisterUserService
    {
        void RegisterUser(string email, string password);
    }
}
