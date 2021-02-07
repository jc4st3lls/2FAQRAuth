using System;
using System.Collections.Concurrent;
using PortalExample.Models;

namespace PortalExample
{
    public static class AppSet
    {
        internal static ConcurrentDictionary<string, Profile> DbUsers { get; private set; }

        static AppSet()
        {
            DbUsers = new ConcurrentDictionary<string, Profile>();

            DbUsers.TryAdd("jc4st3lls",new Profile()
            {
                Uid = "jc4st3lls",
                Name = "Jordi Zero Days"
            });


        }
    }
}
