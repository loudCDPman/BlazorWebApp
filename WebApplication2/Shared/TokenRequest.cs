using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebApplication2.Shared
{
    public class TokenRequest
    {
        public Guid UserId { get; set; }
        public string email { get; set; }
        public string Userpw { get; set; }

    }

    public class Token
    {
        public string token { get; set; }
        public string RefreshToken { get; set; }
    }
}
