using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_commerce.DAL.Data.Clients
{
    public class ApplicationUser : IdentityUser
    {
        public string Role { get; set; } = string.Empty;
    }
}
