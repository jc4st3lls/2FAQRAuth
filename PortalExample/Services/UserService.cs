using System;
using System.Threading.Tasks;
using PortalExample.Models;

namespace PortalExample.Services
{
    public class UserService:IUserService
    {
     

        public async Task<Profile> GetUserBy(string uid)
        {
            // TO-DO
            await Task.CompletedTask;

            if (AppSet.DbUsers.TryGetValue(uid, out var profile))
            {
                return profile;
            }

            return null;
        }
    }
}
