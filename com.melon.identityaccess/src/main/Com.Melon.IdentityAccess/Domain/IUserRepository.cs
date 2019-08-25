using System;
using System.Collections.Generic;
using System.Text;

namespace Com.Melon.IdentityAccess.Domain
{
    public interface IUserRepository
    {
        User GetUserByEmail(string email);

        void Save(User user);
    }
}
