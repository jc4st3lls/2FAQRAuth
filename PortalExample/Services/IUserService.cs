using System;
using System.Threading.Tasks;
using PortalExample.Models;

namespace PortalExample.Services
{
    public interface IUserService
    {

        public Task<Profile> GetUserBy(string uid);
    }

    
}
