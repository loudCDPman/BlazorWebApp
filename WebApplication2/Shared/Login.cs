using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;
using Microsoft.JSInterop;

namespace WebApplication2.Shared
{
    public class Loginlogic
    {
        public async Task<bool> HandleSubmit2(HttpClient httpcontext, IJSRuntime JS, TokenRequest TokenRequests)
        {
            Console.WriteLine("HandleSubmit");
            var blam = await httpcontext.PostAsJsonAsync<TokenRequest>("Account/token", TokenRequests);
            if (blam.IsSuccessStatusCode)
            {
                Console.WriteLine("hit");
                var content = await blam.Content.ReadAsStringAsync();
                Console.WriteLine("hit2");
                var tokens = JsonConvert.DeserializeObject<Token>(content);
                Console.WriteLine("hit3");
                await JS.InvokeVoidAsync("setLocalStorage", new object[] { "Session", tokens.token });
                await JS.InvokeVoidAsync("setLocalStorage", new object[] { "Sessionrefresh", tokens.RefreshToken });
                await JS.InvokeVoidAsync("setLocalStorage", new object[] { "UserName", TokenRequests.email });
                return true;
            }
            else
            {
                return false;
            }
        }

        public async Task<bool> HandleSubmitout(HttpClient httpcontext, IJSRuntime JS)
        {
            Console.WriteLine("HandleSubmit");
            httpcontext.DefaultRequestHeaders.Add("Authorization", $"Bearer {await JS.InvokeAsync<string>("getFromLocalStorage", "Session")}");
            var blam = await httpcontext.DeleteAsync("Account/token");
            if (blam.IsSuccessStatusCode)
            {
                await JS.InvokeVoidAsync("removeLocalStorage", new object[] { "Session"});
                await JS.InvokeVoidAsync("removeLocalStorage", new object[] { "Sessionrefresh"});
                return true;
            }
            else
            {
                if (httpcontext.DefaultRequestHeaders.Remove("Authorization"))
                    Console.WriteLine("blaaa");
                httpcontext.DefaultRequestHeaders.Add("Authorization", $"Bearer {await JS.InvokeAsync<string>("getFromLocalStorage", "Sessionrefresh")}");
                var responscontent = await httpcontext.GetAsync("Account/token");
                if (responscontent.IsSuccessStatusCode)
                {
                    var content = await responscontent.Content.ReadAsStringAsync();
                    var retokens = JsonConvert.DeserializeObject<Token>(content);
                    await JS.InvokeVoidAsync("setLocalStorage", new object[] { "Session", retokens.token });
                    await JS.InvokeVoidAsync("setLocalStorage", new object[] { "Sessionrefresh", retokens.RefreshToken });
                    await HandleSubmitout(httpcontext, JS);
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

        public async Task<bool> HandleSubmit3(HttpClient httpcontext, IJSRuntime JS, object Requests)
        {
            Console.WriteLine("HandleSubmit");
            if (httpcontext.DefaultRequestHeaders.Remove("Authorization"))
                Console.WriteLine("blaaa");
            httpcontext.DefaultRequestHeaders.Add("Authorization", $"Bearer {await JS.InvokeAsync<string>("getFromLocalStorage", "Session")}");
            var blam = await httpcontext.PostAsJsonAsync("Account/token", Requests);
            if (blam.IsSuccessStatusCode)
            {
                var content = await blam.Content.ReadAsStringAsync();
                //var tokens = JsonConvert.DeserializeObject<Token>(content);
                return true;
            }
            else
            {
                if (httpcontext.DefaultRequestHeaders.Remove("Authorization"))
                    Console.WriteLine("blaaa");
                httpcontext.DefaultRequestHeaders.Add("Authorization", $"Bearer {await JS.InvokeAsync<string>("getFromLocalStorage", "Sessionrefresh")}");
                var responscontent = await httpcontext.GetAsync("Account/token");
                if (responscontent.IsSuccessStatusCode)
                {
                    var content = await responscontent.Content.ReadAsStringAsync();
                    var retokens = JsonConvert.DeserializeObject<Token>(content);
                    await JS.InvokeVoidAsync("setLocalStorage", new object[] { "Session", retokens.token });
                    await JS.InvokeVoidAsync("setLocalStorage", new object[] { "Sessionrefresh", retokens.RefreshToken });
                    await HandleSubmit3(httpcontext, JS, Requests);
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

        public async Task<List<CustomerDataModel>> HandleSubmit34(HttpClient httpcontext, IJSRuntime JS)
        {
            if (httpcontext.DefaultRequestHeaders.Remove("Authorization"))
                Console.WriteLine("blaaa");
            Console.WriteLine("HandleSubmit");
            httpcontext.DefaultRequestHeaders.Add("Authorization", $"Bearer {await JS.InvokeAsync<string>("getFromLocalStorage", "Session")}");
            var blam = await httpcontext.GetAsync("CustomerData/GetAppointments");
            if (blam.IsSuccessStatusCode)
            {
                var content = await blam.Content.ReadFromJsonAsync<List<CustomerDataModel>>();
                //var tokens = JsonConvert.DeserializeObject<Token>(content);
                return content;
            }
            else
            {
                if (httpcontext.DefaultRequestHeaders.Remove("Authorization"))
                    Console.WriteLine("blaaa");
                httpcontext.DefaultRequestHeaders.Add("Authorization", $"Bearer {await JS.InvokeAsync<string>("getFromLocalStorage", "Sessionrefresh")}");
                var responscontent = await httpcontext.GetAsync("Account/refreshtoken");
                if (responscontent.IsSuccessStatusCode)
                {
                    Console.WriteLine("blaaa");
                    var content = await responscontent.Content.ReadAsStringAsync();
                    Console.WriteLine(content);
                    var retokens = JsonConvert.DeserializeObject<Token>(content);
                    await JS.InvokeVoidAsync("setLocalStorage", new object[] { "Session", retokens.token });
                    await JS.InvokeVoidAsync("setLocalStorage", new object[] { "Sessionrefresh", retokens.RefreshToken });
                    return await HandleSubmit34(httpcontext, JS);

                }
            }
            return null;
        }



        public async Task<List<object>> HandleSubmitGet(HttpClient httpcontext, IJSRuntime JS, string endpoint)
        {
            if (httpcontext.DefaultRequestHeaders.Remove("Authorization"))

            httpcontext.DefaultRequestHeaders.Add("Authorization", $"Bearer {await JS.InvokeAsync<string>("getFromLocalStorage", "Session")}");
            var blam = await httpcontext.GetAsync(endpoint);
            if (blam.IsSuccessStatusCode)
            {
                var content = await blam.Content.ReadFromJsonAsync<List<object>>();
                //var tokens = JsonConvert.DeserializeObject<Token>(content);
                return content;
            }
            else
            {
                if (httpcontext.DefaultRequestHeaders.Remove("Authorization"))

                httpcontext.DefaultRequestHeaders.Add("Authorization", $"Bearer {await JS.InvokeAsync<string>("getFromLocalStorage", "Sessionrefresh")}");
                var responscontent = await httpcontext.GetAsync("Account/refreshtoken");
                if (responscontent.IsSuccessStatusCode)
                {
                    var content = await responscontent.Content.ReadAsStringAsync();
                    var retokens = JsonConvert.DeserializeObject<Token>(content);
                    await JS.InvokeVoidAsync("setLocalStorage", new object[] { "Session", retokens.token });
                    await JS.InvokeVoidAsync("setLocalStorage", new object[] { "Sessionrefresh", retokens.RefreshToken });
                    return await HandleSubmitGet(httpcontext, JS, endpoint);

                }
            }
            return null;
        }

        public async Task<HttpResponseMessage> HandleSubmitPost<T>(HttpClient httpcontext, IJSRuntime JS, string endpoint, T submitobj)
        {
            if (httpcontext.DefaultRequestHeaders.Remove("Authorization"))

            httpcontext.DefaultRequestHeaders.Add("Authorization", $"Bearer {await JS.InvokeAsync<string>("getFromLocalStorage", "Session")}");
            var blam = await httpcontext.PostAsJsonAsync(endpoint, submitobj);
            Console.WriteLine("22");
            if (blam.IsSuccessStatusCode)
            {
                try
                {
                    // var content = await blam.Content.ReadFromJsonAsync<List<object>>();
                    ////var tokens = JsonConvert.DeserializeObject<Token>(content);JsonConvert.SerializeObject(submitobj)
                    return blam;
                }
                catch (Exception)
                {
                    return null;
                }
          
            }
            else
            {
                Console.WriteLine("33");
                if (httpcontext.DefaultRequestHeaders.Remove("Authorization"))
                { }
                httpcontext.DefaultRequestHeaders.Add("Authorization", $"Bearer {await JS.InvokeAsync<string>("getFromLocalStorage", "Sessionrefresh")}");
                var responscontent = await httpcontext.GetAsync("Account/refreshtoken");
                if (responscontent.IsSuccessStatusCode)
                {
                    var content = await responscontent.Content.ReadAsStringAsync();
                    var retokens = JsonConvert.DeserializeObject<Token>(content);
                    await JS.InvokeVoidAsync("setLocalStorage", new object[] { "Session", retokens.token });
                    await JS.InvokeVoidAsync("setLocalStorage", new object[] { "Sessionrefresh", retokens.RefreshToken });
                    return await HandleSubmitPost(httpcontext, JS, endpoint, submitobj);

                }
            }
            return null;
        }

        public async Task<HttpResponseMessage> HandleSubmitDelete(HttpClient httpcontext, IJSRuntime JS, string endpoint)
        {
            if (httpcontext.DefaultRequestHeaders.Remove("Authorization"))
            { }
            httpcontext.DefaultRequestHeaders.Add("Authorization", $"Bearer {await JS.InvokeAsync<string>("getFromLocalStorage", "Session")}");
            var blam = await httpcontext.DeleteAsync(endpoint);
            if (blam.IsSuccessStatusCode)
            {
                return blam;
            }
            else
            {
                if (httpcontext.DefaultRequestHeaders.Remove("Authorization"))
                { }
                httpcontext.DefaultRequestHeaders.Add("Authorization", $"Bearer {await JS.InvokeAsync<string>("getFromLocalStorage", "Sessionrefresh")}");
                var responscontent = await httpcontext.GetAsync("Account/refreshtoken");
                if (responscontent.IsSuccessStatusCode)
                {
                    var content = await responscontent.Content.ReadAsStringAsync();
                    var retokens = JsonConvert.DeserializeObject<Token>(content);
                    await JS.InvokeVoidAsync("setLocalStorage", new object[] { "Session", retokens.token });
                    await JS.InvokeVoidAsync("setLocalStorage", new object[] { "Sessionrefresh", retokens.RefreshToken });
                    return await HandleSubmitDelete(httpcontext, JS, endpoint);

                }
            }
            return null;
        }
    }
}
