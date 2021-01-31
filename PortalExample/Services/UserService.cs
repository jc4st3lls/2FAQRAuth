using System;
using System.Threading.Tasks;

namespace PortalExample.Services
{
    public class UserService:IUserService
    {
        public UserService()
        {
        }

        public async Task<Profile> GetUserBy(string uid)
        {
            // TO-DO
            await Task.CompletedTask;
            if (uid.Equals("jc4st3lls"))
            {
                return new Profile()
                {
                    Uid = uid,
                    Name = "Jordi"
                };
            }

            return null;
        }
    }
}
