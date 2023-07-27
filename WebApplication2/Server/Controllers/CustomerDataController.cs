using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication2.Shared;

namespace WebApplication2.Server.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CustomerDataController : ControllerBase
    {
        private static CustomerDataModel GetCustomer = new CustomerDataModel
        {
            Id = new Guid(),
            Age = 55,
            Name = "Bob Bobson",
            Income = 19000,
            Address = "Hell street hull",

        };

        [HttpGet]
        public CustomerDataModel[] Get()
        {
            return new CustomerDataModel[] { GetCustomer };
        }
        [HttpPost]
        public void Post(CustomerDataModel[] customerDataModels)
        {
            var customerdateuploaded = customerDataModels;
        }

        [Authorize]
        [HttpPost("UserData")]
        public ActionResult PostUserData([FromBody] UserForm request)
        {
            var re = request;
            return Ok();
        }
        [Authorize]
        [HttpPost("UserData2")]
        public ActionResult PostUserData([FromBody] UserHeathForm request)
        {
            var re = request;
            return Ok();
        }
        [Authorize]
        [HttpGet("GetAppointments")]
        public CustomerDataModel[] GetAppointments()
        {
            BL.DB dB = new BL.DB();
            var app = dB.GetAppointments("nat@gmail.com");
            var customers = new List<CustomerDataModel>();
            foreach (var appcustomer in app)
            {
                var temp = dB.GetCustomer(appcustomer.CustomerId);
                customers.Add(new CustomerDataModel() 
                {
                    Id = temp.UserId,
                    Name = temp.Name,
                    Age = temp.Age,
                    Address = temp.PostCode,
                    Income = temp.Income,
                    AppointmentDate = appcustomer.BookedAppointment
                });
            }  
                
            return customers.ToArray();
        }
    }
}
