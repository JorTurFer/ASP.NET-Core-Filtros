using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Filtros.Viewmodels
{
    public class LoginViewModel
    {
        [Required]
        public string Username { get; set; }
        public string Password { get; set; }        
        public string ErrorMessage { get; set; }
    }
}
