using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace BL
{
    public class User
    {
        public Guid UserId { get; set; }
        public string UserName { get; set; }
        public string UserPW { get; set; }
        public string UserRole { get; set; }

    }

    public class Customer
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
    public class Appointments 
    {
        public int Id { get; set; }
        public Guid CustomerId { get; set; }
        public Guid UserId { get; set; }
        public DateTime BookedAppointment { get; set; }
        public bool Completed { get; set; }
    }
    public class RefreshToken
    {
        public Guid id { get; set; }
        public string refresh { get; set; }
        public DateTime deadline { get; set; }
        public Guid UserId { get; set; }
    }
    
    public class DB
    {
        private List<User> userdb = new List<User>();
        private List<Customer> customerdb = new List<Customer>();
        private List<Appointments> Appointmentsdb = new List<Appointments>();
        private List<RefreshToken> refreshTokensdb = new List<RefreshToken>();
        public DB()
        {
            Onload();
            fakedata();
            //refreshTokensdb.Add(new RefreshToken
            //{
            //    refresh = @"09vz7KyJVqq1RXg/vnC5Tq4cPH4iRcCc2U0I0rrdmFQvthHSmdlpN11gepr1+g2r5LmZRv0DkI4aVhNegIPv2KwkEAhf5rTUsgQgwsompt8/XO4BRFJWKyaCuELGC7bZcKLt0eoETP0ExJP4487D9LA0HiOU7pNazeTi3C5yDqc=",
            //    deadline = new DateTime(2023,08, 01 ),
            //    UserId = new Guid("9e252e0a-0f6b-463f-a104-b256fedc67a3")
            //});
        }
        public void fakedata()
        {
            userdb.Add(new User()
            {
                UserId = new Guid("9e252e0a-0f6b-463f-a104-b256fedc67a3"),
                UserName = "nat@gmail.com",
                UserPW = "blam",
                UserRole = "admin"
            });
            userdb.Add(new User()
            {
                UserId = Guid.NewGuid(),
                UserName = "nath@gmail.com",
                UserPW = "blams",
                UserRole = "user"
            });
            customerdb.Add(new Customer()
            {
                UserId = Guid.NewGuid(),
                Name = "Bobs bobson",
                Age = 55
            });
            customerdb.Add(new Customer()
            {
                UserId = Guid.NewGuid(),
                Name = "god godson",
                Age = 56,
                PostCode = "ls9"
            });
            Appointmentsdb.Add(new Appointments
            {
                Id = 0,
                CustomerId = GetCustomer("Bobs bobson").UserId,
                UserId = GetUser("nat@gmail.com").UserId,
                BookedAppointment = DateTime.Now.AddDays(1),
                Completed = false
            });
            Appointmentsdb.Add(new Appointments
            {
                Id = 1,
                CustomerId = GetCustomer("god godson").UserId,
                UserId = GetUser("nat@gmail.com").UserId,
                BookedAppointment = DateTime.Now.AddDays(1),
                Completed = false
            });


        }
        public void Submit()
        {
            string json = JsonSerializer.Serialize(refreshTokensdb);
            File.WriteAllText("refreshTokensdb.json", json);
            string json1 = JsonSerializer.Serialize(Appointmentsdb);
            File.WriteAllText("Appointmentsdb.json", json1);
            string json2 = JsonSerializer.Serialize(customerdb);
            File.WriteAllText("customerdb.json", json2);
            string json3 = JsonSerializer.Serialize(userdb);
            File.WriteAllText("userdb.json", json3);
        }
        public void Onload()
        {
            try
            {
                string json = File.ReadAllText("refreshTokensdb.json");
                refreshTokensdb = JsonSerializer.Deserialize<RefreshToken[]>(json).ToList();
                string json1 = File.ReadAllText("Appointmentsdb.json");
                Appointmentsdb = JsonSerializer.Deserialize<Appointments[]>(json1).ToList();
                string json2 = File.ReadAllText("customerdb.json");
                customerdb = JsonSerializer.Deserialize<Customer[]>(json2).ToList();
                string json3 = File.ReadAllText("userdb.json");
                userdb = JsonSerializer.Deserialize<User[]>(json3).ToList();
            }
            catch
            {

            }
        }
        
        public bool UserRole(string role, string UserName)
        {
            return userdb.FirstOrDefault(u => u.UserName == UserName).UserRole.Equals(role);
        }

        public User GetUser(string UserName)
        {
            return userdb.FirstOrDefault(u => u.UserName == UserName);
        }

        public User GetUser(Guid guid)
        {
            return userdb.FirstOrDefault(u => u.UserId == guid);
        }

        public bool CheckPw(string username, string pw)
        {
            return userdb.FirstOrDefault(u => u.UserName == username).UserPW == pw;
        }

        public Customer GetCustomer(string name)
        {
            return customerdb.FirstOrDefault(c => c.Name == name);
        }
        public Customer GetCustomer(Guid CustomerId)
        {
            return customerdb.FirstOrDefault(c => c.UserId == CustomerId);
        }
        public Appointments[] GetAppointments(string user)
        {
            return Appointmentsdb.Where(a => a.UserId == GetUser(user).UserId).ToArray();
        }
        public void SetRefreshtoken(string user, string token)
        {
            refreshTokensdb.Add(new RefreshToken()
            {
                id = Guid.NewGuid(),
                UserId = GetUser(user).UserId,
                refresh = token,
                deadline = DateTime.Now.AddDays(30)
            });
            Submit();
        }
        public bool CheckRefreshtoken(string token)
        {
            if(!refreshTokensdb.Any(x=>x.refresh == token))
            {
                return false;
            }
            return refreshTokensdb.First(t => t.refresh == token)!.deadline > DateTime.Now;
        }
        public void InvalidRefreshtoken(string token)
        {
            refreshTokensdb.Remove(refreshTokensdb.FirstOrDefault(x => x.refresh == token));
            Submit();
        }
        public void InvalidRefreshtokenLogout(string username)
        {
            refreshTokensdb.Remove(refreshTokensdb.FirstOrDefault(x => x.UserId == GetCustomer(username).UserId));
            Submit();
        }
        public Guid getuseroftoken(string token)
        {
            return refreshTokensdb.FirstOrDefault(x => x.refresh == token).UserId;
        }
        public void ChangePassword(Guid UserId, string NewPassword)
        {
            userdb.First(u => u.UserId == UserId).UserPW = NewPassword;
            Submit();
        }
    }
}
