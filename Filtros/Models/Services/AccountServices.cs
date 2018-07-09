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
    }
}
