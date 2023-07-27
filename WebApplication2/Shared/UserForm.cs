using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebApplication2.Shared
{
    public class UserForm
    {
        public Guid UserId { get; set; }
        public string Name { get; set; }
        public int Age { get; set; }
        public string PostCode { get; set; }
        public int Income { get; set; }
        public string HouseType { get; set; }
        public int HouseValue { get; set; }
        public string Reason { get; set; }
        public DateTime DateTimeMovedin { get; set; }
        public char Geneder { get; set; }
        public int PhoneNumber { get; set; }
        public string[] heathProblems { get; set; }
    }
}
