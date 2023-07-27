using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebApplication2.Shared
{
    public class ChangePasswordModel : LoginModel
    {
        public string NewPassword { get; set; }
    }
}
