using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.BL
{
    class SyncLogic
    {
        public SyncLogic()
        {

        }
        public Customer[] GetCustomerTosync(string user)
        {
            DB db = new DB();
            var customerapp = db.GetAppointments(user);
            var customersappintime = customerapp.Where(id => id.BookedAppointment >= DateTime.Now.AddHours(10));
            var customers = new List<Customer>();
            foreach(var customerappointment in customersappintime)
            {
                customers.Add(db.GetCustomer(customerappointment.CustomerId.ToString()));
            }
            return customers.ToArray();
        }
    }
}
