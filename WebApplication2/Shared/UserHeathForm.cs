using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebApplication2.Shared
{
    public  class UserHeathForm
    {
        public bool Smoke { get; set; }
        public int SmokeAmount { get; set; }
        public bool Diabetes { get; set; }
        public bool Cancer { get; set; }
        public string TypeOfCancer { get; set; }
        public bool MentalHeath { get; set; }
        public string TypeOfMentalHeath { get; set; }
        public string Other { get; set; }
    }
}
