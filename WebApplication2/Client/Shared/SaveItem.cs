using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication2.Shared;

namespace WebApplication2.Client.Shared
{
    public class SaveItem
    {
        public bool isloggedin { get; set; } = false;
        public bool CompletedDetails1 { get; set; } = false;
        public bool CompletedDetails2 { get; set; } = true;
        public CustomerDataModel data { get; set; }
        public async Task Update(string key, bool value)
        {
            Console.WriteLine("update");
            if (Notify != null)
            {
                 await Notify.Invoke(key, value);
            }
        }

        public event Func<string, bool, Task>? Notify;

    }
}
