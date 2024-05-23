using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_commerce.BL.Dtos.Users
{
    public record RegisterDto(string UserName, string Email, string Password, string Role);

}
