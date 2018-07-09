using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Filtros.Models.Services
{
    public class AccountServices : IAccountServices
    {
        public bool CheckCredentials(string user, string password)
        {
            return string.Compare(user, password, true) == 0;
        }

        public IEnumerable<string> GetRolesForUser(string userName)
        {
            List<string> roles = new List<string>();
            if (userName.Equals("jorge", StringComparison.OrdinalIgnoreCase))
            {
                roles.Add("admin");
            }
            return roles;
        }
    }
}
