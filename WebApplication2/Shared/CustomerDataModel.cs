using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebApplication2.Shared
{
    public  class CustomerDataModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public int Income { get; set; }
        public int Age { get; set; }
        public string Address { get; set; }
        public DateTime AppointmentDate { get; set; }
    }
}
