using System;
using System.Threading.Tasks;

namespace PortalExample.Services
{
    public interface IUserService
    {

        public Task<Profile> GetUserBy(string uid);
    }

    public class Profile
    {
        public string Uid { get; set; }
        public string Name { get; set; }


    }
}
