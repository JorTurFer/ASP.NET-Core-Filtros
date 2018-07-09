using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Filtros.Models.Services
{
    public interface IAccountServices
    {
        bool CheckCredentials(string user, string password);
        IEnumerable<string> GetRolesForUser(string userName);
    }
}
